import 'package:flutter/material.dart';
import 'package:reservation_app/Models/utils/new_reservation_params.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:interval_time_picker/interval_time_picker.dart';

class MakeReservationPage extends StatefulWidget {
  @override
  _MakeReservationPageState createState() => _MakeReservationPageState();
}

class _MakeReservationPageState extends State<MakeReservationPage> {
  var _currentStep = 0;
  var _startTime = TimeOfDay.now();
  var _endTime = TimeOfDay.now();
  var _isTimeValid = true;
  final _calendarController = CalendarController();

  @override
  void dispose() {
    _calendarController.dispose();
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
                onStepTapped: (step) {
                  setState(() => _currentStep = step);
                },
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
                        onPressed: onStepCancel,
                        icon: Icon(Icons.arrow_upward),
                      ),
                      IconButton(
                        onPressed: _isCurrentStepValid() ? onStepContinue : null,
                        icon: Icon(Icons.arrow_downward),
                      ),
                    ],
                  );
                },
                steps: [_timePickStep(context), _tablePickStep, _confirmStep],
              ),
            ),
          ),
        ]),
      ),
    );
  }

  Step _timePickStep(context) {
    return Step(
      title: Text('Pasirinkite laiką'),
      content: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          TableCalendar(
            calendarController: _calendarController,
            locale: 'lt_LT',
            headerStyle: HeaderStyle(formatButtonVisible: false, centerHeaderTitle: true),
            calendarStyle: CalendarStyle(
              selectedColor: Colors.orange,
              todayColor: Colors.orangeAccent,
            ),
            enabledDayPredicate: (day) => day.isAfter(DateTime.now().subtract(Duration(days: 1))),
            onDaySelected: (day, events, holidays) => _onDaySelected(day),
          ),
          Padding(
            padding: EdgeInsets.all(8),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                Padding(
                  padding: EdgeInsets.only(right: 3),
                  child: OutlinedButton(
                    child: Text(
                      'Pradžia ${_startTime.format(context)}',
                      style: TextStyle(color: Colors.black),
                    ),
                    onPressed: () => _selectStartTime(context),
                  ),
                ),
                Padding(
                  padding: EdgeInsets.only(left: 3),
                  child: OutlinedButton(
                    child: Text(
                      'Pabaiga ${_endTime.format(context)}',
                      style: TextStyle(color: Colors.black),
                    ),
                    onPressed: () => _selectEndTime(context),
                  ),
                )
              ],
            ),
          )
        ],
      ),
    );
  }

  Step get _tablePickStep {
    return Step(
      title: Text('Pasirinkite'),
      content: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [],
      ),
    );
  }

  Step get _confirmStep {
    return Step(
      title: Text('Pasirinkite'),
      content: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [],
      ),
    );
  }

  bool _isCurrentStepValid() {
    switch (_currentStep) {
      case 0:
        return _isTimeValid;
      case 1:
        return false;
      case 2:
        return false;
    }
  }

  void _onDaySelected(DateTime day) {
    var params = ModalRoute.of(context).settings.arguments as NewReservationParams;
    params.schedule.firstWhere((element) => element.weekDay == 0).weekDay = 7;
    var openHours = params.schedule.firstWhere((element) => element.weekDay == day.weekday);
  }

  Future<void> _selectStartTime(BuildContext context) async {
    var selectedTime = await showIntervalTimePicker(
        context: context,
        initialTime: _startTime,
        helpText: 'Pasirinkite laiką',
        cancelText: 'Atšaukti',
        confirmText: 'Pasirinkti',
        interval: 15);
    if (selectedTime != null) {
      setState(() {
        _startTime = selectedTime;
      });
    }
  }

  Future<void> _selectEndTime(BuildContext context) async {
    var selectedTime = await showIntervalTimePicker(
        context: context,
        initialTime: _endTime,
        helpText: 'Pasirinkite laiką',
        cancelText: 'Atšaukti',
        confirmText: 'Pasirinkti',
        interval: 15);
    if (selectedTime != null) {
      setState(() {
        _endTime = selectedTime;
      });
    }
  }
}
