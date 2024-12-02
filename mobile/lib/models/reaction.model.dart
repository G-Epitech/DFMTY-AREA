class Reaction {
  final String title;
  String description = '';
  String imageUrl = '';
  String url = '';
  List<String> parameters = []; // Don't know yet if it'll be useful
  List<String> outputs = []; // Don't know yet if it'll be useful

  Reaction({required this.title});
}
