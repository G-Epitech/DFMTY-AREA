import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';

class BaseScaffold extends StatelessWidget {
  final Widget body;

  const BaseScaffold({super.key, required this.body});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: body,
      bottomNavigationBar: const TriggoNavigationBar(),
    );
  }
}
