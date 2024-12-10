import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/view/automation_page.view.dart';
import 'package:triggo/app/features/automations/view/automation_page.view.dart';
import 'package:triggo/app/features/home/home.dart';
import 'package:triggo/app/features/integrations/view/integration_connect_page.view.dart';
import 'package:triggo/app/features/integrations/view/integration_page.view.dart';
import 'package:triggo/app/features/login/view/login_screen.dart';
import 'package:triggo/app/features/register/view/register_screen.dart';
import 'package:triggo/app/features/splash/view/splash_screen.dart';
import 'package:triggo/app/features/welcome/view/welcome.screen.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/routes/routes_names.dart';

Route<dynamic> generateRoute(RouteSettings settings) {
  switch (settings.name) {
    case (RoutesNames.home):
      return CustomPageRouteBuilder(const HomeScreen());
    case (RoutesNames.login):
      return CustomPageRouteBuilder(const LoginScreen());
    case (RoutesNames.splashScreen):
      return CustomPageRouteBuilder(const SplashScreen());
    case (RoutesNames.welcome):
      return CustomPageRouteBuilder(const WelcomeScreen());
    case (RoutesNames.register):
      return CustomPageRouteBuilder(const RegisterScreen());
    case (RoutesNames.integrations):
      return CustomPageRouteBuilder(const IntegrationPage());
    case (RoutesNames.automations):
      return CustomPageRouteBuilder(const AutomationPage());
    case (RoutesNames.automationTrigger):
      return CustomPageRouteBuilder(AutomationScreen());
    case (RoutesNames.connectIntegration):
      return CustomPageRouteBuilder(const ConnectIntegrationScreen());
    default:
      return CustomPageRouteBuilder(const Placeholder());
  }
}
