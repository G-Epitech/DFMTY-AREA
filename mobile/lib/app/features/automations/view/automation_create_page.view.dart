import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class CreateAutomationPage extends StatefulWidget {
  const CreateAutomationPage({super.key});

  @override
  State<CreateAutomationPage> createState() => _IntegrationPageState();
}

class _IntegrationPageState extends State<CreateAutomationPage> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: 'Create Automation',
      body: _AutomationContainer(),
      getBack: true,
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
