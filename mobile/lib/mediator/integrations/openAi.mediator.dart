import 'dart:async';

import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/integration/openAI.repository.dart';

enum LinkStatus {
  unknown,
  linked,
  linking,
  notLinked,
}

class OpenAIMediator with ChangeNotifier {
  final OpenAIRepository _openAIRepository;

  OpenAIMediator(this._openAIRepository);

  final _controller = StreamController<LinkStatus>();

  Stream<LinkStatus> get status async* {
    await Future<void>.delayed(const Duration(seconds: 1));
    yield* _controller.stream;
  }

  Future<void> linkAccount(String apiToken, String adminApiToken) async {
    try {
      _controller.add(LinkStatus.linking);
      final res = await _openAIRepository.linkAccount(apiToken, adminApiToken);

      if (res.statusCode == Codes.created) {
        _controller.add(LinkStatus.linked);
      } else {
        _controller.add(LinkStatus.notLinked);
        throw Exception(res.message);
      }
    } catch (e) {
      _controller.add(LinkStatus.notLinked);
      rethrow;
    }
  }
}
