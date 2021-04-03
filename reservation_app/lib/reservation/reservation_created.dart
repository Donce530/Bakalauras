import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:reservation_app/Models/reservations/new_reservation.dart';
import 'package:reservation_app/services/http_requests.dart';
import 'package:reservation_app/utils/loading_spinner.dart';

class ReservationCompletedPage extends StatefulWidget {
  final NewReservation reservation;

  const ReservationCompletedPage({Key key, this.reservation}) : super(key: key);

  @override
  _ReservationCompletedPageState createState() => _ReservationCompletedPageState();
}

class _ReservationCompletedPageState extends State<ReservationCompletedPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.yellow,
      body: Center(
        child: FutureBuilder(
          future: _createReservation(),
          builder: (context, snapshot) {
            if (snapshot.connectionState != ConnectionState.done || snapshot.hasError) {
              return Loader();
            } else {
              return _buildConfirmMessage();
            }
          },
        ),
      ),
    );
  }

  Widget _buildConfirmMessage() {
    return Center(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Padding(
            padding: EdgeInsets.all(40),
            child: Icon(
              Icons.done_outline,
              size: 100,
            ),
          ),
          Padding(
            padding: EdgeInsets.fromLTRB(8, 8, 8, 32),
            child: Text(
              'Patvirtinta!',
              textAlign: TextAlign.center,
              style: TextStyle(fontSize: 50, fontWeight: FontWeight.bold),
            ),
          ),
        ],
      ),
    );
  }

  Future<void> _createReservation() async {
    final url = '/api/Reservation/Create';
    var response = await HttpRequests.post(url, widget.reservation);

    if (response.statusCode != 200) {
      throw Exception('Could not create reservation');
    } else {
      Future.delayed(Duration(seconds: 2), () {
        Navigator.of(context, rootNavigator: true).popUntil((route) => route.isFirst);
      });
    }
  }
}
