import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations/leagueOfLegends/league_of_legends_bloc.dart';
import 'package:triggo/app/features/integration/widgets/integrations/league_of_legends_form.widget.dart';
import 'package:triggo/mediator/integration.mediator.dart';

class LeagueOfLegendsIntegrationView extends StatelessWidget {
  const LeagueOfLegendsIntegrationView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      extendBodyBehindAppBar: true,
      body: Stack(
        children: [
          Padding(
            padding: EdgeInsets.only(
              top: kToolbarHeight + MediaQuery.of(context).padding.top + 8,
              left: 12,
              right: 12,
              bottom: 12,
            ),
            child: BlocProvider(
              create: (context) => LeagueOfLegendsIntegrationBloc(
                leagueOfLegends:
                    context.read<IntegrationMediator>().leagueOfLegends,
              ),
              child: const LeagueOfLegendsIntegrationForm(),
            ),
          ),
          Positioned(
            top: 0,
            left: 0,
            right: 0,
            child: Container(
              color: Colors.white,
              padding: EdgeInsets.only(
                top: MediaQuery.of(context).padding.top,
                left: 8,
                right: 8,
              ),
              child: Row(
                children: [
                  IconButton(
                    icon: const Icon(Icons.arrow_back),
                    onPressed: () => Navigator.of(context).pop(),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
