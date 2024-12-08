import 'package:flutter/material.dart';
import 'package:triggo/app/features/home/home.dart';
import 'package:triggo/app/features/login/view/login_screen.dart';
import 'package:triggo/app/features/register/view/register_screen.dart';
import 'package:triggo/app/features/splash/view/splash_screen.dart';
import 'package:triggo/app/features/welcome/view/welcome.screen.dart';
import 'package:triggo/app/routes/routes_names.dart';

Route<dynamic> generateRoute(RouteSettings settings) {
  switch (settings.name) {
    case (RoutesNames.home):
      return MaterialPageRoute(builder: (_) => const HomeScreen());
    case (RoutesNames.login):
      return MaterialPageRoute(builder: (_) => const LoginScreen());
    case (RoutesNames.splashScreen):
      return MaterialPageRoute(builder: (_) => const SplashScreen());
    case (RoutesNames.welcome):
      return MaterialPageRoute(builder: (_) => const WelcomeScreen());
    case (RoutesNames.register):
      return MaterialPageRoute(builder: (_) => const RegisterScreen());
    default:
      return MaterialPageRoute(builder: (_) => const Placeholder());
  }
}
