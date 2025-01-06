import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automations/view/automation_create_page.view.dart';
import 'package:triggo/app/features/integrations/widgets/integration.widget.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationPage extends StatefulWidget {
  const AutomationPage({super.key});

  @override
  State<AutomationPage> createState() => _IntegrationPageState();
}

class _IntegrationPageState extends State<AutomationPage> {
  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    final Future<List<Automation>> automations =
        automationMediator.getUserAutomations();
    return BaseScaffold(
      title: 'Automations',
      body: _AutomationContainer(automations: automations),
    );
  }
}

class _AutomationContainer extends StatelessWidget {
  final Future<List<Automation>> automations;

  const _AutomationContainer({required this.automations});

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
        Expanded(child: _AutomationList(automations: automations)),
      ],
    );
  }
}

class _AutomationCreationButton extends StatelessWidget {
  const _AutomationCreationButton();

  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    return TriggoButton(
      text: "Create Automation",
      onPressed: () {
        automationMediator.createAutomation();
      },
    );
  }
}

class _AutomationList extends StatelessWidget {
  final Future<List<Automation>> automations;

  const _AutomationList({required this.automations});

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<Automation>>(
      future: automations,
      builder: (context, snapshot) {
        return _AutomationListView(snapshot: snapshot);
      },
    );
  }
}

class _AutomationListView extends StatelessWidget {
  final AsyncSnapshot<List<Automation>> snapshot;

  const _AutomationListView({required this.snapshot});

  @override
  Widget build(BuildContext context) {
    if (snapshot.connectionState == ConnectionState.waiting) {
      return Center(child: CircularProgressIndicator());
    } else if (snapshot.hasError) {
      return _ErrorView(error: snapshot.error!);
    } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
      return const _NoDataView();
    } else {
      return _AutomationViewContent(automations: snapshot.data!);
    }
  }
}

class _ErrorView extends StatelessWidget {
  final Object error;

  const _ErrorView({required this.error});

  @override
  Widget build(BuildContext context) {
    return Text('Error: $error');
  }
}

class _NoDataView extends StatelessWidget {
  const _NoDataView();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Text('No automations',
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _AutomationViewContent extends StatelessWidget {
  final List<Automation> automations;

  const _AutomationViewContent({required this.automations});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: automations.length,
      itemBuilder: (context, index) {
        return _AutomationListItem(automation: automations[index]);
      },
    );
  }
}

class _AutomationListItem extends StatelessWidget {
  final Automation automation;

  const _AutomationListItem({required this.automation});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (context) => CreateAutomationPage(
              automation: automation,
            ),
          ),
        );
      },
      child: IntegrationCard(
        customWidget: Row(
          children: [
            Stack(
              clipBehavior: Clip.none,
              children: [
                Container(
                  width: 60,
                  height: 60,
                  decoration: BoxDecoration(
                    color: automation.iconColor,
                    borderRadius: BorderRadius.circular(12.0),
                  ),
                  child: Center(
                    child: SvgPicture.asset(
                      automation.iconUri,
                      width: 30,
                      height: 30,
                      colorFilter: ColorFilter.mode(
                        Colors.white,
                        BlendMode.srcIn,
                      ),
                    ),
                  ),
                ),
                Positioned(
                  bottom: -5,
                  right: -5,
                  child: _ActivityIcon(state: automation.isActive),
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
                      Expanded(
                        child: Text(
                          automation.name,
                          style: Theme.of(context).textTheme.labelLarge,
                          maxLines: 1,
                          overflow: TextOverflow.ellipsis,
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 5),
                  Row(
                    children: [
                      Expanded(
                        child: Text(
                          automation.description,
                          style: Theme.of(context).textTheme.labelMedium,
                          maxLines: 2,
                          overflow: TextOverflow.ellipsis,
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}

class _ActivityIcon extends StatelessWidget {
  final bool state;
  final Color color;

  _ActivityIcon({required this.state}) : color = _getColor(state);

  static Color _getColor(bool state) {
    return state ? Colors.green : Colors.grey;
  }

  @override
  Widget build(BuildContext context) {
    return CircleAvatar(
      radius: 10,
      backgroundColor: color,
    );
  }
}
