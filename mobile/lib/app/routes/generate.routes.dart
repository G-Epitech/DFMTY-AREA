import 'package:flutter/material.dart';
import 'package:triggo/app/features/automations/view/automation_create_page.view.dart';
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
      return customPageRouteBuilder(const HomeScreen());
    case (RoutesNames.login):
      return customPageRouteBuilder(const LoginScreen());
    case (RoutesNames.splashScreen):
      return customPageRouteBuilder(const SplashScreen());
    case (RoutesNames.welcome):
      return customPageRouteBuilder(const WelcomeScreen());
    case (RoutesNames.register):
      return customPageRouteBuilder(const RegisterScreen());
    case (RoutesNames.integrations):
      return customPageRouteBuilder(const IntegrationPage());
    case (RoutesNames.automations):
      return customPageRouteBuilder(const AutomationPage());
    case (RoutesNames.connectIntegration):
      return customPageRouteBuilder(const ConnectIntegrationScreen());
    case (RoutesNames.createAutomation):
      return customPageRouteBuilder(const CreateAutomationPage());
    default:
      return customPageRouteBuilder(const Placeholder());
  }
}
