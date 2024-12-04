import 'package:flutter/material.dart';
import 'package:triggo/app/features/home/home.dart';

Route<dynamic> generateRoute(RouteSettings settings) {
  switch (settings.name) {
    case '/home':
      return MaterialPageRoute(
          builder: (_) => const MyHomePage(title: 'Home Page'));
    default:
      return MaterialPageRoute(builder: (_) => const Placeholder());
  }
}
