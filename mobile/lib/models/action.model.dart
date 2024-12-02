class Action {
  final String title;
  String description = '';
  String imageUrl = '';
  String url = '';
  List<String> parameters = []; // Don't know yet if it'll be useful

  Action({required this.title});
}
