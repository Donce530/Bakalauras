import 'package:flutter/material.dart';
import 'package:reservation_app/reservations.dart';
import 'package:reservation_app/restaurants.dart';
import 'package:shared_preferences/shared_preferences.dart';

class HomePage extends StatefulWidget {
  HomePage({Key key}) : super(key: key);

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final _navigatorKey = GlobalKey<NavigatorState>();
  var _currentIndex = 0;
  static const _routeOptions = ['restaurants', 'reservations'];
  static const _navigationItems = [
    BottomNavigationBarItem(icon: Icon(Icons.restaurant), label: 'Restoranai'),
    BottomNavigationBarItem(icon: Icon(Icons.playlist_add_check), label: 'Rezervacijos'),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.yellow,
      appBar: AppBar(
        title: Text(_navigationItems[_currentIndex].label),
        actions: [
          IconButton(
            icon: Icon(Icons.logout),
            onPressed: _logout,
          )
        ],
      ),
      floatingActionButton: FloatingActionButton(
        child: Icon(Icons.qr_code),
        onPressed: () {
          Navigator.of(context, rootNavigator: true).pushNamed('/qrScan');
        },
      ),
      body: Navigator(
          key: _navigatorKey, initialRoute: 'restaurants', onGenerateRoute: _resolveRoute),
      bottomNavigationBar: _getNavigationBar(),
    );
  }

  BottomNavigationBar _getNavigationBar() {
    return BottomNavigationBar(
        items: _navigationItems,
        currentIndex: _currentIndex,
        onTap: (index) {
          if (index != _currentIndex) {
            setState(() {
              _navigatorKey.currentState.pushReplacementNamed(_routeOptions[index]);
              _currentIndex = index;
            });
          }
        });
  }

  Future<void> _logout() async {
    var preferences = await SharedPreferences.getInstance();
    await preferences.remove('user');
    await preferences.remove('token');
    Navigator.of(context, rootNavigator: true).pushReplacementNamed('login');
  }

  Route<dynamic> _resolveRoute(RouteSettings settings) {
    WidgetBuilder builder;
    switch (settings.name) {
      case 'restaurants':
        builder = (context) => RestaurantsPage();
        break;
      case 'reservations':
        builder = (context) => ReservationsPage();
        break;
      default:
        throw Exception('Invalid route');
    }

    return MaterialPageRoute(builder: builder, settings: settings);
  }
}
