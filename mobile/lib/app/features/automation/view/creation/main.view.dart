import 'dart:developer';

import 'package:dotted_border/dotted_border.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation_creation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/creation/parameters.view.dart';
import 'package:triggo/app/features/automation/view/creation/select_integration.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/app/theme/fonts/fonts.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationCreationMainView extends StatefulWidget {
  const AutomationCreationMainView({super.key});

  @override
  State<AutomationCreationMainView> createState() =>
      _AutomationCreationMainViewState();
}

class _AutomationCreationMainViewState
    extends State<AutomationCreationMainView> {
  @override
  Widget build(BuildContext context) {
    context.read<AutomationCreationBloc>().add(AutomationCreationReset());

    return BlocBuilder<AutomationCreationBloc, AutomationCreationState>(
      builder: (context, state) {
        log('AutomationCreationMainView: ${state.cleanedAutomation.trigger.providers.length}');
        return BaseScaffold(
          title: 'Automation',
          header: _Header(automation: state.cleanedAutomation),
          getBack: true,
          body: Padding(
            padding: const EdgeInsets.all(0.0),
            child: Column(
              children: [
                _AutomationCreationContainer(
                    automation: state.cleanedAutomation),
                const Spacer(),
                Row(
                  children: [
                    Expanded(
                      child: _SaveButton(),
                    ),
                  ],
                ),
              ],
            ),
          ),
        );
      },
    );
  }
}

class _Header extends StatelessWidget {
  final Automation automation;

  const _Header({required this.automation});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(bottom: 10.0),
      child: Row(
        children: [
          GestureDetector(
            onTap: () {
              Navigator.of(context).pop();
            },
            child: Icon(
              Icons.arrow_back,
              size: 26.0,
              weight: 2.0,
            ),
          ),
          SizedBox(width: 10.0),
          Container(
            width: 50,
            height: 50,
            decoration: BoxDecoration(
              color: Color(
                  automation.iconColor > 0 ? automation.iconColor : 0xFFEE883A),
              borderRadius: BorderRadius.circular(12.0),
            ),
            child: Center(
              child: SvgPicture.asset(
                automation.iconUri.isEmpty
                    ? 'assets/icons/chat.svg'
                    : automation.iconUri,
                width: 25,
                height: 25,
                colorFilter: ColorFilter.mode(
                  Colors.white,
                  BlendMode.srcIn,
                ),
              ),
            ),
          ),
          SizedBox(width: 10.0),
          Expanded(
            child: Text(
              automation.label.isEmpty ? 'Untitled' : automation.label,
              style: Theme.of(context).textTheme.titleLarge!.copyWith(
                    fontSize: 20.0,
                  ),
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          IconButton(
            onPressed: () {
              log('Settings button pressed');
              Navigator.of(context).pushNamed(RoutesNames.automationSettings);
            },
            icon: SvgPicture.asset(
              'assets/icons/cog-6-tooth.svg',
              height: 24,
              width: 24,
              colorFilter: ColorFilter.mode(
                textPrimaryColor,
                BlendMode.srcIn,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class _SaveButton extends StatelessWidget {
  const _SaveButton();

  @override
  Widget build(BuildContext context) {
    return TriggoButton(
      text: 'Save',
      onPressed: () async {
        log('Save button pressed');
      },
      padding: const EdgeInsets.symmetric(horizontal: 20.0, vertical: 12.0),
      style: TextStyle(
        color: Theme.of(context).colorScheme.onPrimary,
        fontFamily: containerTitle.fontFamily,
        fontSize: 20,
        fontWeight: containerTitle.fontWeight,
        letterSpacing: containerTitle.letterSpacing,
      ),
    );
  }
}

class _AutomationCreationContainer extends StatelessWidget {
  final Automation automation;

  const _AutomationCreationContainer({required this.automation});

  @override
  Widget build(BuildContext context) {
    if (automation.trigger.identifier.isEmpty) {
      return _AddTriggerEventWidget();
    }
    return Expanded(
        child: Column(
      children: [
        CustomRectangleList(automation: automation),
      ],
    ));
  }
}

class _AddTriggerEventWidget extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        context
            .read<AutomationCreationBloc>()
            .add(AutomationCreationLoadAutomation());
        Navigator.push(
            context,
            customScreenBuilder(AutomationCreationSelectIntegrationView(
              type: AutomationChoiceEnum.trigger,
              indexOfTheTriggerOrAction: 0,
            )));
      },
      child: Row(
        children: [
          Expanded(
            child: DottedBorder(
              color: textPrimaryColor,
              strokeWidth: 3,
              dashPattern: [10],
              borderType: BorderType.RRect,
              radius: Radius.circular(8),
              child: Container(
                padding: const EdgeInsets.all(12.0),
                width: double.infinity,
                child: Center(
                  child: Text(
                    'Add a Trigger Event',
                    style: TextStyle(
                      color: textPrimaryColor,
                      fontFamily: containerTitle.fontFamily,
                      fontSize: 18,
                      fontWeight: containerTitle.fontWeight,
                      letterSpacing: containerTitle.letterSpacing,
                    ),
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class CustomRectangleList extends StatelessWidget {
  final Automation automation;

  const CustomRectangleList({super.key, required this.automation});

  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    final schema = automationMediator.automationSchemas;

    if (schema == null) {
      return Container();
    }
    final trigger = automation.trigger;
    final integrationIdentifier = trigger.identifier.split('.').first;
    final triggerOrActionIdentifier = trigger.identifier.split('.').last;

    final triggerIntegration = schema.schemas[integrationIdentifier]!;
    final triggerOrAction =
        triggerIntegration.triggers[triggerOrActionIdentifier]!;

    return Column(
      children: [
        _TriggerListItem(
          icon: "assets/icons/${triggerOrAction.icon}.svg",
          color: HexColor(triggerIntegration.color),
          text: triggerOrAction.name,
          type: AutomationChoiceEnum.trigger,
          indexOfTheTriggerOrAction: 0,
          integrationIdentifier: integrationIdentifier,
          triggerOrActionIdentifier: triggerOrActionIdentifier,
        ),
        SizedBox(height: 10),
        /*_TriggerListItem(
          icon: "assets/icons/people.svg",
          color: Color(0xFF5865F2),
          text: "React to a message",
        ),*/
      ],
    );
  }
}

class _TriggerListItem extends StatelessWidget {
  final String icon;
  final Color color;
  final String text;
  final AutomationChoiceEnum type;
  final int indexOfTheTriggerOrAction;
  final String integrationIdentifier;
  final String triggerOrActionIdentifier;

  const _TriggerListItem({
    required this.icon,
    required this.color,
    required this.text,
    required this.type,
    required this.indexOfTheTriggerOrAction,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        context
            .read<AutomationCreationBloc>()
            .add(AutomationCreationLoadAutomation());
        Navigator.push(
            context,
            customScreenBuilder(AutomationCreationParametersView(
              type: type,
              integrationIdentifier: integrationIdentifier,
              triggerOrActionIdentifier: triggerOrActionIdentifier,
              indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
              isEdit: true,
            )));
      },
      child: Container(
        decoration: BoxDecoration(
          border: Border.all(color: Color(0xFF5865F2), width: 3),
          borderRadius: BorderRadius.circular(8),
        ),
        padding: const EdgeInsets.all(10),
        child: Row(
          children: [
            Container(
              width: 40,
              height: 40,
              decoration: BoxDecoration(
                color: color,
                borderRadius: BorderRadius.circular(8),
              ),
              child: Padding(
                padding: const EdgeInsets.all(10.0),
                child: SvgPicture.asset(
                  icon,
                  width: 20,
                  height: 20,
                  colorFilter: ColorFilter.mode(
                    Colors.white,
                    BlendMode.srcIn,
                  ),
                ),
              ),
            ),
            SizedBox(width: 10),
            Expanded(
              child: Text(
                text,
                style: Theme.of(context).textTheme.labelLarge?.copyWith(
                      color: Colors.black87,
                    ),
                maxLines: 1,
                overflow: TextOverflow.ellipsis,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
