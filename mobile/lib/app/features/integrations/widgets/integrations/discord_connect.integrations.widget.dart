import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integrations/widgets/integration.widget.dart';
import 'package:triggo/mediator/integration.mediator.dart';

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
  const _DiscordCustomWidget();

  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);

    return GestureDetector(
      onTap: () {
        integrationMediator.launchURL("discord");
      },
      child: Row(
        children: [
          Container(
            width: 40,
            height: 40,
            decoration: BoxDecoration(
              color: Color(0xFF5865F2),
              borderRadius: BorderRadius.circular(4),
            ),
            child: Center(
              child: Icon(
                Icons.discord,
                size: 30,
                color: Colors.white,
              ),
            ),
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
                      style: Theme.of(context).textTheme.labelLarge,
                    ),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
