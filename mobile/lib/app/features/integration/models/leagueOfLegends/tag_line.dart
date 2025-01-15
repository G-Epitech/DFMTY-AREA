import 'package:formz/formz.dart';

enum TagLineValidationError { empty, invalid }

class TagLine extends FormzInput<String, TagLineValidationError> {
  const TagLine.pure() : super.pure('');
  const TagLine.dirty([super.value = '']) : super.dirty();

  @override
  TagLineValidationError? validator(String value) {
    if (value.isEmpty) return TagLineValidationError.empty;
    final tagLineRegex = RegExp(r'^[a-zA-Z0-9]{3,5}$');
    if (!tagLineRegex.hasMatch(value)) return TagLineValidationError.invalid;
    return null;
  }
}
