import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationPage extends StatefulWidget {
  const AutomationPage({super.key});

  @override
  State<AutomationPage> createState() => _IntegrationPageState();
}

class _IntegrationPageState extends State<AutomationPage> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(body: Center(child: Text("Automation Page")));
  }
}
