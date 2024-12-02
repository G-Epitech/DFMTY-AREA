import 'package:triggo/models/action.model.dart';
import 'package:triggo/models/reaction.model.dart';

class ActionReactionLink {
  Action action;
  List<Reaction> reactions;

  ActionReactionLink({required this.action, required this.reactions});
}

class Automation {
  String name;
  String description = '';
  String imageUrl = '';
  String iconUrl = '';
  List<ActionReactionLink> actionReactionLinks = [];

  Automation({required this.name});
}
