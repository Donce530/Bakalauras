import 'package:flutter/material.dart';

class AnimatedRoutes {
  static Route slideToLeft(BuildContext context, Widget page) =>
      _getSlideTransition(context, page, Offset(-1.0, 0.0));

  static Route slideToRight(BuildContext context, Widget page) =>
      _getSlideTransition(context, page, Offset(1.0, 0.0));

  static Route _getSlideTransition(
      BuildContext context, Widget page, Offset offset) {
    return PageRouteBuilder(
        pageBuilder: (context, animation, secondaryAnimation) => page,
        transitionsBuilder: (context, animation, secondaryAnimation, child) {
          var begin = offset;
          var end = Offset.zero;

          var curve = Curves.ease;
          var curveTween = CurveTween(curve: curve);

          var tween = Tween(begin: begin, end: end).chain(curveTween);

          return SlideTransition(
            position: animation.drive(tween),
            child: child,
          );
        });
  }
}
