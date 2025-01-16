import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/integration/bloc/integrations/leagueOfLegends/league_of_legends_bloc.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class LeagueOfLegendsIntegrationForm extends StatelessWidget {
  const LeagueOfLegendsIntegrationForm({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocListener<LeagueOfLegendsIntegrationBloc,
        LeagueOfLegendsIntegrationState>(
      listener: _listener,
      child: Align(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              _Label(),
              const SizedBox(height: 12),
              _GameNameInput(),
              const SizedBox(height: 12),
              _TagLineInput(),
              const SizedBox(height: 12),
              _LinkAccountButton(),
            ],
          ),
        ),
      ),
    );
  }
}

class _Label extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Text(
      'Enter your LeagueOfLegends keys',
      style: Theme.of(context).textTheme.titleLarge,
    );
  }
}

class _GameNameInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      placeholder: 'Game Name',
      onChanged: (gameName) {
        context
            .read<LeagueOfLegendsIntegrationBloc>()
            .add(LeagueOfLegendsGameNameChanged(gameName));
      },
    );
  }
}

class _TagLineInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      placeholder: 'Tag Line',
      onChanged: (tagLine) {
        context
            .read<LeagueOfLegendsIntegrationBloc>()
            .add(LeagueOfLegendsTagLineChanged(tagLine));
      },
    );
  }
}

class _LinkAccountButton extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final isInProgressOrSuccess = context.select(
      (LeagueOfLegendsIntegrationBloc bloc) =>
          bloc.state.status.isInProgressOrSuccess,
    );

    if (isInProgressOrSuccess) return const CircularProgressIndicator();

    final isValid = context
        .select((LeagueOfLegendsIntegrationBloc bloc) => bloc.state.isValid);

    return SizedBox(
      width: double.infinity,
      child: TriggoButton(
          text: 'Link Account',
          onPressed: isValid
              ? () {
                  context
                      .read<LeagueOfLegendsIntegrationBloc>()
                      .add(const LeagueOfLegendsSubmitted());
                }
              : null),
    );
  }
}

void _onLoginSuccess(
    BuildContext context, LeagueOfLegendsIntegrationState state) {
  List<String> tokens = [state.gameName.value, state.tagLine.value];
  Navigator.pop(context, tokens);
}

void _listener(BuildContext context, LeagueOfLegendsIntegrationState state) {
  if (state.status.isFailure) {
    ScaffoldMessenger.of(context)
      ..hideCurrentSnackBar()
      ..showSnackBar(
        SnackBar(
            content: const Text('Link Failure'),
            backgroundColor: Theme.of(context).colorScheme.onError),
      );
    context
        .read<LeagueOfLegendsIntegrationBloc>()
        .add(const LeagueOfLegendsReset());
    return;
  }
  if (state.status.isSuccess) {
    _onLoginSuccess(context, state);
  }
}
