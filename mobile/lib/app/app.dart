import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:provider/provider.dart';
import 'package:triggo/app/bloc/authentication_bloc.dart';
import 'package:triggo/app/features/home/view/home_page.dart';
import 'package:triggo/app/features/login/login.dart';
import 'package:triggo/app/features/splash/splash.dart';
import 'package:triggo/app/theme/theme.dart';
import 'package:triggo/mediator/authentication.mediator.dart';
import 'package:triggo/repositories/authentication.repository.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/user.repository.dart';

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: triggoTheme,
      home: const MyHomePage(title: 'Triggo Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  late final AuthenticationRepository _authenticationRepository;
  late final CredentialsRepository _credentialsRepository;
  late final UserRepository _userRepository;
  late final AuthenticationMediator _authenticationMediator;

  @override
  void initState() {
    super.initState();
    _authenticationRepository = AuthenticationRepository();
    _credentialsRepository = CredentialsRepository();
    _userRepository = UserRepository();

    _authenticationMediator = AuthenticationMediator(
      _authenticationRepository,
      _credentialsRepository,
    );
  }

  @override
  void dispose() {
    _authenticationMediator.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        RepositoryProvider.value(value: _authenticationRepository),
        RepositoryProvider.value(value: _credentialsRepository),
        RepositoryProvider.value(value: _userRepository),
        ChangeNotifierProvider.value(value: _authenticationMediator),
      ],
      child: BlocProvider(
        create: (_) => AuthenticationBloc(
          authenticationMediator: _authenticationMediator,
          userRepository: _userRepository,
        )..add(AuthenticationSubscriptionRequested()),
        child: const AppView(),
      ),
    );
  }
}

class AppView extends StatefulWidget {
  const AppView({super.key});

  @override
  State<AppView> createState() => _AppViewState();
}

class _AppViewState extends State<AppView> {
  final _navigatorKey = GlobalKey<NavigatorState>();

  NavigatorState get _navigator => _navigatorKey.currentState!;

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      navigatorKey: _navigatorKey,
      builder: (context, child) {
        return BlocListener<AuthenticationBloc, AuthenticationState>(
          listener: (context, state) {
            switch (state.status) {
              case AuthenticationStatus.authenticated:
                print('Authenticated');
                _navigator.pushAndRemoveUntil<void>(
                  HomePage.route(),
                  (route) => false,
                );
              case AuthenticationStatus.unauthenticated:
                print('Unauthenticated');
                _navigator.pushAndRemoveUntil<void>(
                  LoginPage.route(),
                  (route) => false,
                );
              case AuthenticationStatus.unknown:
                print('Unknown');
                break;
              case AuthenticationStatus.authenticating:
                print('Authenticating');
                break;
            }
          },
          child: child,
        );
      },
      onGenerateRoute: (_) => SplashPage.route(),
    );
  }
}
