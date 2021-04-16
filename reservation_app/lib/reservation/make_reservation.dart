import 'dart:async';
import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:reservation_app/Models/reservations/new_reservation.dart';
import 'package:reservation_app/Models/restaurants/plan/plan_table.dart';
import 'package:reservation_app/Models/restaurants/restaurant_details.dart';
import 'package:reservation_app/reservation/date_time_picker.dart';
import 'package:reservation_app/reservation/reservation_summary.dart';
import 'package:reservation_app/reservation/table_picker.dart';
import 'package:reservation_app/services/http_requests.dart';

class MakeReservationPage extends StatefulWidget {
  final RestaurantDetails restaurant;

  const MakeReservationPage({Key key, this.restaurant}) : super(key: key);

  @override
  _MakeReservationPageState createState() => _MakeReservationPageState();
}

class _MakeReservationPageState extends State<MakeReservationPage> {
  var _currentStep = 0;

  DateTimePicker _dateTimePicker;
  TablePicker _tablePicker;

  var _availableTableStream = StreamController<List<int>>();

  var _isTimeValid = true;
  TimeOfDay _startTime;
  TimeOfDay _endTime;
  var _day = DateTime.now();
  PlanTable _selectedTable;

  @override
  void initState() {
    var addHour = false;
    var startMinutes = (TimeOfDay.now().minute + (15 - TimeOfDay.now().minute % 15));
    if (startMinutes >= 60) {
      startMinutes = startMinutes % 60;
      addHour = true;
    }
    _startTime = TimeOfDay.now().replacing(minute: startMinutes);
    if (addHour) {
      _startTime = _startTime.replacing(hour: _startTime.hour + 1);
      addHour = false;
    }

    var endMinutes = (TimeOfDay.now().minute + (15 - TimeOfDay.now().minute % 15) + 15);
    if (endMinutes >= 60) {
      endMinutes = endMinutes % 60;
      addHour = true;
    }
    _endTime = TimeOfDay.now().replacing(minute: endMinutes);
    if (addHour) {
      _endTime = _endTime.replacing(hour: _endTime.hour + 1);
    }

    _dateTimePicker = DateTimePicker(
      schedule: widget.restaurant.schedule,
      onTimeChanged: _onTimePicked,
      initialDay: _day,
      initialEnd: _endTime,
      initialStart: _startTime,
    );
    _tablePicker = TablePicker(
      availableTableStream: _availableTableStream.stream,
      restaurantId: widget.restaurant.id,
      onTablePicked: _onTablePicked,
    );
    _updateAvailableTables(_startTime, _endTime, _day);
    super.initState();
  }

  @override
  void dispose() {
    _availableTableStream.close();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.yellow,
      body: Container(
        child: Column(children: [
          Expanded(
            child: Theme(
              data: Theme.of(context).copyWith(canvasColor: Colors.yellow),
              child: Stepper(
                type: StepperType.vertical,
                physics: ScrollPhysics(),
                currentStep: _currentStep,
                onStepTapped: null,
                onStepContinue: () {
                  if (_currentStep < 2) {
                    setState(() {
                      _currentStep++;
                    });
                  }
                },
                onStepCancel: () {
                  if (_currentStep > 0) {
                    setState(() {
                      _currentStep--;
                    });
                  }
                },
                controlsBuilder: (BuildContext context,
                    {VoidCallback onStepContinue, VoidCallback onStepCancel}) {
                  return Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: <Widget>[
                      IconButton(
                        onPressed: _currentStep != 0 ? onStepCancel : null,
                        icon: Icon(Icons.arrow_upward),
                      ),
                      IconButton(
                        onPressed: _isCurrentStepValid() ? onStepContinue : null,
                        icon: Icon(Icons.arrow_downward),
                      ),
                    ],
                  );
                },
                steps: [_timePickStep(context), _tablePickStep(context), _confirmStep],
              ),
            ),
          ),
        ]),
      ),
    );
  }

  Step _timePickStep(context) {
    return Step(title: Text('Pasirinkite laiką'), content: _dateTimePicker);
  }

  Step _tablePickStep(context) {
    return Step(title: Text('Pasirinkite staliuką'), content: _tablePicker);
  }

  Step get _confirmStep {
    Widget content;
    if (!_isTimeValid || _selectedTable == null) {
      content = Container();
    } else {
      content = ReservationSummary(
          reservation: NewReservation(
              widget.restaurant.id,
              _day,
              DateTime(_day.year, _day.month, _day.day, _startTime.hour, _startTime.minute),
              DateTime(_day.year, _day.month, _day.day, _endTime.hour, _endTime.minute),
              _selectedTable.id),
          restaurant: widget.restaurant,
          table: _selectedTable);
    }

    return Step(
      title: Text('Patvirtinkite rezervaciją'),
      content: content,
    );
  }

  bool _isCurrentStepValid() {
    switch (_currentStep) {
      case 0:
        return _isTimeValid;
      case 1:
        return _selectedTable != null;
      case 2:
        return true;
    }

    return false;
  }

  void _onTablePicked(PlanTable table) {
    setState(() {
      _selectedTable = table;
    });
  }

  void _onTimePicked(bool isValid, TimeOfDay start, TimeOfDay end, DateTime day) {
    setState(() {
      _isTimeValid = isValid;
      _startTime = start;
      _endTime = end;
      _day = day;

      if (isValid) {
        _updateAvailableTables(start, end, day);
      }
    });
  }

  void _updateAvailableTables(TimeOfDay start, TimeOfDay end, DateTime day) {
    Map<String, String> queryParams = {
      'restaurantId': widget.restaurant.id.toString(),
      'startTime':
          DateTime(day.year, day.month, day.day, start.hour, start.minute).toIso8601String(),
      'endTime': DateTime(day.year, day.month, day.day, end.hour, end.minute).toIso8601String(),
      'day': day.toIso8601String()
    };

    HttpRequests.get('/api/Reservation/GetTablesAvailableToReserve', queryParams).then((response) {
      if (response.statusCode != 200) {
        throw Exception('Could not get available tables');
      }
      final result = jsonDecode(response.body) as List<dynamic>;
      final tableIds = result.cast<int>();
      _availableTableStream.sink.add(tableIds);
    });
  }
}
