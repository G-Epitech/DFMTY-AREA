import 'dart:async';

import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/integration/leagueOfLegends.repository.dart';

enum LinkStatus {
  unknown,
  linked,
  linking,
  notLinked,
}

class LeagueOfLegendsMediator with ChangeNotifier {
  final LeagueOfLegendsRepository _leagueOfLegendsRepository;

  LeagueOfLegendsMediator(this._leagueOfLegendsRepository);

  final _controller = StreamController<LinkStatus>();

  Stream<LinkStatus> get status async* {
    await Future<void>.delayed(const Duration(seconds: 1));
    yield* _controller.stream;
  }

  Future<void> linkAccount(String gameName, String tagLine) async {
    try {
      _controller.add(LinkStatus.linking);
      final res =
          await _leagueOfLegendsRepository.linkAccount(gameName, tagLine);

      if (res.statusCode == Codes.created) {
        _controller.add(LinkStatus.linked);
      } else {
        _controller.add(LinkStatus.notLinked);
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      _controller.add(LinkStatus.notLinked);
      rethrow;
    }
  }
}
