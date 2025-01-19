import 'package:dotted_border/dotted_border.dart';
import 'package:dotted_line/dotted_line.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/singleton/parameters.view.dart';
import 'package:triggo/app/features/automation/view/singleton/select_integration.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/app/theme/fonts/fonts.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationMainView extends StatefulWidget {
  final Automation? automation;

  const AutomationMainView({super.key, this.automation});

  @override
  State<AutomationMainView> createState() => _AutomationMainViewState();
}

class _AutomationMainViewState extends State<AutomationMainView> {
  @override
  void initState() {
    super.initState();

    if (widget.automation != null) {
      context
          .read<AutomationBloc>()
          .add(AutomationLoadExisting(automation: widget.automation!));
    } else {
      context.read<AutomationBloc>().add(AutomationReset());
    }
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<AutomationBloc, AutomationState>(
      builder: (context, state) {
        return BaseScaffold(
          title: 'Automation',
          header: _Header(automation: state.cleanedAutomation),
          getBack: true,
          body: Padding(
            padding: const EdgeInsets.all(4.0),
            child: Column(
              children: [
                _AutomationContainer(automation: state.cleanedAutomation),
                _SaveWidget(),
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
            behavior: HitTestBehavior.opaque,
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
          Stack(
            children: [
              IconButton(
                onPressed: () {
                  Navigator.of(context)
                      .pushNamed(RoutesNames.automationSettings);
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
              if (automation.label.isEmpty || automation.description.isEmpty)
                Positioned(
                  bottom: 4,
                  right: 8,
                  child: Icon(
                    Icons.warning_amber_rounded,
                    color: Theme.of(context).colorScheme.onError,
                    size: 20,
                  ),
                ),
            ],
          ),
        ],
      ),
    );
  }
}

class _SaveWidget extends StatelessWidget {
  const _SaveWidget();

  @override
  Widget build(BuildContext context) {
    return BlocListener<AutomationBloc, AutomationState>(
      listener: (context, state) {
        if (state.savingStatus.isSuccess) {
          Navigator.of(context).pop();
        }

        if (state.savingStatus.isFailure) {
          ScaffoldMessenger.of(context)
            ..hideCurrentSnackBar()
            ..showSnackBar(
              SnackBar(
                content: Text('Failed to save automation'),
                backgroundColor: Theme.of(context).colorScheme.onError,
              ),
            );
        }
      },
      child: _SaveButton(),
    );
  }
}

class _SaveButton extends StatelessWidget {
  const _SaveButton();

  @override
  Widget build(BuildContext context) {
    final state = context.select((AutomationBloc bloc) => bloc.state);

    if (state.savingStatus.isInProgress) {
      return Center(
        child: CircularProgressIndicator(),
      );
    }

    final automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);

    final isValid = _isSaveButtonEnabled(state, automationMediator);

    return Row(
      children: [
        Expanded(
          child: TriggoButton(
            text: 'Save',
            onPressed: isValid
                ? () async {
                    context.read<AutomationBloc>().add(AutomationSubmitted());
                  }
                : null,
            padding:
                const EdgeInsets.symmetric(horizontal: 20.0, vertical: 12.0),
            style: TextStyle(
              color: Theme.of(context).colorScheme.onPrimary,
              fontFamily: containerTitle.fontFamily,
              fontSize: 20,
              fontWeight: containerTitle.fontWeight,
              letterSpacing: containerTitle.letterSpacing,
            ),
          ),
        ),
      ],
    );
  }
}

bool _isSaveButtonEnabled(
    AutomationState state, AutomationMediator automationMediator) {
  for (final action in state.cleanedAutomation.actions) {
    if (!validateAction(state.cleanedAutomation, automationMediator,
        state.cleanedAutomation.actions.indexOf(action))) {
      return false;
    }
  }

  return state.cleanedAutomation.label.isNotEmpty &&
      state.cleanedAutomation.description.isNotEmpty &&
      state.cleanedAutomation.trigger.identifier.isNotEmpty &&
      state.cleanedAutomation.actions.isNotEmpty;
}

class _AutomationContainer extends StatelessWidget {
  final Automation automation;

  const _AutomationContainer({required this.automation});

  @override
  Widget build(BuildContext context) {
    final state = context.select((AutomationBloc bloc) => bloc.state);

    if (state.loadingStatus.isInProgress) {
      return Expanded(
        child: Center(
          child: CircularProgressIndicator(),
        ),
      );
    }

    if (state.loadingStatus.isFailure) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        ScaffoldMessenger.of(context)
          ..removeCurrentSnackBar()
          ..showSnackBar(
            SnackBar(content: Text('Failed to load automation, retrying...')),
          );
      });
      return Expanded(
        child: Center(
          child: CircularProgressIndicator(),
        ),
      );
    }

    if (automation.trigger.identifier.isEmpty) {
      return Expanded(
        child: Column(
          children: [
            _AddTriggerEventWidget(),
          ],
        ),
      );
    }
    return Expanded(
      child: Column(
        children: [
          CustomRectangleList(automation: automation),
        ],
      ),
    );
  }
}

class _AddTriggerEventWidget extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        context.read<AutomationBloc>().add(AutomationLoadCleanToDirty());
        Navigator.push(
            context,
            customScreenBuilder(AutomationSelectIntegrationView(
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

    return Expanded(
      child: SingleChildScrollView(
        child: Column(
          children: [
            _TriggerListItem(
              icon: "assets/icons/${triggerOrAction.icon}.svg",
              color: HexColor(triggerIntegration.color),
              text: triggerOrAction.name,
              type: AutomationChoiceEnum.trigger,
              indexOfTheTriggerOrAction: 0,
              integrationIdentifier: integrationIdentifier,
              triggerOrActionIdentifier: triggerOrActionIdentifier,
              isDeletable: false,
              missingParameters: false,
            ),
            if (automation.actions.isNotEmpty) SizedBox(height: 10),
            ListView.separated(
              shrinkWrap: true,
              physics: NeverScrollableScrollPhysics(),
              itemCount: automation.actions.length,
              itemBuilder: (context, index) {
                final action = automation.actions[index];
                final integrationIdentifier =
                    action.identifier.split('.').first;
                final actionIdentifier = action.identifier.split('.').last;

                final actionIntegration =
                    schema.schemas[integrationIdentifier]!;
                final actionSchema =
                    actionIntegration.actions[actionIdentifier]!;

                final missingParameters =
                    !validateAction(automation, automationMediator, index);

                return _TriggerListItem(
                  icon: "assets/icons/${actionSchema.icon}.svg",
                  color: HexColor(actionIntegration.color),
                  text: actionSchema.name,
                  type: AutomationChoiceEnum.action,
                  indexOfTheTriggerOrAction: index,
                  integrationIdentifier: integrationIdentifier,
                  triggerOrActionIdentifier: actionIdentifier,
                  isDeletable: true,
                  missingParameters: missingParameters,
                );
              },
              separatorBuilder: (context, index) {
                return const SizedBox(height: 8.0);
              },
            ),
            SizedBox(
              height: 40,
              child: DottedLine(
                direction: Axis.vertical,
                lineLength: double.infinity,
                lineThickness: 2.40,
                dashLength: 10,
                dashGapRadius: 2,
                dashColor: textPrimaryColor,
              ),
            ),
            Center(
              child: GestureDetector(
                behavior: HitTestBehavior.opaque,
                onTap: () {
                  context
                      .read<AutomationBloc>()
                      .add(AutomationLoadCleanToDirty());
                  Navigator.push(
                      context,
                      customScreenBuilder(AutomationSelectIntegrationView(
                        type: AutomationChoiceEnum.action,
                        indexOfTheTriggerOrAction: automation.actions.length,
                      )));
                },
                child: Container(
                  width: 35,
                  height: 35,
                  decoration: BoxDecoration(
                    color: Theme.of(context).colorScheme.primary,
                    borderRadius: BorderRadius.circular(8),
                  ),
                  child: Center(
                    child: SvgPicture.asset(
                      'assets/icons/plus.svg',
                      height: 16,
                      width: 16,
                      colorFilter: ColorFilter.mode(
                        Colors.white,
                        BlendMode.srcIn,
                      ),
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
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
  final bool isDeletable;
  final bool missingParameters;

  const _TriggerListItem({
    required this.icon,
    required this.color,
    required this.text,
    required this.type,
    required this.indexOfTheTriggerOrAction,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
    required this.isDeletable,
    required this.missingParameters,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        context.read<AutomationBloc>().add(AutomationLoadCleanToDirty());
        Navigator.push(
            context,
            customScreenBuilder(AutomationParametersView(
              type: type,
              integrationIdentifier: integrationIdentifier,
              triggerOrActionIdentifier: triggerOrActionIdentifier,
              indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
              isEdit: true,
            )));
      },
      onLongPress: isDeletable
          ? () {
              showDialog(
                context: context,
                builder: (context) => AlertDialog(
                  title: Text('Delete this action?'),
                  content: Text(
                      'Are you sure you want to delete this action named: $text?'),
                  actions: [
                    TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: Text('Cancel'),
                    ),
                    TextButton(
                      onPressed: () {
                        context.read<AutomationBloc>().add(
                              AutomationActionDeleted(
                                  index: indexOfTheTriggerOrAction),
                            );
                        Navigator.pop(context);
                      },
                      child: Text('Delete'),
                    ),
                  ],
                ),
              );
            }
          : null,
      child: Container(
        decoration: BoxDecoration(
          border: Border.all(color: color, width: 2),
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
            if (missingParameters)
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 5.0),
                child: Icon(
                  Icons.warning_amber_rounded,
                  color: Theme.of(context).colorScheme.onError,
                  size: 30,
                ),
              ),
          ],
        ),
      ),
    );
  }
}
