import 'package:flutter/material.dart';

class IntegrationPage extends StatefulWidget {
  const IntegrationPage({super.key, required this.title});

  final String title;

  @override
  State<IntegrationPage> createState() => _IntegrationPageState();
}

class _IntegrationPageState extends State<IntegrationPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        title: Text(widget.title),
      ),
      body: Center(
        child: Text('No connected integrations',
            style: Theme.of(context).textTheme.labelLarge),
      ),
    );
  }
}
