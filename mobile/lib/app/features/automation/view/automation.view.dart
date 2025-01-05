import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/view/automation_parameter.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationView extends StatefulWidget {
  final Automation automation;

  const AutomationView({super.key, required this.automation});

  @override
  State<AutomationView> createState() => _AutomationViewState();
}

class _AutomationViewState extends State<AutomationView> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: 'Create Automation',
      header: _Header(automation: widget.automation),
      body: _AutomationTriggerContainer(automation: widget.automation),
      getBack: true,
    );
  }
}

class _Header extends StatelessWidget {
  final Automation automation;

  const _Header({required this.automation});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(bottom: 8.0),
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
              color: automation.iconColor,
              borderRadius: BorderRadius.circular(12.0),
            ),
            child: Center(
              child: SvgPicture.asset(
                automation.iconUri,
                width: 25,
                height: 25,
                color: Colors.white,
              ),
            ),
          ),
          SizedBox(width: 10.0),
          Text(
            automation.name,
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

class _AutomationTriggerContainer extends StatelessWidget {
  final Automation automation;

  const _AutomationTriggerContainer({required this.automation});

  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);

    return Column(
      children: [
        CustomRectangleList(automation: automation),
      ],
    );
  }
}

class CustomRectangleList extends StatelessWidget {
  final Automation automation;

  const CustomRectangleList({super.key, required this.automation});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        _TriggerListItem(
          icon: "assets/icons/chat.svg",
          color: Color(0xFF5865F2),
          text: "Message received in channel",
        ),
        SizedBox(height: 10),
        _TriggerListItem(
          icon: "assets/icons/people.svg",
          color: Color(0xFF5865F2),
          text: "React to a message",
        ),
      ],
    );
  }
}

class _TriggerListItem extends StatelessWidget {
  final String icon;
  final Color color;
  final String text;

  const _TriggerListItem({
    required this.icon,
    required this.color,
    required this.text,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        Navigator.push(context, customScreenBuilder(AutomationParameterView()));
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
