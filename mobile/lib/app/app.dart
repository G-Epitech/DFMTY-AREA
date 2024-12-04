import 'package:flutter/material.dart';
import 'package:triggo/app/routes/generate.routes.dart';
import 'package:triggo/app/theme/theme.dart';

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: triggoTheme,
      initialRoute: '/home',
      onGenerateRoute: generateRoute,
    );
  }
}
