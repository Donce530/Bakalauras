import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:reservation_app/services/http_requests.dart';
import 'package:reservation_app/utils/loading_spinner.dart';

import 'Models/reservations/reservation_list_item.dart';
import 'Models/reservations/reservation_state.dart';

class ReservationsPage extends StatefulWidget {
  @override
  _ReservationsPageState createState() => _ReservationsPageState();
}

class _ReservationsPageState extends State<ReservationsPage> {
  List<ReservationListItem> _upcomingReservations;
  List<ReservationListItem> _previousReservations;
  String _filter;
  final labelStyle = TextStyle(
    fontSize: 30,
  );

  @override
  initState() {
    _getReservations(_filter);
    super.initState();
  }

  Widget _buildUpcomingItem(BuildContext context, int i) {
    if (i.isOdd) {
      return Divider();
    }

    var index = i ~/ 2;
    if (index >= _upcomingReservations.length) {
      return Loader();
    }

    return _buildRow(_upcomingReservations[index], true);
  }

  Widget _buildPreviousItem(BuildContext context, int i) {
    if (i.isOdd) {
      return Divider();
    }

    var index = i ~/ 2;
    if (index >= _previousReservations.length) {
      return Loader();
    }

    return _buildRow(_previousReservations[index], false);
  }

  Card _buildRow(ReservationListItem item, bool allowDelete) {
    final cancelButton = Padding(
      padding: EdgeInsets.only(top: 40, bottom: 40),
      child: OutlinedButton(
        style: ButtonStyle(foregroundColor: MaterialStateProperty.all(Colors.red)),
        onPressed: () => _confirmCancel(item.id),
        child: Padding(
          padding: EdgeInsets.symmetric(vertical: 8),
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Text(
                'Atšaukti ',
                style: TextStyle(fontSize: 30),
              ),
              Icon(Icons.delete)
            ],
          ),
        ),
      ),
    );

    var content = Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        Padding(
          padding: EdgeInsets.all(8),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Padding(
                padding: EdgeInsets.only(right: 4),
                child: Icon(Icons.restaurant),
              ),
              Text(
                'Staliukas # ${item.tableNumber}',
                textAlign: TextAlign.left,
                style: TextStyle(fontSize: 20),
              ),
            ],
          ),
        ),
        Padding(
          padding: EdgeInsets.all(8),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Padding(
                padding: EdgeInsets.only(right: 4),
                child: Icon(Icons.people),
              ),
              Text(
                '${item.tableSeats} sėdimos vietos',
                textAlign: TextAlign.left,
                style: TextStyle(fontSize: 20),
              ),
            ],
          ),
        ),
        Padding(
          padding: EdgeInsets.all(8),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Padding(
                padding: EdgeInsets.only(right: 4),
                child: Icon(Icons.arrow_forward),
              ),
              Text(
                'Pradžia: ${DateFormat("kk:mm", 'lt').format(item.start)}',
                textAlign: TextAlign.left,
                style: TextStyle(fontSize: 20),
              ),
            ],
          ),
        ),
        Padding(
          padding: EdgeInsets.all(8),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Padding(
                padding: EdgeInsets.only(right: 4),
                child: Icon(Icons.arrow_back),
              ),
              Text(
                'Pabaiga: ${DateFormat("kk:mm", 'lt').format(item.end)}',
                textAlign: TextAlign.left,
                style: TextStyle(fontSize: 20),
              ),
            ],
          ),
        ),
        Padding(
          padding: EdgeInsets.all(8),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Icon(Icons.location_on),
              Text(
                '${item.restaurantAddress}',
                textAlign: TextAlign.left,
                style: TextStyle(fontSize: 20),
              ),
            ],
          ),
        ),
      ],
    );

    if (allowDelete) {
      content.children.add(cancelButton);
    }

    final card = Card(
      child: Padding(
        padding: EdgeInsets.fromLTRB(8, 8, 0, 8),
        child: ExpansionTile(
          onExpansionChanged: (value) {
            setState(() {
              item.isExpanded = value;
            });
          },
          title: Text(item.restaurantTitle),
          trailing: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Text(
                  '${DateFormat("dd MM yyyy", 'lt').format(item.day)} ${DateFormat("kk:mm", 'lt').format(item.start)}'),
              IconButton(
                  icon: Icon(item.isExpanded ? Icons.arrow_upward : Icons.arrow_downward),
                  onPressed: null),
            ],
          ),
          children: [content],
        ),
      ),
    );

    return card;
  }

  @override
  Widget build(BuildContext context) {
    if (_upcomingReservations == null) {
      return Center(
        child: SizedBox(
          child: CircularProgressIndicator(),
          height: 24,
          width: 24,
        ),
      );
    }

    return Column(
      children: [
        Padding(
          child: Row(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              Expanded(
                child: Padding(
                  padding: EdgeInsets.only(right: 10, left: 10),
                  child: TextField(
                    onSubmitted: _onFilterSubmitted,
                    decoration: InputDecoration(
                      labelText: 'Paieška',
                    ),
                  ),
                ),
              )
            ],
          ),
          padding: EdgeInsets.all(5),
        ),
        Padding(
          child: Text(
            'Būsimos rezervacijos',
            style: labelStyle,
          ),
          padding: EdgeInsets.all(5),
        ),
        Expanded(
          child: ListView.builder(
            padding: EdgeInsets.all(16),
            itemCount: _upcomingReservations != null ? _upcomingReservations.length * 2 : 1,
            itemBuilder: _buildUpcomingItem,
          ),
        ),
        Padding(
          child: Text(
            'Ankstesnės rezervacijos',
            style: labelStyle,
          ),
          padding: EdgeInsets.all(5),
        ),
        Expanded(
          child: ListView.builder(
            padding: EdgeInsets.all(16),
            itemCount: _previousReservations != null ? _previousReservations.length * 2 : 1,
            itemBuilder: _buildPreviousItem,
          ),
        ),
      ],
    );
  }

  Future<void> _onFilterSubmitted(String filter) async {
    if (filter == _filter) {
      return;
    }

    this._filter = filter;
    await _getReservations(filter);
  }

  Future<void> _getReservations(String filter) async {
    final url = '/api/Reservation/GetByUser';
    final queryParams = {'filter': filter};
    final response = await HttpRequests.get(url, queryParams);

    if (response.statusCode != 200) {
      throw new Exception("Couldn't get restaurants");
    }
    final list = jsonDecode(response.body) as List<dynamic>;

    var reservations =
        _upcomingReservations = list.map((le) => ReservationListItem.fromJson(le)).toList()
          ..sort((a, b) {
            var comp = a.day.compareTo(b.day);
            if (comp == 0) {
              comp = a.start.compareTo(b.start);
            }
            return comp;
          });

    setState(() {
      _upcomingReservations = reservations
          .where((r) =>
              r.day.isAfter(DateTime.now().add(Duration(days: -1))) &&
              r.state == ReservationState.Created)
          .toList();
      _previousReservations =
          reservations.where((r) => !_upcomingReservations.contains(r)).toList();
    });
  }

  Future<void> _confirmCancel(int id) async {
    final okButton = TextButton(
      child: Text("Taip"),
      onPressed: () {
        _cancel(id).then((_) => Navigator.of(context, rootNavigator: true).pop('dialog'));
      },
    );

    final cancelButton = TextButton(
      child: Text("Ne"),
      onPressed: () {
        Navigator.of(context, rootNavigator: true).pop('dialog');
      },
    );

    // set up the AlertDialog
    final alert = AlertDialog(
      title: Text("Įspėjimas"),
      content: Text("Ar tikrai norite atšaukti rezervaciją ?"),
      actions: [
        cancelButton,
        okButton,
      ],
    );

    // show the dialog
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return alert;
      },
    );
  }

  Future<void> _cancel(int id) async {
    var url = '/api/Reservation/Cancel/$id';

    var response = await HttpRequests.delete(url);

    if (response.statusCode != 200) {
      throw new Exception('Could not cancel reservaiton');
    }

    setState(() {
      _upcomingReservations.removeWhere((element) => element.id == id);
    });
  }
}
