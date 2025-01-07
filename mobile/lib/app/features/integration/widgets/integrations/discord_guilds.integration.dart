import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/integration/widgets/integration_card.widget.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class DiscordGuildItemWidget extends StatelessWidget {
  final DiscordGuild guild;

  const DiscordGuildItemWidget({required this.guild, super.key});

  @override
  Widget build(BuildContext context) {
    return IntegrationCard(
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
            // TODO: Implement action
          },
          child: Container(
            width: 40,
            height: 40,
            decoration: BoxDecoration(
              color: guild.linked
                  ? Colors.green
                  : Theme.of(context).colorScheme.onPrimaryContainer,
              borderRadius: BorderRadius.circular(8),
            ),
            child: Center(
              child: SvgPicture.asset(
                guild.linked
                    ? 'assets/icons/check.svg'
                    : 'assets/icons/plus.svg',
                height: 20,
                width: 20,
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
