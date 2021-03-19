import 'package:flutter/material.dart';
import 'package:reservation_app/login.dart';
import 'package:reservation_app/register.dart';
import 'package:reservation_app/splash.dart';

import 'home.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        title: 'Flutter Demo',
        theme: ThemeData(primarySwatch: Colors.orange),
        initialRoute: 'splash',
        routes: <String, WidgetBuilder>{
          'login': (context) => LoginPage(),
          'home': (context) => HomePage(),
          'splash': (context) => SplashPage(),
          '/register': (context) => RegisterPage(),
        });
  }
}
