import 'package:formz/formz.dart';

enum AdminApiTokenValidationError { empty }

class AdminApiToken extends FormzInput<String, AdminApiTokenValidationError> {
  const AdminApiToken.pure() : super.pure('');
  const AdminApiToken.dirty([super.value = '']) : super.dirty();

  @override
  AdminApiTokenValidationError? validator(String value) {
    if (value.isEmpty) return AdminApiTokenValidationError.empty;
    return null;
  }
}
