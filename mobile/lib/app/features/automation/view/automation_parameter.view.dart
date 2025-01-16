import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation_trigger_bloc.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/features/automation/widgets/fields.widget.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';

class AutomationParameterView extends StatelessWidget {
  final List<TriggerPropertiesFields> initialFields = [
    TriggerPropertiesFields(
      name: 'List Guilds',
      label: 'Guild',
      icon: 'assets/icons/people.svg',
      values: [
        TriggerPropertiesFieldsValues(
          name: 'Guild 1',
          id: 'id1',
        ),
        TriggerPropertiesFieldsValues(
          name: 'Guild 2',
          id: 'id2',
        ),
      ],
    ),
    TriggerPropertiesFields(
      name: 'List Channels',
      label: 'Channel',
      icon: 'assets/icons/chat.svg',
      values: [
        TriggerPropertiesFieldsValues(
          name: 'Channel 1',
          id: 'id1',
        ),
        TriggerPropertiesFieldsValues(
          name: 'Channel 2',
          id: 'id2',
        ),
      ],
    ),
  ];

  AutomationParameterView({super.key});

  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);

    if (automationMediator.automationSchemas != null &&
        automationMediator.automationSchemas!.schemas["discord"] != null) {
      log(automationMediator.automationSchemas!.schemas["discord"]!.name);
    }
    return BlocProvider(
      create: (_) => AutomationTriggerBloc(
        triggerPropertiesFields: initialFields,
      ),
      child: BaseScaffold(
        title: 'Automation Trigger',
        header: const _Header(),
        body: const _Body(),
      ),
    );
  }
}

class _Body extends StatelessWidget {
  const _Body();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: BlocBuilder<AutomationTriggerBloc, AutomationTriggerState>(
        builder: (context, state) {
          return AutomationTriggerFields(
            fields: state.triggerPropertiesFields,
          );
        },
      ),
    );
  }
}

class _Header extends StatelessWidget {
  const _Header();

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(bottom: 8.0),
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
              color: Color(0xFFEE883A),
              borderRadius: BorderRadius.circular(12.0),
            ),
            child: Center(
              child: SvgPicture.asset(
                "assets/icons/chat.svg",
                width: 25,
                height: 25,
                colorFilter: ColorFilter.mode(Colors.white, BlendMode.srcIn),
              ),
            ),
          ),
          SizedBox(width: 10.0),
          Text(
            "Temporary Title",
            style: Theme.of(context).textTheme.titleLarge!.copyWith(
                  fontSize: 20.0,
                ),
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
          )
        ],
      ),
    );
  }
}
