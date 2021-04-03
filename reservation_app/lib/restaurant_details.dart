import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:reservation_app/Models/restaurants/restaurant_details.dart';
import 'package:reservation_app/services/http_requests.dart';

class RestaurantDetailsPage extends StatefulWidget {
  final int restaurantId;

  RestaurantDetailsPage(this.restaurantId);

  @override
  _RestaurantDetailsPageState createState() => _RestaurantDetailsPageState();
}

class _RestaurantDetailsPageState extends State<RestaurantDetailsPage> {
  final Map<int, String> _weekDays = {
    1: 'Pirmadienis',
    2: 'Antradienis',
    3: 'Trečiadienis',
    4: 'Ketvirtadienis',
    5: 'Penktadienis',
    6: 'Šeštadienis',
    7: 'Sekmadienis',
  };

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: _details,
        builder: (context, snapshot) {
          if (snapshot.connectionState != ConnectionState.done) {
            return Scaffold(
              backgroundColor: Colors.yellow,
              body: Center(
                child: SizedBox(
                  child: CircularProgressIndicator(),
                  height: 24,
                  width: 24,
                ),
              ),
            );
          } else {
            return _buildDetails(snapshot.data);
          }
        });
  }

  Widget _buildDetails(RestaurantDetails details) {
    return Scaffold(
      backgroundColor: Colors.yellow,
      body: Padding(
        padding: EdgeInsets.all(16),
        child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Center(
                child: IntrinsicHeight(
                  child: Center(
                    child: Card(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.stretch,
                        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                        children: [
                          Padding(
                            padding: EdgeInsets.all(8),
                            child: Text(
                              details.title,
                              textAlign: TextAlign.left,
                              style: TextStyle(fontSize: 40, fontWeight: FontWeight.bold),
                            ),
                          ),
                          Padding(
                            padding: EdgeInsets.all(8),
                            child: Text(
                              details.description,
                              textAlign: TextAlign.left,
                              style: TextStyle(fontSize: 24),
                            ),
                          ),
                          Padding(
                            padding: EdgeInsets.all(8),
                            child: Row(
                              crossAxisAlignment: CrossAxisAlignment.center,
                              mainAxisAlignment: MainAxisAlignment.start,
                              children: [
                                Icon(Icons.location_on),
                                Text(
                                  '${details.address}, ${details.city}',
                                  textAlign: TextAlign.left,
                                  style: TextStyle(fontSize: 20),
                                ),
                              ],
                            ),
                          ),
                          Padding(
                            padding: EdgeInsets.fromLTRB(8, 8, 8, 8),
                            child: FittedBox(
                              fit: BoxFit.fill,
                              child: DataTable(
                                columns: [
                                  DataColumn(
                                    label: Expanded(
                                      child: Text(
                                        'Diena',
                                        textAlign: TextAlign.center,
                                        style: TextStyle(fontStyle: FontStyle.italic),
                                      ),
                                    ),
                                  ),
                                  DataColumn(
                                    label: Expanded(
                                      child: Text(
                                        'Atsidarome',
                                        textAlign: TextAlign.center,
                                        style: TextStyle(fontStyle: FontStyle.italic),
                                      ),
                                    ),
                                  ),
                                  DataColumn(
                                    label: Expanded(
                                      child: Text(
                                        'Užsidarome',
                                        textAlign: TextAlign.center,
                                        style: TextStyle(fontStyle: FontStyle.italic),
                                      ),
                                    ),
                                  ),
                                ],
                                rows: details.schedule
                                    .map((oh) => DataRow(cells: [
                                          DataCell(
                                            Center(
                                              child: Text(
                                                _weekDays[oh.weekDay],
                                              ),
                                            ),
                                          ),
                                          DataCell(
                                            Center(
                                              child: Text(
                                                TimeOfDay.fromDateTime(oh.open).format(context),
                                              ),
                                            ),
                                          ),
                                          DataCell(
                                            Center(
                                              child: Text(
                                                TimeOfDay.fromDateTime(oh.close).format(context),
                                              ),
                                            ),
                                          ),
                                        ]))
                                    .toList(),
                              ),
                            ),
                          )
                        ],
                      ),
                    ),
                  ),
                ),
              ),
              Padding(
                padding: EdgeInsets.only(top: 40, bottom: 40),
                child: ElevatedButton(
                  onPressed: () {
                    Navigator.of(context, rootNavigator: true)
                        .pushNamed('/newReservation', arguments: details);
                  },
                  child: Padding(
                    padding: EdgeInsets.symmetric(vertical: 8),
                    child: Text(
                      'Rezervuoti',
                      style: TextStyle(fontSize: 30),
                    ),
                  ),
                ),
              )
            ]),
      ),
    );
  }

  Future<RestaurantDetails> get _details async {
    final response =
        await HttpRequests.get('/api/Restaurant/Details', {'id': widget.restaurantId.toString()});
    if (response.statusCode != 200) {
      throw Exception('could not get restaurant');
    }

    final details = RestaurantDetails.fromJson(jsonDecode(response.body));
    details.schedule.sort((a, b) => a.weekDay < b.weekDay ? -1 : 1);
    details.schedule.firstWhere((element) => element.weekDay == 0).weekDay = 7;
    details.schedule.add(details.schedule.removeAt(0));

    return details;
  }
}
