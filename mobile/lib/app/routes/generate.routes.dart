import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/view/automation_parameter.view.dart';
import 'package:triggo/app/features/automation/view/automations.view.dart';
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
      return customScreenBuilder(const HomeScreen());
    case (RoutesNames.login):
      return customScreenBuilder(const LoginScreen());
    case (RoutesNames.splashScreen):
      return customScreenBuilder(const SplashScreen());
    case (RoutesNames.welcome):
      return customScreenBuilder(const WelcomeScreen());
    case (RoutesNames.register):
      return customScreenBuilder(const RegisterScreen());
    case (RoutesNames.integrations):
      return customScreenBuilder(const IntegrationScreen());
    case (RoutesNames.automations):
      return customScreenBuilder(const AutomationsScreen());
    case (RoutesNames.automationTrigger):
      return customScreenBuilder(AutomationParameterView());
    case (RoutesNames.connectIntegration):
      return customScreenBuilder(const ConnectIntegrationScreen());
    default:
      return customScreenBuilder(const Placeholder());
  }
}
