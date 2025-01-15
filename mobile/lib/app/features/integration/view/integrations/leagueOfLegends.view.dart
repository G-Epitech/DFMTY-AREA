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
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => Navigator.of(context).pop(),
        ),
      ),
      extendBodyBehindAppBar: true,
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: BlocProvider(
          create: (context) => LeagueOfLegendsIntegrationBloc(
            leagueOfLegends:
                context.read<IntegrationMediator>().leagueOfLegends,
          ),
          child: const LeagueOfLegendsIntegrationForm(),
        ),
      ),
    );
  }
}
