import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/creation/select_integration_account.view.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/features/integration/view/integrations/leagueOfLegends.view.dart';
import 'package:triggo/app/features/integration/view/integrations/openAI.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';

class IntegrationListItemWidget extends StatelessWidget {
  final AvailableIntegration integration;
  final AutomationChoiceEnum? type;
  final int? indexOfTheTriggerOrAction;

  const IntegrationListItemWidget({
    super.key,
    required this.integration,
    this.type,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoCard(
      customWidget: _CustomWidget(
        integration: integration,
        type: type,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
      ),
    );
  }
}

class _CustomWidget extends StatelessWidget {
  final AvailableIntegration integration;
  final AutomationChoiceEnum? type;
  final int? indexOfTheTriggerOrAction;

  const _CustomWidget({
    required this.integration,
    this.type,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        if (type != null) {
          Navigator.push(
              context,
              customScreenBuilder(
                  AutomationCreationSelectIntegrationsAccountView(
                type: type!,
                integrationIdentifier: integration.url,
                indexOfTheTriggerOrAction: indexOfTheTriggerOrAction!,
              )));
        } else {
          _customOnTap(context, integration, integrationMediator);
        }
      },
      child: Row(
        children: [
          Container(
            width: 45,
            height: 45,
            decoration: BoxDecoration(
              color: integration.color,
              borderRadius: BorderRadius.circular(100),
            ),
            child: Center(
              child: SvgPicture.asset(
                integration.iconUri,
                width: 26,
                height: 26,
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
                      overflow: TextOverflow.ellipsis,
                    ),
                  ],
                ),
              ],
            ),
          ),
          SvgPicture.asset(
            'assets/icons/chevron-right.svg',
            width: 20,
            height: 20,
            colorFilter: ColorFilter.mode(
              textPrimaryColor,
              BlendMode.srcIn,
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
  switch (integration.name) {
    case IntegrationNames.openAI:
      await Navigator.push(context,
          MaterialPageRoute(builder: (context) => OpenAIIntegrationView()));
      break;
    case IntegrationNames.leagueOfLegends:
      await Navigator.push(
          context,
          MaterialPageRoute(
              builder: (context) => LeagueOfLegendsIntegrationView()));
      break;
    default:
      integrationMediator.launchURLFromIntegration(integration.url);
  }
}
