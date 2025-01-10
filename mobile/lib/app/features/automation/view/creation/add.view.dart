import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationCreationAddView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationName;

  const AutomationCreationAddView({
    super.key,
    required this.type,
    required this.integrationName,
  });

  @override
  State<AutomationCreationAddView> createState() =>
      _AutomationCreationAddViewState();
}

class _AutomationCreationAddViewState extends State<AutomationCreationAddView> {
  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    final List<AutomationSchemaTriggerAction> triggersOrActions =
        automationMediator.getTriggersOrAction(
            widget.integrationName, widget.type);
    return BaseScaffold(
      title:
          'Add a${widget.type == AutomationChoiceEnum.trigger ? ' Trigger' : 'n Action'}',
      getBack: true,
      body: _List(triggersOrActions: triggersOrActions),
    );
  }
}

class _List extends StatelessWidget {
  final List<AutomationSchemaTriggerAction> triggersOrActions;

  const _List({required this.triggersOrActions});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: triggersOrActions.length,
      itemBuilder: (context, index) {
        final triggerOrAction = triggersOrActions[index];
        return GestureDetector(
          onTap: () {
            // Navigate to the next screen
          },
          child: TriggoCard(
            customWidget: Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Container(
                  padding: const EdgeInsets.all(6),
                  decoration: BoxDecoration(
                    color: Theme.of(context).colorScheme.primary,
                    borderRadius: BorderRadius.circular(8),
                  ),
                  child: SvgPicture.asset(
                    "assets/icons/${triggerOrAction.icon}.svg",
                    width: 24,
                    height: 24,
                    colorFilter: ColorFilter.mode(
                        Theme.of(context).colorScheme.onPrimary,
                        BlendMode.srcIn),
                  ),
                ),
                const SizedBox(width: 10),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        triggerOrAction.name,
                        style: Theme.of(context).textTheme.labelLarge,
                        overflow: TextOverflow.ellipsis,
                      ),
                      const SizedBox(height: 4),
                      Text(
                        triggerOrAction.description,
                        style: Theme.of(context).textTheme.labelMedium,
                        overflow: TextOverflow.clip,
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        );
      },
    );
  }
}
