import 'package:flutter/material.dart';
import 'package:interval_time_picker/interval_time_picker.dart';
import 'package:reservation_app/Models/restaurants/open_hours.dart';
import 'package:table_calendar/table_calendar.dart';

class DateTimePicker extends StatefulWidget {
  final List<OpenHours> schedule;
  final Function(bool, TimeOfDay, TimeOfDay, DateTime) onTimeChanged;
  final DateTime initialDay;
  final TimeOfDay initialStart;
  final TimeOfDay initialEnd;

  const DateTimePicker(
      {Key key,
      this.schedule,
      this.onTimeChanged,
      this.initialDay,
      this.initialStart,
      this.initialEnd})
      : super(key: key);

  @override
  _DateTimePickerState createState() => _DateTimePickerState();
}

class _DateTimePickerState extends State<DateTimePicker> {
  TimeOfDay _startTime;
  TimeOfDay _endTime;
  DateTime _selectedDay;

  var _isTimeValid = true;
  OpenHours _currentOpenHours;
  final _calendarController = CalendarController();
  final yesterday = DateTime(DateTime.now().year, DateTime.now().month, DateTime.now().day - 1);

  @override
  void initState() {
    _startTime = widget.initialStart;
    _endTime = widget.initialEnd;
    _selectedDay = widget.initialDay;

    _currentOpenHours =
        widget.schedule.firstWhere((element) => element.weekDay == _selectedDay.weekday);
    super.initState();
  }

  @override
  void dispose() {
    _calendarController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final column = Column(
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
          startingDayOfWeek: StartingDayOfWeek.monday,
          enabledDayPredicate: (day) {
            final dayStart = DateTime(day.year, day.month, day.day);
            return dayStart.isAfter(yesterday);
          },
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
                  style: ButtonStyle(
                    backgroundColor: _isTimeValid ? null : MaterialStateProperty.all(Colors.red),
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
                  style: ButtonStyle(
                    backgroundColor: _isTimeValid ? null : MaterialStateProperty.all(Colors.red),
                  ),
                  onPressed: () => _selectEndTime(context),
                ),
              )
            ],
          ),
        )
      ],
    );

    if (!_isTimeValid) {
      column.children.add(Padding(
        padding: EdgeInsets.fromLTRB(8, 8, 8, 0),
        child: Text(
          'Netinkamas rezervacijos laikas',
          textAlign: TextAlign.center,
          style: TextStyle(color: Colors.red),
        ),
      ));
    }

    return column;
  }

  void _onDaySelected(DateTime day) {
    _currentOpenHours = widget.schedule.firstWhere((element) => element.weekDay == day.weekday);
    _selectedDay = day;
    _validateTimes();
    widget.onTimeChanged(_isTimeValid, _startTime, _endTime, day);
  }

  void _validateTimes() {
    _isTimeValid =
        _startTime.hour + _startTime.minute / 60 < _endTime.hour + _endTime.minute / 60 &&
            (_startTime.hour > _currentOpenHours.open.hour ||
                _startTime.hour == _currentOpenHours.open.hour &&
                    _startTime.minute >= _currentOpenHours.open.minute) &&
            (_startTime.hour < _currentOpenHours.close.hour ||
                _startTime.hour == _currentOpenHours.close.hour &&
                    _startTime.minute < _currentOpenHours.close.minute) &&
            (_endTime.hour > _currentOpenHours.open.hour ||
                _endTime.hour == _currentOpenHours.open.hour &&
                    _endTime.minute > _currentOpenHours.open.minute) &&
            (_endTime.hour < _currentOpenHours.close.hour ||
                _endTime.hour == _currentOpenHours.close.hour &&
                    _endTime.minute <= _currentOpenHours.close.minute);
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
        _validateTimes();
        widget.onTimeChanged(_isTimeValid, _startTime, _endTime, _selectedDay);
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
        _validateTimes();
        widget.onTimeChanged(_isTimeValid, _startTime, _endTime, _selectedDay);
      });
    }
  }
}
