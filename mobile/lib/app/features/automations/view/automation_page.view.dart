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
    return BaseScaffold(
      title: 'Automations',
      body: _AutomationContainer(),
    );
  }
}

class _AutomationContainer extends StatelessWidget {
  const _AutomationContainer();

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          children: [
            Expanded(
              child: Padding(
                padding:
                    const EdgeInsets.symmetric(vertical: 8.0, horizontal: 4.0),
              ),
            ),
          ],
        ),
      ],
    );
  }
}
