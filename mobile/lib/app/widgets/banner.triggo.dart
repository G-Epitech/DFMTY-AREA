import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class TriggoBanner extends StatelessWidget {
  const TriggoBanner({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 8.0),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(12.0),
        color: Theme.of(context).colorScheme.primary,
      ),
      child: Container(
        padding: const EdgeInsets.only(top: 14.0),
        child: Row(
          children: [
            Text(
              'Triggo',
              style: Theme.of(context).textTheme.titleMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onPrimary,
                  ),
            ),
            const SizedBox(width: 8.0),
            SvgPicture.asset(
              'assets/icons/bubbles.svg',
              width: 24.0,
              height: 24.0,
            ),
          ],
        ),
      ),
    );
  }
}
