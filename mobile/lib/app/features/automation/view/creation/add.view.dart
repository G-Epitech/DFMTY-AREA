import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation_creation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/creation/parameters.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
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
    final Map<String, AutomationSchemaTriggerAction> triggersOrActions =
        automationMediator.getTriggersOrAction(
            widget.integrationName, widget.type);
    log("Triggers or actions: $triggersOrActions");
    log("Integration name: ${widget.integrationName}");
    context.read<AutomationCreationBloc>().add(
        AutomationCreationTriggerProviderChanged(
            triggerName: widget.integrationName));
    return BaseScaffold(
      title:
          'Add a${widget.type == AutomationChoiceEnum.trigger ? ' Trigger' : 'n Action'}',
      getBack: true,
      body: _List(
        triggersOrActions: triggersOrActions,
        integrationName: widget.integrationName,
        type: widget.type,
      ),
    );
  }
}

class _List extends StatelessWidget {
  final Map<String, AutomationSchemaTriggerAction> triggersOrActions;
  final String integrationName;
  final AutomationChoiceEnum type;

  const _List({
    required this.triggersOrActions,
    required this.integrationName,
    required this.type,
  });

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<AutomationCreationBloc, AutomationCreationState>(
        builder: (context, state) {
      return ListView.builder(
        itemCount: triggersOrActions.length,
        itemBuilder: (context, index) {
          final key = triggersOrActions.keys.elementAt(index);
          final triggerOrAction = triggersOrActions[key]!;
          return GestureDetector(
            onTap: () {
              // Navigate to the next screen
              log("Go to parameters screen");
              final index = type == AutomationChoiceEnum.trigger
                  ? 0
                  : state.automation.actions.length;
              if (type == AutomationChoiceEnum.trigger) {
                context.read<AutomationCreationBloc>().add(
                    AutomationCreationTriggerIdentifierChanged(
                        identifier: key));
              }
              Navigator.push(
                  context,
                  customScreenBuilder(AutomationCreationParametersView(
                    type: type,
                    integrationIdentifier: integrationName,
                    triggerOrActionIdentifier: key,
                    indexOfTheTriggerOrAction: index,
                  )));
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
