import 'package:flutter/material.dart';

PageRouteBuilder<dynamic> customScreenBuilder(Widget child,
    [RouteSettings? settings]) {
  return PageRouteBuilder(
    pageBuilder: (context, animation, secondaryAnimation) => child,
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      return FadeTransition(
        opacity: animation,
        child: child,
      );
    },
    transitionDuration: const Duration(milliseconds: 200),
    settings: settings,
  );
}
