import 'package:formz/formz.dart';

enum GameNameValidationError { empty }

class GameName extends FormzInput<String, GameNameValidationError> {
  const GameName.pure() : super.pure('');
  const GameName.dirty([super.value = '']) : super.dirty();

  @override
  GameNameValidationError? validator(String value) {
    if (value.isEmpty) return GameNameValidationError.empty;
    return null;
  }
}
