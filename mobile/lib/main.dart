import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:triggo/app/app.dart';

void main() {
  runApp(const MyApp());
  SystemChrome.setPreferredOrientations(
      [DeviceOrientation.portraitUp, DeviceOrientation.portraitDown]);
}
