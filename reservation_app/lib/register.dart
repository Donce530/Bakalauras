import 'package:flutter/material.dart';
import 'package:reservation_app/Models/register/register_request.dart';
import 'package:reservation_app/services/http_requests.dart';
import 'package:another_flushbar/flushbar.dart';
import 'package:another_flushbar/flushbar_route.dart' as flushbarRoute;

class RegisterPage extends StatefulWidget {
  @override
  _RegisterPageState createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  final _formKey = GlobalKey<FormState>();
  final _firstNameController = TextEditingController();
  final _lastNameController = TextEditingController();
  final _phoneNumberController = TextEditingController();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    final _focusScope = FocusScope.of(context);

    final form = Form(
      key: _formKey,
      child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          mainAxisSize: MainAxisSize.min,
          children: [
            Padding(
              padding: EdgeInsets.only(top: 20, left: 20, right: 20),
              child: Text(
                'Registracija',
                textAlign: TextAlign.center,
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 40),
              ),
            ),
            Container(
              // height: 400,
              child: Column(children: [
                Padding(
                  padding: EdgeInsets.fromLTRB(20, 0, 20, 10),
                  child: TextFormField(
                    decoration: const InputDecoration(labelText: 'Vardas'),
                    controller: _firstNameController,
                    validator: _validateRequired,
                    textInputAction: TextInputAction.next,
                    onEditingComplete: () => _focusScope.nextFocus(),
                  ),
                ),
                Padding(
                  padding: EdgeInsets.fromLTRB(20, 0, 20, 10),
                  child: TextFormField(
                    decoration: const InputDecoration(labelText: 'Pavardė'),
                    controller: _lastNameController,
                    validator: _validateRequired,
                    textInputAction: TextInputAction.next,
                    onEditingComplete: () => _focusScope.nextFocus(),
                  ),
                ),
                Padding(
                  padding: EdgeInsets.fromLTRB(20, 0, 20, 10),
                  child: TextFormField(
                    decoration:
                        const InputDecoration(labelText: 'Telefono numeris'),
                    controller: _phoneNumberController,
                    validator: _validatePhoneNumber,
                    textInputAction: TextInputAction.next,
                    onEditingComplete: () => _focusScope.nextFocus(),
                  ),
                ),
                Padding(
                  padding: EdgeInsets.fromLTRB(20, 0, 20, 10),
                  child: TextFormField(
                    decoration:
                        const InputDecoration(labelText: 'El. pašto adresas'),
                    controller: _emailController,
                    validator: _validateEmail,
                    textInputAction: TextInputAction.next,
                    onEditingComplete: () => _focusScope.nextFocus(),
                  ),
                ),
                Padding(
                  padding: EdgeInsets.fromLTRB(20, 0, 20, 20),
                  child: TextFormField(
                    decoration: const InputDecoration(labelText: 'Slaptažodis'),
                    controller: _passwordController,
                    validator: _validateRequired,
                    obscureText: true,
                    textInputAction: TextInputAction.done,
                    onEditingComplete: _focusScope.unfocus,
                  ),
                ),
              ]),
            ),
            Padding(
              padding: EdgeInsets.fromLTRB(20, 0, 20, 20),
              child: ElevatedButton(
                style: ButtonStyle(),
                onPressed: _register,
                child: Padding(
                  padding: EdgeInsets.all(10),
                  child: Text(
                    'Registruotis',
                    textAlign: TextAlign.center,
                    style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                  ),
                ),
              ),
            )
          ]),
    );

    return Scaffold(
      body: Container(
        color: Colors.yellow,
        child: Center(
          child: Container(
            margin: EdgeInsets.all(20),
            child: Card(
              child: form,
            ),
          ),
        ),
      ),
    );
  }

  String _validateRequired(String value) {
    return value != null && value.isEmpty ? 'Būtina užpildyti' : null;
  }

  String _validateEmail(String value) {
    var result = _validateRequired(value);
    if (result == null) {
      var emailValid = RegExp(
              r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+")
          .hasMatch(value);

      if (!emailValid) {
        result = 'Neteisingas el. pašto adresas';
      }
    }

    return result;
  }

  String _validatePhoneNumber(String value) {
    var result = _validateRequired(value);
    if (result == null) {
      var emailValid = RegExp(r"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
          .hasMatch(value);

      if (!emailValid) {
        result = 'Neteisingas telefono numeris';
      }
    }

    return result;
  }

  Future<void> _register() async {
    if (!_formKey.currentState.validate()) {
      return;
    }

    final request = RegisterRequest(
        _firstNameController.text,
        _lastNameController.text,
        _phoneNumberController.text,
        _emailController.text,
        _passwordController.text);

    final response = await HttpRequests.post('api/User/Register', request);

    if (response.statusCode != 200) {
      final toastMessage = 'El. pašto adresas jau naudojamas';
      final _flushbarRoute = flushbarRoute.showFlushbar(
          context: context,
          flushbar: Flushbar(
              message: toastMessage,
              flushbarPosition: FlushbarPosition.TOP,
              isDismissible: true,
              duration: Duration(seconds: 5)));
      Navigator.of(context, rootNavigator: true).push(_flushbarRoute);
    } else {
      Navigator.of(context).pop('registered');
    }
  }
}
