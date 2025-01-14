import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/models/integrations/openAI.integration.model.dart';

class OpenAIIntegrationListItemWidget extends StatelessWidget {
  final OpenAIIntegration integration;

  const OpenAIIntegrationListItemWidget({required this.integration, super.key});

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: OpenAICustomWidget(integration: integration),
    );
  }
}

class OpenAICustomWidget extends StatelessWidget {
  final OpenAIIntegration integration;

  const OpenAICustomWidget({required this.integration, super.key});

  @override
  Widget build(BuildContext context) {
    return Row(
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
                style: Theme.of(context)
                    .textTheme
                    .titleMedium
                    ?.copyWith(height: 1.1),
                overflow: TextOverflow.ellipsis,
              ),
              Text(
                integration.ownerEmail,
                style: Theme.of(context).textTheme.labelLarge,
                overflow: TextOverflow.ellipsis,
              ),
            ],
          ),
        ),
      ],
    );
  }
}
