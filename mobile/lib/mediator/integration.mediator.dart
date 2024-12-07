import 'package:flutter/material.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/integration.repository.dart';

class IntegrationMediator with ChangeNotifier {
  final IntegrationRepository _integrationRepository;
  final CredentialsRepository _credentialsRepository;

  IntegrationMediator(this._integrationRepository, this._credentialsRepository);
}
