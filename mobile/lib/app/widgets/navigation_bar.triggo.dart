import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/card/bloc/card_bloc.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/colors/colors.dart';

class TriggoNavigationBar extends StatelessWidget {
  const TriggoNavigationBar({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 12.0),
      child: Row(
        children: [
          _TriggoNavigationBarItem(
            routeName: RoutesNames.home,
            icon: Icons.home,
          ).build(context),
          _TriggoNavigationBarItem(
            routeName: RoutesNames.integrations,
            icon: Icons.link,
          ).build(context),
          _TriggoNavigationBarItem(
                  routeName: RoutesNames.automations, icon: Icons.bolt_outlined)
              .build(context),
        ],
      ),
    );
  }
}

class _TriggoNavigationBarItem {
  final IconData? icon;
  final String routeName;

  const _TriggoNavigationBarItem({required this.icon, required this.routeName});

  Widget build(BuildContext context) {
    return BlocProvider(
      create: (_) => CardBloc(),
      child: Builder(
        builder: (context) {
          return Expanded(
            child: GestureDetector(
              onTap: () {
                if (ModalRoute.of(context)?.settings.name != routeName) {
                  Navigator.of(context).pushNamedAndRemoveUntil(
                      routeName, (route) => route.settings.name == routeName);
                }
              },
              onTapDown: (_) => context.read<CardBloc>().add(CardEvent.press),
              onTapUp: (_) => context.read<CardBloc>().add(CardEvent.release),
              onTapCancel: () =>
                  context.read<CardBloc>().add(CardEvent.release),
              child: BlocBuilder<CardBloc, bool>(
                builder: (context, isPressed) {
                  return Card(
                    color: isPressed ? lightContainer : Colors.grey[100],
                    child: SizedBox(
                      height: 80,
                      child: Center(
                        child: Icon(
                          icon,
                          size: 40,
                          color: isPressed
                              ? Theme.of(context).colorScheme.primary
                              : Theme.of(context).textTheme.titleMedium?.color,
                        ),
                      ),
                    ),
                  );
                },
              ),
            ),
          );
        },
      ),
    );
  }
}
