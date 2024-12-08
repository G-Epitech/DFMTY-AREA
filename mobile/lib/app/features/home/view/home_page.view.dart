import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  final String title = 'Home Screen';

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      body: Center(
        child: Text(
          "Home Page",
          style: Theme.of(context).textTheme.titleLarge,
        ),
      ),
    );
  }
}
