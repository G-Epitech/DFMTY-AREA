import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/creation/add.view.dart';
import 'package:triggo/app/features/integration/view/integrations/discord.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class DiscordIntegrationListItemWidget extends StatelessWidget {
  final DiscordIntegration integration;
  final AutomationChoiceEnum? type;
  final String? integrationIdentifier;
  final int? indexOfTheTriggerOrAction;

  const DiscordIntegrationListItemWidget({
    super.key,
    required this.integration,
    this.type,
    this.integrationIdentifier,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: DiscordCustomWidget(
        integration: integration,
        type: type,
        integrationIdentifier: integrationIdentifier,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
      ),
    );
  }
}

class DiscordCustomWidget extends StatelessWidget {
  final DiscordIntegration integration;
  final AutomationChoiceEnum? type;
  final String? integrationIdentifier;
  final int? indexOfTheTriggerOrAction;

  const DiscordCustomWidget({
    super.key,
    required this.integration,
    this.type,
    this.integrationIdentifier,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        if (type != null &&
            integrationIdentifier != null &&
            indexOfTheTriggerOrAction != null) {
          Navigator.push(
              context,
              customScreenBuilder(AutomationCreationAddView(
                type: type!,
                integrationIdentifier: integrationIdentifier!,
                indexOfTheTriggerOrAction: indexOfTheTriggerOrAction!,
              )));
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
                  backgroundColor: Color(0xFF5865F2),
                  child: Icon(
                    Icons.discord,
                    size: 15,
                    color: Colors.white,
                  ),
                ),
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
                  integration.displayName,
                  style: Theme.of(context).textTheme.labelLarge?.copyWith(
                        height: 1.1,
                        fontSize: 25,
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
          if (type == null &&
              integrationIdentifier == null &&
              indexOfTheTriggerOrAction == null)
            IconButton(
              onPressed: () {
                Navigator.push(
                    context,
                    customScreenBuilder(DiscordView(
                      discordGuild: integration,
                    )));
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
