import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/creation/add.view.dart';
import 'package:triggo/app/features/integration/utils/automation_update_provider.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/models/integrations/openAI.integration.model.dart';

class OpenAIIntegrationListItemWidget extends StatelessWidget {
  final OpenAIIntegration integration;
  final AutomationChoiceEnum? type;
  final String? integrationIdentifier;
  final int? indexOfTheTriggerOrAction;

  const OpenAIIntegrationListItemWidget({
    required this.integration,
    this.type,
    this.integrationIdentifier,
    this.indexOfTheTriggerOrAction,
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: OpenAICustomWidget(
        integration: integration,
        type: type,
        integrationIdentifier: integrationIdentifier,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
      ),
    );
  }
}

class OpenAICustomWidget extends StatelessWidget {
  final OpenAIIntegration integration;
  final AutomationChoiceEnum? type;
  final String? integrationIdentifier;
  final int? indexOfTheTriggerOrAction;

  const OpenAICustomWidget({
    required this.integration,
    this.type,
    this.integrationIdentifier,
    this.indexOfTheTriggerOrAction,
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        if (type != null &&
            integrationIdentifier != null &&
            indexOfTheTriggerOrAction != null) {
          automationUpdateProvider(
            context,
            type!,
            integration.id,
            indexOfTheTriggerOrAction!,
          );
          Navigator.push(
            context,
            customScreenBuilder(AutomationAddView(
              type: type!,
              integrationIdentifier: integrationIdentifier!,
              indexOfTheTriggerOrAction: indexOfTheTriggerOrAction!,
            )),
          );
        }
      },
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          CircleAvatar(
            backgroundColor: Color(0xFF10a37f),
            radius: 25,
            child: SvgPicture.asset(
              'assets/icons/openai.svg',
              width: 30,
              height: 30,
              colorFilter: ColorFilter.mode(
                Colors.white,
                BlendMode.srcIn,
              ),
            ),
          ),
          SizedBox(width: 10),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
                Text(
                  integration.ownerName,
                  style: Theme.of(context).textTheme.labelLarge?.copyWith(
                        height: 1.1,
                        fontSize: 24,
                        fontWeight: FontWeight.w700,
                      ),
                  overflow: TextOverflow.ellipsis,
                ),
                Text(
                  integration.ownerEmail,
                  style: Theme.of(context).textTheme.labelMedium?.copyWith(
                        fontSize: 12,
                        fontWeight: FontWeight.w400,
                      ),
                  overflow: TextOverflow.ellipsis,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
