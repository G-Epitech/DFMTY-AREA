import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/models/user.model.dart';
import 'package:triggo/repositories/user/user.repository.dart';

class UserMediator with ChangeNotifier {
  final UserRepository _userRepository;

  UserMediator(this._userRepository);

  Future<User> getUser() async {
    try {
      final res = await _userRepository.getUser();
      if (res.statusCode == Codes.ok && res.data != null) {
        return User(
          firstName: res.data!.firstName,
          lastName: res.data!.lastName,
          email: res.data!.email,
          picture: res.data!.picture,
        );
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      throw Exception(e);
    }
  }
}
