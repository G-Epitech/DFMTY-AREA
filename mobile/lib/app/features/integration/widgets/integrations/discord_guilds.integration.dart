import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/mediator/integrations/integration.mediator.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class DiscordGuildItemWidget extends StatelessWidget {
  final DiscordGuild guild;

  const DiscordGuildItemWidget({required this.guild, super.key});

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: _DiscordGuildsCustomWidget(guild: guild),
    );
  }
}

class _DiscordGuildsCustomWidget extends StatelessWidget {
  final DiscordGuild guild;

  const _DiscordGuildsCustomWidget({
    required this.guild,
  });

  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        CircleAvatar(
          backgroundImage: NetworkImage(guild.iconUri),
          radius: 25,
        ),
        SizedBox(width: 10),
        Expanded(
          child: Text(
            guild.name,
            style:
                Theme.of(context).textTheme.titleMedium?.copyWith(fontSize: 22),
            overflow: TextOverflow.ellipsis,
          ),
        ),
        GestureDetector(
          onTap: () {
            integrationMediator.launchURL(
                "https://discord.com/oauth2/authorize?client_id=1313818262806462464&permissions=8&integration_type=0&scope=bot");
          },
          child: Container(
            width: 35,
            height: 35,
            decoration: BoxDecoration(
              color: guild.linked
                  ? Colors.green
                  : Theme.of(context).colorScheme.primary,
              borderRadius: BorderRadius.circular(8),
            ),
            child: Center(
              child: SvgPicture.asset(
                guild.linked
                    ? 'assets/icons/check.svg'
                    : 'assets/icons/plus.svg',
                height: 16,
                width: 16,
                colorFilter: ColorFilter.mode(
                  Colors.white,
                  BlendMode.srcIn,
                ),
              ),
            ),
          ),
        ),
      ],
    );
  }
}
