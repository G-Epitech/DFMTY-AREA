import 'package:flutter/material.dart';
import 'package:triggo/app/features/integrations/widgets/integration.widget.dart';
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
            children: [
              Row(
                children: [
                  Text(
                    integration.displayName,
                    style: Theme.of(context).textTheme.titleMedium,
                  ),
                ],
              ),
              Row(
                children: [
                  Text(
                    '${integration.username} - ${integration.email}',
                    style: Theme.of(context).textTheme.labelLarge,
                  ),
                ],
              ),
            ],
          ),
        ),
      ],
    );
  }
}
