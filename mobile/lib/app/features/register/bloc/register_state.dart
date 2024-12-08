part of 'register_bloc.dart';

final class RegisterState extends Equatable {
  const RegisterState({
    this.status = FormzSubmissionStatus.initial,
    this.firstName = const FirstName.pure(),
    this.lastName = const LastName.pure(),
    this.email = const Email.pure(),
    this.password = const Password.pure(),
    this.isValid = false,
  });

  final FormzSubmissionStatus status;
  final FirstName firstName;
  final LastName lastName;
  final Email email;
  final Password password;
  final bool isValid;

  RegisterState copyWith({
    FormzSubmissionStatus? status,
    FirstName? firstName,
    LastName? lastName,
    Email? email,
    Password? password,
    bool? isValid,
  }) {
    return RegisterState(
      status: status ?? this.status,
      firstName: firstName ?? this.firstName,
      lastName: lastName ?? this.lastName,
      email: email ?? this.email,
      password: password ?? this.password,
      isValid: isValid ?? this.isValid,
    );
  }

  @override
  List<Object> get props =>
      [status, email, password, firstName, lastName, isValid];
}
