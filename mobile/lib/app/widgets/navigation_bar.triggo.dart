import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/colors/colors.dart';

final ValueNotifier<String?> currentRouteNotifier =
    ValueNotifier<String?>(RoutesNames.home);

class TriggoNavigationBar extends StatelessWidget {
  const TriggoNavigationBar({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 12.0, vertical: 8.0),
        child: Row(
          children: [
            _TriggoNavigationBarItem(
              routeName: RoutesNames.home,
              iconAsset: 'assets/icons/home.svg',
            ).build(context),
            _TriggoNavigationBarItem(
              routeName: RoutesNames.integrations,
              iconAsset: 'assets/icons/link.svg',
            ).build(context),
            _TriggoNavigationBarItem(
              routeName: RoutesNames.automations,
              iconAsset: 'assets/icons/bolt.svg',
            ).build(context),
            _TriggoNavigationBarItem(
              routeName: RoutesNames.profile,
              iconAsset: 'assets/icons/user.svg',
            ).build(context),
          ],
        ),
      ),
    );
  }
}

class _TriggoNavigationBarItem {
  final String iconAsset;
  final String routeName;

  const _TriggoNavigationBarItem(
      {required this.iconAsset, required this.routeName});

  Widget build(BuildContext context) {
    return ValueListenableBuilder<String?>(
      valueListenable: currentRouteNotifier,
      builder: (context, currentRoute, child) {
        final bool isSelected = currentRoute == routeName;
        return Expanded(
          child: GestureDetector(
            behavior: HitTestBehavior.opaque,
            onTap: () {
              if (currentRoute != routeName) {
                currentRouteNotifier.value = routeName;
                Navigator.of(context).pushNamedAndRemoveUntil(
                    routeName, (route) => route.settings.name == routeName);
              }
            },
            child: Card(
              color: isSelected ? lightContainer : Colors.grey[100],
              child: SizedBox(
                height: 64,
                child: Center(
                  child: SvgPicture.asset(
                    iconAsset,
                    colorFilter: ColorFilter.mode(
                      (isSelected
                          ? Theme.of(context).colorScheme.primary
                          : Theme.of(context).textTheme.titleMedium!.color!),
                      BlendMode.srcIn,
                    ),
                    width: 27,
                    height: 27,
                  ),
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}
