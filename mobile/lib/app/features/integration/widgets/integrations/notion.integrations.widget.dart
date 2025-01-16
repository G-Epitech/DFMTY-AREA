import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/singleton/add.view.dart';
import 'package:triggo/app/features/integration/utils/automation_update_provider.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/models/integrations/notion.integration.model.dart';

class NotionIntegrationListItemWidget extends StatelessWidget {
  final NotionIntegration integration;
  final AutomationChoiceEnum? type;
  final String? integrationIdentifier;
  final int? indexOfTheTriggerOrAction;

  const NotionIntegrationListItemWidget({
    super.key,
    required this.integration,
    this.type,
    this.integrationIdentifier,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: NotionCustomWidget(
        integration: integration,
        type: type,
        integrationIdentifier: integrationIdentifier,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
      ),
    );
  }
}

class NotionCustomWidget extends StatelessWidget {
  final NotionIntegration integration;
  final AutomationChoiceEnum? type;
  final String? integrationIdentifier;
  final int? indexOfTheTriggerOrAction;

  const NotionCustomWidget({
    super.key,
    required this.integration,
    this.type,
    this.integrationIdentifier,
    this.indexOfTheTriggerOrAction,
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
          Stack(
            children: [
              CircleAvatar(
                backgroundImage: NetworkImage(integration.avatarUri),
                radius: 25,
              ),
              Positioned(
                bottom: 0,
                right: 0,
                child: CircleAvatar(
                    radius: 10,
                    backgroundColor: Color(0xFF4E4E4E),
                    child: SvgPicture.asset(
                      "assets/icons/notion.svg",
                      width: 15,
                      height: 15,
                      colorFilter: ColorFilter.mode(
                        Colors.white,
                        BlendMode.srcIn,
                      ),
                    )),
              ),
            ],
          ),
          SizedBox(width: 10),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
                Text(
                  integration.workspaceName,
                  style: Theme.of(context).textTheme.labelLarge?.copyWith(
                        height: 1.1,
                        fontSize: 24,
                        fontWeight: FontWeight.w700,
                      ),
                  overflow: TextOverflow.ellipsis,
                ),
                Text(
                  '${integration.username} - ${integration.email}',
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
