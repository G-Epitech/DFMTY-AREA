import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/models/integrations/leagueOfLegends.integration.model.dart';

class LeagueOfLegendsIntegrationListItemWidget extends StatelessWidget {
  final LeagueOfLegendsIntegration integration;

  const LeagueOfLegendsIntegrationListItemWidget({
    super.key,
    required this.integration,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: LeagueOfLegendsCustomWidget(
        integration: integration,
      ),
    );
  }
}

class LeagueOfLegendsCustomWidget extends StatelessWidget {
  final LeagueOfLegendsIntegration integration;

  const LeagueOfLegendsCustomWidget({
    required this.integration,
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Stack(
          children: [
            CircleAvatar(
              backgroundImage: NetworkImage(integration.summonerProfileIcon),
              radius: 25,
            ),
            Positioned(
              bottom: 0,
              right: 0,
              child: CircleAvatar(
                  radius: 10,
                  backgroundColor: Color(0xFFc89b3c),
                  child: SvgPicture.asset(
                    "assets/icons/league_of_legends.svg",
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
                '${integration.riotGameName}#${integration.riotTagLine}',
                style: Theme.of(context).textTheme.labelLarge?.copyWith(
                      height: 1.1,
                      fontSize: 24,
                      fontWeight: FontWeight.w700,
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
