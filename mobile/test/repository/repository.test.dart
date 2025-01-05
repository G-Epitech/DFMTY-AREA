import 'authentication.test.dart';
import 'automation.test.dart';
import 'credentials.test.dart';
import 'integration.test.dart';
import 'user.test.dart';

void repositoryTests() {
  authRepositoryTests();
  credentialsRepositoryTests();
  integrationRepositoryTests();
  userRepositoryTests();
  automationRepositoryTests();
}
