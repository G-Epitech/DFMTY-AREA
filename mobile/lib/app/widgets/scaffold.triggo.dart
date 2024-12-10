import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';

class BaseScaffold extends StatelessWidget {
  final Widget body;
  final AppBar? appBar;

  const BaseScaffold({super.key, required this.body, this.appBar});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: appBar,
      body: body,
      bottomNavigationBar: const TriggoNavigationBar(),
    );
  }
}
