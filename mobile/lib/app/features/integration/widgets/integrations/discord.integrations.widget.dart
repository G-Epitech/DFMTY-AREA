import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/integration/widgets/integration_card.widget.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class DiscordIntegrationListItemWidget extends StatelessWidget {
  final DiscordIntegration integration;

  const DiscordIntegrationListItemWidget(
      {required this.integration, super.key});

  @override
  Widget build(BuildContext context) {
    return IntegrationCard(
      customWidget: DiscordCustomWidget(integration: integration),
    );
  }
}

class DiscordCustomWidget extends StatelessWidget {
  final DiscordIntegration integration;

  const DiscordCustomWidget({required this.integration, super.key});

  @override
  Widget build(BuildContext context) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      // Centrer verticalement les enfants
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
                style: Theme.of(context)
                    .textTheme
                    .titleMedium
                    ?.copyWith(height: 1.1),
                overflow: TextOverflow.ellipsis,
              ),
              Text(
                '${integration.username} - ${integration.email}',
                style: Theme.of(context).textTheme.labelLarge,
                overflow: TextOverflow.ellipsis,
              ),
            ],
          ),
        ),
        IconButton(
          onPressed: () {
            print("Accéder aux paramètres de ${integration.displayName}");
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
