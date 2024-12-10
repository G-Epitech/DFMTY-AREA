import 'package:flutter/material.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationPage extends StatefulWidget {
  const AutomationPage({super.key});

  @override
  State<AutomationPage> createState() => _IntegrationPageState();
}

class _IntegrationPageState extends State<AutomationPage> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(body: Center(child: _AutomationTMPButton()));
  }
}

class _AutomationTMPButton extends StatelessWidget {
  const _AutomationTMPButton();

  @override
  Widget build(BuildContext context) {
    return TriggoButton(
      text: 'New integration',
      onPressed: () {
        Navigator.pushNamed(context, RoutesNames.automationTrigger);
      },
    );
  }
}
