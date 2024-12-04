import 'package:equatable/equatable.dart';

class User extends Equatable {
  final String firstName;
  final String lastName;
  final String email;

  const User(
      {required this.firstName, required this.lastName, required this.email});

  @override
  List<Object?> get props => [firstName, lastName, email];

  static const empty = User(firstName: '', lastName: '', email: '');
}
