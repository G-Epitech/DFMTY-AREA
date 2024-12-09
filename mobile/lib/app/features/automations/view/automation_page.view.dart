import 'package:flutter/material.dart';
import 'package:triggo/app/features/integrations/widgets/integration.widget.dart';
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
                child: _AutomationCreationButton(),
              ),
            ),
          ],
        ),
        Expanded(child: _AutomationList()),
      ],
    );
  }
}

class _AutomationCreationButton extends StatelessWidget {
  const _AutomationCreationButton();

  @override
  Widget build(BuildContext context) {
    return TriggoButton(
      text: "Create Automation",
      onPressed: () {
        Navigator.pushNamed(context, RoutesNames.createAutomation);
      },
    );
  }
}

class _AutomationList extends StatelessWidget {
  const _AutomationList();

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: 20, // Need to be changed to the automation length
      itemBuilder: (context, index) {
        return _AutomationListItem(); // Need to be changed to AutomationCard
      },
    );
  }
}

class _AutomationListItem extends StatelessWidget {
  const _AutomationListItem();

  @override
  Widget build(BuildContext context) {
    return IntegrationCard(
        customWidget: Row(
      children: [
        Stack(
          clipBehavior: Clip.none,
          children: [
            ClipRRect(
              borderRadius: BorderRadius.circular(12.0),
              child: Image.network(
                "https://via.placeholder.com/150",
                width: 60,
                height: 60,
                fit: BoxFit.cover,
              ),
            ),
            Positioned(
              bottom: -5,
              right: -5,
              child: _ActivityIcon(state: "active"),
            )
          ],
        ),
        SizedBox(width: 10),
        Expanded(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Row(
                children: [
                  Text("Reply \"feur\" to \"Quoi\"",
                      style: Theme.of(context).textTheme.labelLarge),
                ],
              ),
              SizedBox(height: 5),
              Row(
                children: [
                  Text(
                    "Last update: Monday 18:24",
                    style: Theme.of(context).textTheme.labelMedium,
                  ),
                ],
              ),
            ],
          ),
        )
      ],
    ));
  }
}

class _ActivityIcon extends StatelessWidget {
  final String state;
  final Color color;
  _ActivityIcon({required this.state}) : color = _getColor(state);

  static Color _getColor(String state) {
    switch (state) {
      case "active":
        return Colors.green;
      default:
        return Colors.grey;
    }
  }

  @override
  Widget build(BuildContext context) {
    return CircleAvatar(
      radius: 10,
      backgroundColor: color,
    );
  }
}
