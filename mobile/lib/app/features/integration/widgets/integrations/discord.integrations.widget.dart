import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/integration/view/integrations/discord.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class DiscordIntegrationListItemWidget extends StatelessWidget {
  final DiscordIntegration integration;

  const DiscordIntegrationListItemWidget({
    super.key,
    required this.integration,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: DiscordCustomWidget(
        integration: integration,
      ),
    );
  }
}

class DiscordCustomWidget extends StatelessWidget {
  final DiscordIntegration integration;

  const DiscordCustomWidget({
    super.key,
    required this.integration,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
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
    );
  }
}
