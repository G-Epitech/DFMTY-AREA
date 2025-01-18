import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:provider/provider.dart';
import 'package:triggo/app/bloc/authentication_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/routes/generate.routes.dart';
import 'package:triggo/app/routes/route_observer.router.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/theme.dart';
import 'package:triggo/mediator/authentication.mediator.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/mediator/user.mediator.dart';
import 'package:triggo/repositories/authentication/authentication.repository.dart';
import 'package:triggo/repositories/authentication/google.repository.dart';
import 'package:triggo/repositories/automation/automation.repository.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/integration.repository.dart';
import 'package:triggo/repositories/user/user.repository.dart';

class TriggoApp extends StatefulWidget {
  const TriggoApp({super.key});

  @override
  State<TriggoApp> createState() => _TriggoAppState();
}

class _TriggoAppState extends State<TriggoApp> {
  late final AuthenticationRepository _authenticationRepository;
  late final CredentialsRepository _credentialsRepository;
  late final UserRepository _userRepository;
  late final IntegrationRepository _integrationRepository;
  late final GoogleRepository _googleRepository;
  late final AuthenticationMediator _authenticationMediator;
  late final IntegrationMediator _integrationMediator;
  late final AutomationMediator _automationMediator;
  late final AutomationRepository _automationRepository;
  late final UserMediator _userMediator;

  @override
  void initState() {
    super.initState();
    _authenticationRepository = AuthenticationRepository();
    _credentialsRepository = CredentialsRepository();
    _userRepository =
        UserRepository(credentialsRepository: _credentialsRepository);
    _automationRepository = AutomationRepository(
      credentialsRepository: _credentialsRepository,
    );
    _integrationRepository =
        IntegrationRepository(credentialsRepository: _credentialsRepository);

    _googleRepository = GoogleRepository(
      credentialsRepository: _credentialsRepository,
    );

    _authenticationMediator = AuthenticationMediator(
      _authenticationRepository,
      _credentialsRepository,
      _googleRepository,
    );
    _integrationMediator = IntegrationMediator(
      _integrationRepository,
      _automationRepository,
    );
    try {
      _automationMediator = AutomationMediator(_automationRepository);
    } catch (e) {
      ScaffoldMessenger.of(context)
        ..removeCurrentSnackBar()
        ..showSnackBar(
            SnackBar(content: Text('Error initializing Automation')));
    }
    _userMediator = UserMediator(_userRepository);
  }

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        RepositoryProvider.value(value: _authenticationRepository),
        RepositoryProvider.value(value: _credentialsRepository),
        RepositoryProvider.value(value: _userRepository),
        RepositoryProvider.value(value: _integrationRepository),
        RepositoryProvider.value(value: _googleRepository),
        ChangeNotifierProvider.value(value: _authenticationMediator),
        ChangeNotifierProvider.value(value: _integrationMediator),
        ChangeNotifierProvider.value(value: _automationMediator),
        ChangeNotifierProvider.value(value: _userMediator),
      ],
      child: MultiBlocProvider(
        providers: [
          BlocProvider(
            create: (_) => AuthenticationBloc(
              authenticationMediator: _authenticationMediator,
              userRepository: _userRepository,
            ),
          ),
          BlocProvider(
            create: (_) => AutomationBloc(
              automationMediator: _automationMediator,
              integrationMediator: _integrationMediator,
            ),
          ),
        ],
        child: MaterialApp(
          navigatorObservers: [routeObserver],
          debugShowCheckedModeBanner: false,
          title: 'Triggo',
          theme: triggoTheme,
          initialRoute: RoutesNames.splashScreen,
          onGenerateRoute: generateRoute,
        ),
      ),
    );
  }
}
