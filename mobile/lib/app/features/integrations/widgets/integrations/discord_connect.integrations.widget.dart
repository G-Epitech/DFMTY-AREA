import 'package:flutter/material.dart';
import 'package:triggo/app/features/integrations/widgets/integration.widget.dart';

class DiscordConnectIntegrationListItemWidget extends StatelessWidget {
  const DiscordConnectIntegrationListItemWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return IntegrationCard(
      customWidget: _DiscordCustomWidget(),
    );
  }
}

class _DiscordCustomWidget extends StatelessWidget {
  const _DiscordCustomWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Stack(
          children: [
            CircleAvatar(
              radius: 10,
              backgroundColor: Color(0xFF5865F2),
              child: Icon(
                Icons.discord,
                size: 15,
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
                    "Discord",
                    style: Theme.of(context).textTheme.titleMedium,
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
