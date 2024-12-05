import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:provider/provider.dart';
import 'package:triggo/app/bloc/authentication_bloc.dart';
import 'package:triggo/app/routes/generate.routes.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/theme.dart';
import 'package:triggo/mediator/authentication.mediator.dart';
import 'package:triggo/repositories/authentication.repository.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/user.repository.dart';

class MyApp extends StatefulWidget {
  const MyApp({super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
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
        child: MaterialApp(
          title: 'Triggo',
          theme: triggoTheme,
          initialRoute: RoutesNames.splashScreen,
          onGenerateRoute: generateRoute,
        ),
      ),
    );
  }
}
