import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/mediator/authentication.mediator.dart';

class SplashScreen extends StatelessWidget {
  const SplashScreen({super.key});

  static Route<void> route() {
    return MaterialPageRoute<void>(builder: (_) => const SplashScreen());
  }

  @override
  Widget build(BuildContext context) {
    final authenticationMediator =
        Provider.of<AuthenticationMediator>(context, listen: false);

    return StreamBuilder<AuthenticationStatus>(
      stream: authenticationMediator.status,
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Scaffold(
            body: Center(child: CircularProgressIndicator()),
            backgroundColor: Colors.white,
          );
        }

        final status = snapshot.data;
        if (status == AuthenticationStatus.authenticated) {
          WidgetsBinding.instance.addPostFrameCallback((_) {
            Navigator.pushNamedAndRemoveUntil(
                context, RoutesNames.home, (route) => false);
          });
        } else if (status == AuthenticationStatus.unauthenticated) {
          WidgetsBinding.instance.addPostFrameCallback((_) {
            Navigator.pushNamedAndRemoveUntil(
                context, RoutesNames.login, (route) => false);
          });
        }

        return const Scaffold(
          body: Center(child: CircularProgressIndicator()),
          backgroundColor: Colors.white,
        );
      },
    );
  }
}
