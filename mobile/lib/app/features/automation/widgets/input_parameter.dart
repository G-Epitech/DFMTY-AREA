import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/routes/custom.router.dart';

class AutomationInputParameterWithLabel extends StatelessWidget {
  final String title;
  final String? previewData;
  final Widget input;
  final bool disabled;

  const AutomationInputParameterWithLabel({
    super.key,
    required this.title,
    required this.previewData,
    required this.input,
    this.disabled = false,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        if (disabled) return;
        Navigator.push(context, customScreenBuilder(input));
      },
      child: Container(
        padding: EdgeInsets.all(12.0),
        decoration: BoxDecoration(
          color: disabled ? Colors.grey.shade300 : Colors.white,
          borderRadius: BorderRadius.circular(8.0),
        ),
        child: Row(
          children: [
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    title,
                    style: Theme.of(context).textTheme.labelLarge!.copyWith(
                          fontWeight: FontWeight.bold,
                        ),
                  ),
                  Text(
                    previewData == null ? 'No $title' : previewData!,
                    style: Theme.of(context).textTheme.labelMedium,
                  ),
                ],
              ),
            ),
            if (previewData == null)
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 5.0),
                child: Icon(
                  Icons.warning_amber_rounded,
                  color: Theme.of(context).colorScheme.onError,
                  size: 30,
                ),
              ),
            SvgPicture.asset(
              "assets/icons/chevron-right.svg",
              width: 24.0,
              height: 24.0,
            ),
          ],
        ),
      ),
    );
  }
}
