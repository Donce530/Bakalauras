import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';
import 'package:reservation_app/services/http_requests.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'Models/reservations/reservation_list_item.dart';

class QrScanPage extends StatefulWidget {
  @override
  _QrScanPageState createState() => _QrScanPageState();
}

class _QrScanPageState extends State<QrScanPage> {
  final GlobalKey _qrKey = GlobalKey();
  var _idScanned = false;
  bool _isCheckedIn = false;
  ReservationListItem _details;
  int _scannedRestaurantId;
  QRViewController _controller;

  @override
  void reassemble() {
    super.reassemble();
    if (Platform.isAndroid) {
      _controller.pauseCamera();
    } else if (Platform.isIOS) {
      _controller.resumeCamera();
    }
  }

  @override
  void initState() {
    SharedPreferences.getInstance().then((preferences) {
      if (preferences.containsKey('checkAction')) {
        _isCheckedIn = preferences.getBool('checkAction');
      } else {
        _isCheckedIn = false;
        preferences.setBool('checkAction', false);
      }
    });
    super.initState();
  }

  @override
  void dispose() {
    _controller?.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final viewStack = Stack(
      children: [],
    );

    viewStack.children.add(Scaffold(
      body: Column(
        children: [
          Expanded(
            flex: 1,
            child: Container(
              color: Colors.yellow,
              child: Padding(
                padding: EdgeInsets.fromLTRB(32, 64, 32, 32),
                child: Center(
                  child: Text(
                    'Nuskenuokite kodą',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 30,
                    ),
                  ),
                ),
              ),
            ),
          ),
          Expanded(
            flex: 4,
            child: QRView(
              key: _qrKey,
              onQRViewCreated: _onQRViewCreated,
            ),
          ),
          Expanded(
            flex: 1,
            child: Container(
              color: Colors.yellow,
              child: Center(
                child: SizedBox(
                  height: 50,
                  width: 200,
                  child: ElevatedButton(
                    child: Text(
                      'Atgal',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 30,
                      ),
                    ),
                    onPressed: () => Navigator.of(context).pop(),
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    ));

    if (_idScanned) {
      viewStack.children.add(_details == null ? _askForReservation() : _showDetails(_details));
    }

    return viewStack;
  }

  void _onQRViewCreated(QRViewController _controller) {
    this._controller = _controller;
    _controller.scannedDataStream.listen((scanData) {
      if (!_idScanned) {
        _tryCheckInOut(int.parse(scanData.code));
      }
    });
  }

  void _tryCheckInOut(int restaurantId) {
    if (_idScanned) {
      return;
    }
    _scannedRestaurantId = restaurantId;
    if (_isCheckedIn) {
      _tryCheckOutAction(restaurantId);
    } else {
      _tryCheckInAction(restaurantId);
    }
  }

  void _tryCheckOutAction(int restaurantId) {
    _idScanned = true;
    HttpRequests.get('api/Reservation/TryCheckOut/$restaurantId').then((response) {
      if (response.statusCode == 200) {
        _details = ReservationListItem.fromJson(jsonDecode(response.body));
      }

      setState(() {});
    });
  }

  void _tryCheckInAction(int restaurantId) {
    final now = DateTime.now();
    _idScanned = true;
    HttpRequests.get('api/Reservation/TryCheckIn/$restaurantId/${now.toIso8601String()}')
        .then((response) {
      if (response.statusCode == 200) {
        _details = ReservationListItem.fromJson(jsonDecode(response.body));
      }

      setState(() {});
    });
  }

  Widget _showDetails(ReservationListItem item) {
    return Container(
      child: Center(
        child: Card(
          child: Column(
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
              Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    Padding(
                      padding: EdgeInsets.all(10),
                      child: ElevatedButton(
                        onPressed: () {
                          setState(() {
                            _idScanned = false;
                          });
                        },
                        child: Padding(
                          padding: EdgeInsets.symmetric(vertical: 8),
                          child: Text(
                            'Iš naujo',
                            style: TextStyle(fontSize: 30),
                          ),
                        ),
                      ),
                    ),
                    Padding(
                      padding: EdgeInsets.all(10),
                      child: ElevatedButton(
                        onPressed: () {
                          setState(() {
                            _checkInOut();
                          });
                        },
                        child: Padding(
                          padding: EdgeInsets.symmetric(vertical: 8),
                          child: Text(
                            'Patvirtinti',
                            style: TextStyle(fontSize: 30),
                          ),
                        ),
                      ),
                    )
                  ]),
            ],
          ),
        ),
      ),
    );
  }

  Widget _askForReservation() {
    return Container(
      child: Center(
        child: Padding(
          padding: EdgeInsets.symmetric(horizontal: 25),
          child: Card(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Padding(
                  padding: EdgeInsets.all(8),
                  child: Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Flexible(
                        child: Text(
                          'Šiuo metu neturite rezervuoto staliuko.\n Ar norite užsirezervuoti?',
                          textAlign: TextAlign.center,
                          style: TextStyle(fontSize: 30),
                        ),
                      )
                    ],
                  ),
                ),
                Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Padding(
                        padding: EdgeInsets.all(10),
                        child: ElevatedButton(
                          onPressed: () {
                            Navigator.of(context, rootNavigator: true)
                                .popUntil((route) => route.isFirst);
                          },
                          child: Padding(
                            padding: EdgeInsets.symmetric(vertical: 8),
                            child: Text(
                              'Ne',
                              style: TextStyle(fontSize: 30),
                            ),
                          ),
                        ),
                      ),
                      Padding(
                        padding: EdgeInsets.all(10),
                        child: ElevatedButton(
                          onPressed: () {
                            Navigator.of(context, rootNavigator: true)
                                .pushNamed('/restaurantDetails', arguments: _scannedRestaurantId);
                          },
                          child: Padding(
                            padding: EdgeInsets.symmetric(vertical: 8),
                            child: Text(
                              'Taip',
                              style: TextStyle(fontSize: 30),
                            ),
                          ),
                        ),
                      )
                    ]),
              ],
            ),
          ),
        ),
      ),
    );
  }

  void _checkInOut() {
    if (_isCheckedIn) {
      _checkOut();
    } else {
      _checkIn();
    }
  }

  void _checkOut() {
    HttpRequests.get('api/Reservation/CheckOut/${_details.id}/${DateTime.now().toIso8601String()}')
        .then((response) {
      if (response.statusCode != 200) {
        throw Exception('Could not check out');
      }

      SharedPreferences.getInstance().then((preferences) {
        preferences.setBool('checkAction', false);
      });

      Navigator.of(context, rootNavigator: true)
          .pushNamed('/qrScanConfirm', arguments: 'Ačiū, iki kito apsilankymo!');
    });
  }

  void _checkIn() {
    HttpRequests.get('api/Reservation/CheckIn/${_details.id}/${DateTime.now().toIso8601String()}')
        .then((response) {
      if (response.statusCode != 200) {
        throw Exception('Could not check out');
      }

      SharedPreferences.getInstance().then((preferences) {
        preferences.setBool('checkAction', true);
      });

      Navigator.of(context, rootNavigator: true)
          .pushNamed('/qrScanConfirm', arguments: 'Sveiki atvykę!');
    });
  }
}
