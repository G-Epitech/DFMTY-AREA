import 'package:flutter/material.dart';

class TriggoCard extends StatelessWidget {
  final Widget? customWidget;

  const TriggoCard({
    super.key,
    this.customWidget,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            if (customWidget != null) customWidget!,
          ],
        ),
      ),
    );
  }
}
