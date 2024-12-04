import 'package:triggo/models/user.model.dart';

class UserRepository {
  User? _user;

  Future<User?> getUser() async {
    if (_user != null) return _user;
    return Future.delayed(
        const Duration(milliseconds: 300),
        () => _user = const User(
            firstName: "Yann",
            lastName: "Masson",
            email: "yann.masson@epitech.eu"));
  }
}
