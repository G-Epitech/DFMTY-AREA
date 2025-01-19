import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/models/integrations/gmail.integration.model.dart';

class GmailIntegrationListItemWidget extends StatelessWidget {
  final GmailIntegration integration;

  const GmailIntegrationListItemWidget({
    super.key,
    required this.integration,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: GmailCustomWidget(
        integration: integration,
      ),
    );
  }
}

class GmailCustomWidget extends StatelessWidget {
  final GmailIntegration integration;

  const GmailCustomWidget({
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
                integration.displayName,
                style: Theme.of(context).textTheme.labelLarge?.copyWith(
                      height: 1.1,
                      fontSize: 24,
                      fontWeight: FontWeight.w700,
                    ),
                overflow: TextOverflow.ellipsis,
              ),
              Text(
                '${integration.firstName} ${integration.lastName} - ${integration.email}',
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
    );
  }
}
