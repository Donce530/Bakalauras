import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'services/jwt_helper.dart';

class SplashPage extends StatefulWidget {
  @override
  _SplashPageState createState() => _SplashPageState();
}

class _SplashPageState extends State<SplashPage> {
  @override
  void initState() {
    SharedPreferences.getInstance().then((preferences) {
      if (!preferences.containsKey('token')) {
        debugPrint('notoken');
        Navigator.pushReplacementNamed(context, 'login');
      } else {
        var parsedToken = parseJwt(preferences.getString('token'));
        var expirationTimestamp = parsedToken['exp'];
        var expirationTime =
            DateTime.fromMicrosecondsSinceEpoch(expirationTimestamp * 1000);
        if (expirationTime
            .isBefore(DateTime.now().add(new Duration(days: 1)))) {
          debugPrint('tokenok');
          Navigator.pushReplacementNamed(context, 'home');
        } else {
          debugPrint('tokenShit');
          preferences.remove('user').then((_) => preferences
              .remove('token')
              .then(
                  (value) => Navigator.pushReplacementNamed(context, 'login')));
        }
      }
    });

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Container();
  }
}
