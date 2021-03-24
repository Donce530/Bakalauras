import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:reservation_app/login.dart';
import 'package:reservation_app/make_reservation.dart';
import 'package:reservation_app/register.dart';
import 'package:reservation_app/restaurant_details.dart';
import 'package:reservation_app/splash.dart';
import 'package:intl/date_symbol_data_local.dart';

import 'home.dart';

void main() {
  initializeDateFormatting('lt_LT').then((_) => runApp(MyApp()));
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    SystemChrome.setPreferredOrientations([DeviceOrientation.portraitUp]);

    return MaterialApp(
        title: 'Flutter Demo',
        theme: ThemeData(primarySwatch: Colors.orange),
        initialRoute: 'splash',
        builder: (context, child) => MediaQuery(
            data: MediaQuery.of(context).copyWith(alwaysUse24HourFormat: true), child: child),
        routes: <String, WidgetBuilder>{
          'login': (context) => LoginPage(),
          'home': (context) => HomePage(),
          'splash': (context) => SplashPage(),
          '/register': (context) => RegisterPage(),
          '/restaurantDetails': (context) => RestaurantDetailsPage(),
          '/newReservation': (context) => MakeReservationPage()
        });
  }
}
