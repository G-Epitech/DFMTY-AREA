import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/view/automations.view.dart';
import 'package:triggo/app/features/automation/view/creation/main.view.dart';
import 'package:triggo/app/features/home/home.dart';
import 'package:triggo/app/features/integration/view/integration_connect.view.dart';
import 'package:triggo/app/features/integration/view/integrations.view.dart';
import 'package:triggo/app/features/login/view/login.view.dart';
import 'package:triggo/app/features/register/view/register.view.dart';
import 'package:triggo/app/features/splash/view/splash.view.dart';
import 'package:triggo/app/features/welcome/view/welcome.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/routes/routes_names.dart';

Route<dynamic> generateRoute(RouteSettings settings) {
  switch (settings.name) {
    case (RoutesNames.home):
      return customScreenBuilder(const HomeView());
    case (RoutesNames.login):
      return customScreenBuilder(const LoginView());
    case (RoutesNames.splashScreen):
      return customScreenBuilder(const SplashView());
    case (RoutesNames.welcome):
      return customScreenBuilder(const WelcomeView());
    case (RoutesNames.register):
      return customScreenBuilder(const RegisterView());
    case (RoutesNames.integrations):
      return customScreenBuilder(const IntegrationsView());
    case (RoutesNames.automations):
      return customScreenBuilder(const AutomationsView());
    case (RoutesNames.connectIntegration):
      return customScreenBuilder(const IntegrationConnectView());
    case (RoutesNames.automationCreation):
      return customScreenBuilder(const AutomationCreationMainView());
    default:
      return customScreenBuilder(const Placeholder());
  }
}
