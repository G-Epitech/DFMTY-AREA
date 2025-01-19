import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/singleton/parameters.view.dart';
import 'package:triggo/app/features/automation/view/singleton/select_integration_account.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationAddView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;

  const AutomationAddView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  State<AutomationAddView> createState() => _AutomationAddViewState();
}

class _AutomationAddViewState extends State<AutomationAddView> {
  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    final Map<String, AutomationSchemaTriggerAction> triggersOrActions =
        automationMediator.getTriggersOrActions(
            widget.integrationIdentifier, widget.type);
    return BaseScaffold(
      title:
          'Add a${widget.type == AutomationChoiceEnum.trigger ? ' Trigger' : 'n Action'}',
      getBack: true,
      body: _List(
        triggersOrActions: triggersOrActions,
        integrationIdentifier: widget.integrationIdentifier,
        type: widget.type,
        indexOfTheTriggerOrAction: widget.indexOfTheTriggerOrAction,
      ),
    );
  }
}

class _List extends StatelessWidget {
  final Map<String, AutomationSchemaTriggerAction> triggersOrActions;
  final String integrationIdentifier;
  final AutomationChoiceEnum type;
  final int indexOfTheTriggerOrAction;

  const _List({
    required this.triggersOrActions,
    required this.integrationIdentifier,
    required this.type,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);

    return BlocBuilder<AutomationBloc, AutomationState>(
        builder: (context, state) {
      return ListView.builder(
        itemCount: triggersOrActions.length,
        itemBuilder: (context, index) {
          final triggerOrActionIdentifier =
              triggersOrActions.keys.elementAt(index);
          final triggerOrAction = triggersOrActions[triggerOrActionIdentifier]!;
          return GestureDetector(
            behavior: HitTestBehavior.opaque,
            onTap: () {
              if (type == AutomationChoiceEnum.trigger) {
                context.read<AutomationBloc>().add(
                    AutomationTriggerIdentifierChanged(
                        identifier:
                            "$integrationIdentifier.$triggerOrActionIdentifier"));
              } else {
                context.read<AutomationBloc>().add(
                    AutomationActionIdentifierChanged(
                        index: indexOfTheTriggerOrAction,
                        identifier:
                            "$integrationIdentifier.$triggerOrActionIdentifier"));
              }
              final dependencies = automationMediator.getDependencies(
                  integrationIdentifier, type, triggerOrActionIdentifier);
              if (dependencies.isEmpty) {
                Navigator.push(
                    context,
                    customScreenBuilder(AutomationParametersView(
                      type: type,
                      integrationIdentifier: integrationIdentifier,
                      triggerOrActionIdentifier: triggerOrActionIdentifier,
                      indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
                    )));
              } else {
                Navigator.push(
                    context,
                    customScreenBuilder(AutomationSelectIntegrationsAccountView(
                      type: type,
                      integrationIdentifier: integrationIdentifier,
                      triggerOrActionIdentifier: triggerOrActionIdentifier,
                      indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
                      dependencies: dependencies,
                    )));
              }
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
    });
  }
}
