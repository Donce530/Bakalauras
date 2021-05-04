import 'dart:convert';

import 'package:another_flushbar/flushbar.dart';
import 'package:flutter/material.dart';
import 'package:reservation_app/register.dart';
import 'package:reservation_app/services/animated_routes.dart';
import 'package:reservation_app/services/http_requests.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'Models/login/login_request.dart';
import 'package:another_flushbar/flushbar_route.dart' as flushbarRoute;

import 'home.dart';

class LoginPage extends StatefulWidget {
  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final _formKey = GlobalKey<FormState>();
  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    final focusNode = FocusScope.of(context);

    final form = Form(
      key: _formKey,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        mainAxisSize: MainAxisSize.min,
        children: [
          Padding(
              padding: EdgeInsets.only(top: 20),
              child: Text(
                'Sveiki, prisijunkite',
                textAlign: TextAlign.center,
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 40),
              )),
          Padding(
              padding: EdgeInsets.fromLTRB(20, 20, 20, 10),
              child: TextFormField(
                decoration:
                    const InputDecoration(icon: Icon(Icons.person), labelText: 'El. pašto adresas'),
                controller: _usernameController,
                validator: (value) {
                  return value != null && value.isEmpty ? 'Būtina užpildyti' : null;
                },
                textInputAction: TextInputAction.next,
                onEditingComplete: () => focusNode.nextFocus(),
              )),
          Padding(
            padding: EdgeInsets.fromLTRB(20, 0, 20, 20),
            child: TextFormField(
              decoration: const InputDecoration(icon: Icon(Icons.lock), labelText: 'Slaptažodis'),
              controller: _passwordController,
              validator: (value) {
                return value != null && value.isEmpty ? 'Būtina užpildyti' : null;
              },
              obscureText: true,
              textInputAction: TextInputAction.done,
              onEditingComplete: () {
                focusNode.unfocus();
              },
            ),
          ),
          Padding(
            padding: EdgeInsets.fromLTRB(20, 0, 20, 20),
            child: ElevatedButton(
              style: ButtonStyle(),
              onPressed: () => _login(),
              child: Padding(
                padding: EdgeInsets.all(10),
                child: Text(
                  'Prisijungti',
                  textAlign: TextAlign.center,
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                ),
              ),
            ),
          )
        ],
      ),
    );

    return Scaffold(
      body: Container(
        color: Colors.yellow,
        child: Center(
          child: Container(
            margin: EdgeInsets.all(20),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.end,
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                Expanded(
                  flex: 1,
                  child: Center(
                    child: Card(
                      child: form,
                    ),
                  ),
                ),
                _getRegisterPrompt(),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _getRegisterPrompt() {
    final registerButton = Padding(
      padding: EdgeInsets.fromLTRB(20, 0, 20, 20),
      child: ElevatedButton(
        style: ButtonStyle(),
        onPressed: () => _register(),
        child: Padding(
          padding: EdgeInsets.all(10),
          child: Text(
            'Registruotis',
            textAlign: TextAlign.center,
            style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
          ),
        ),
      ),
    );

    final registerLabel = Padding(
      padding: EdgeInsets.fromLTRB(20, 0, 20, 5),
      child: Text(
        'Neturite paskyros?',
        textAlign: TextAlign.center,
        style: TextStyle(
            fontFamily: 'Roboto',
            fontWeight: FontWeight.bold,
            fontSize: 20,
            color: Colors.black,
            decoration: TextDecoration.none),
      ),
    );

    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [registerLabel, registerButton],
    );
  }

  Future<void> _login() async {
    if (!_formKey.currentState.validate()) {
      return;
    }

    final loginRequest = new LoginRequest(_usernameController.text, _passwordController.text);
    final response = await HttpRequests.post('api/Token/Authenticate', loginRequest);
    if (response.statusCode == 200) {
      //final loginResponse = LoginResponse.fromJson(jsonDecode(response.body));
      // check role.
      final preferences = await SharedPreferences.getInstance();
      preferences.setString('user', response.body);
      var token = jsonDecode(response.body)['token'] as String;
      preferences.setString('token', token);

      Navigator.of(context).pushReplacement(AnimatedRoutes.slideToRight(context, HomePage()));

      return;
    } else {
      final _errorFlushbarRoute = flushbarRoute.showFlushbar(
          context: context,
          flushbar: Flushbar(
              message: 'Neteisingas vartotojo vardas ar slaptažodis',
              flushbarPosition: FlushbarPosition.TOP,
              isDismissible: true,
              duration: Duration(seconds: 5)));

      Navigator.of(context, rootNavigator: true).push(_errorFlushbarRoute);
    }
  }

  Future<void> _register() async {
    final registerResult =
        await Navigator.of(context).push(AnimatedRoutes.slideToLeft(context, RegisterPage()));

    if (registerResult != 'registered') {
      return;
    }

    final _flushbarRoute = flushbarRoute.showFlushbar(
        context: context,
        flushbar: Flushbar(
            message: 'Registracija sėkminga',
            flushbarPosition: FlushbarPosition.TOP,
            isDismissible: true,
            duration: Duration(seconds: 5)));
    Navigator.of(context, rootNavigator: true).push(_flushbarRoute);
  }
}
