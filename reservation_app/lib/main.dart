import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:reservation_app/login.dart';
import 'package:reservation_app/reservation/make_reservation.dart';
import 'package:reservation_app/register.dart';
import 'package:reservation_app/reservation/reservation_created.dart';
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
        onGenerateRoute: (RouteSettings settings) {
          final Map<String, WidgetBuilder> routes = {
            'login': (context) => LoginPage(),
            'home': (context) => HomePage(),
            'splash': (context) => SplashPage(),
            '/register': (context) => RegisterPage(),
            '/restaurantDetails': (context) => RestaurantDetailsPage(settings.arguments),
            '/newReservation': (context) => MakeReservationPage(restaurant: settings.arguments),
            '/reservationCreated': (context) =>
                ReservationCompletedPage(reservation: settings.arguments)
          };

          final builder = routes[settings.name];
          return MaterialPageRoute(builder: (ctx) => builder(ctx));
        });
  }
}
