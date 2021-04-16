import 'package:flutter/material.dart';

class QrScanConfirmPage extends StatefulWidget {
  final String message;

  const QrScanConfirmPage({Key key, this.message}) : super(key: key);

  @override
  _QrScanConfirmPageState createState() => _QrScanConfirmPageState();
}

class _QrScanConfirmPageState extends State<QrScanConfirmPage> {
  @override
  void initState() {
    Future.delayed(Duration(seconds: 2), () {
      Navigator.of(context, rootNavigator: true).popUntil((route) => route.isFirst);
    });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.yellow,
      body: Center(
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
                widget.message,
                textAlign: TextAlign.center,
                style: TextStyle(fontSize: 50, fontWeight: FontWeight.bold),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
