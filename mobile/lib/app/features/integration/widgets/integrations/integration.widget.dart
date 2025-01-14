import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/features/integration/view/integrations/openAI.view.dart';
import 'package:triggo/app/features/integration/widgets/integration_card.widget.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';

class IntegrationListItemWidget extends StatelessWidget {
  final AvailableIntegration integration;

  const IntegrationListItemWidget({
    super.key,
    required this.integration,
  });

  @override
  Widget build(BuildContext context) {
    return IntegrationCard(
      customWidget: _CustomWidget(
        integration: integration,
      ),
    );
  }
}

class _CustomWidget extends StatelessWidget {
  final AvailableIntegration integration;

  const _CustomWidget({
    required this.integration,
  });

  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    return GestureDetector(
      onTap: () {
        _customOnTap(context, integration, integrationMediator);
      },
      child: Row(
        children: [
          Container(
            width: 40,
            height: 40,
            decoration: BoxDecoration(
              color: integration.color,
              borderRadius: BorderRadius.circular(4),
            ),
            child: Center(
              child: SvgPicture.asset(
                integration.iconUri,
                width: 30,
                height: 30,
                colorFilter: ColorFilter.mode(
                  Colors.white,
                  BlendMode.srcIn,
                ),
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
                      integration.name,
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

Future<void> _customOnTap(
    BuildContext context,
    AvailableIntegration integration,
    IntegrationMediator integrationMediator) async {
  if (integration.name == IntegrationNames.openAI) {
    await Navigator.push(
      context,
      MaterialPageRoute(builder: (context) => OpenAIIntegrationView()),
    );
  } else {
    integrationMediator.launchURLFromIntegration(integration.url);
  }
}
