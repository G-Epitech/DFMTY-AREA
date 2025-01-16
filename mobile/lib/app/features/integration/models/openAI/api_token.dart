import 'package:formz/formz.dart';

enum ApiTokenValidationError { empty }

class ApiToken extends FormzInput<String, ApiTokenValidationError> {
  const ApiToken.pure() : super.pure('');
  const ApiToken.dirty([super.value = '']) : super.dirty();

  @override
  ApiTokenValidationError? validator(String value) {
    if (value.isEmpty) return ApiTokenValidationError.empty;
    return null;
  }
}
