import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations/discord/discord_guilds_bloc.dart';
import 'package:triggo/app/features/integration/widgets/integrations/discord_guilds.integration.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/utils/launch_url.dart';

class DiscordView extends StatefulWidget {
  final DiscordIntegration discordGuild;

  const DiscordView({super.key, required this.discordGuild});

  @override
  State<DiscordView> createState() => _DiscordViewState();
}

class _DiscordViewState extends State<DiscordView> with WidgetsBindingObserver {
  late final DiscordIntegration discord;
  late DiscordGuildsBloc _discordGuildsBloc;

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    if (state == AppLifecycleState.resumed) {
      _discordGuildsBloc.add(LoadDiscordGuilds(discord.id));
    }
  }

  @override
  void initState() {
    super.initState();
    discord = widget.discordGuild;
    WidgetsBinding.instance.addObserver(this);

    final integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    _discordGuildsBloc = DiscordGuildsBloc(integrationMediator.discord)
      ..add(LoadDiscordGuilds(discord.id));
  }

  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    _discordGuildsBloc.close();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocProvider.value(
      value: _discordGuildsBloc,
      child: BaseScaffold(
        title: 'Discord Guild',
        body: Column(children: [
          Row(
            children: [
              Expanded(
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                      vertical: 8.0, horizontal: 4.0),
                  child: _GuildConnectionButton(),
                ),
              ),
            ],
          ),
          Expanded(
            child: BlocBuilder<DiscordGuildsBloc, DiscordGuildsState>(
                builder: (context, state) {
              return _StateManager(state: state);
            }),
          ),
        ]),
        getBack: true,
      ),
    );
  }
}

class _GuildConnectionButton extends StatelessWidget {
  const _GuildConnectionButton();

  @override
  Widget build(BuildContext context) {
    return TriggoButton(
      text: 'Link a Guild',
      onPressed: () async {
        try {
          await launchURL(
              "https://discord.com/oauth2/authorize?client_id=1313818262806462464&permissions=8&integration_type=0&scope=bot");
        } catch (e) {
          if (context.mounted) {
            ScaffoldMessenger.of(context)
              ..removeCurrentSnackBar()
              ..showSnackBar(SnackBar(content: Text('Failed to Link a Guild')));
          }
        }
      },
    );
  }
}

class _StateManager extends StatelessWidget {
  final DiscordGuildsState state;

  const _StateManager({required this.state});

  @override
  Widget build(BuildContext context) {
    if (state is DiscordGuildsLoading) {
      return const Center(child: CircularProgressIndicator());
    } else if (state is DiscordGuildsLoaded &&
        (state as DiscordGuildsLoaded).guilds.isNotEmpty) {
      return _GuildContainer(guilds: (state as DiscordGuildsLoaded).guilds);
    } else if (state is DiscordGuildsError) {
      return _ErrorView(error: (state as DiscordGuildsError).message);
    } else {
      return const _NoDataView();
    }
  }
}

class _ErrorView extends StatelessWidget {
  final Object error;

  const _ErrorView({required this.error});

  @override
  Widget build(BuildContext context) {
    return Text('Error: $error');
  }
}

class _NoDataView extends StatelessWidget {
  const _NoDataView();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Text('No Guilds found',
          textAlign: TextAlign.center,
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _GuildContainer extends StatelessWidget {
  final List<DiscordGuild> guilds;

  const _GuildContainer({
    required this.guilds,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Expanded(child: _GuildList(guilds: guilds)),
      ],
    );
  }
}

class _GuildList extends StatelessWidget {
  const _GuildList({required this.guilds});

  final List<DiscordGuild> guilds;

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: guilds.length,
      itemBuilder: (context, index) {
        final guild = guilds[index];
        return _GuildListItem(guild: guild);
      },
    );
  }
}

class _GuildListItem extends StatelessWidget {
  final DiscordGuild guild;

  const _GuildListItem({required this.guild});

  @override
  Widget build(BuildContext context) {
    return DiscordGuildItemWidget(guild: guild);
  }
}
