import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/mediator/automation.mediator.dart';

class IconInput extends StatefulWidget {
  final String? placeholder;
  final void Function(String) onValueChanged;
  final String defaultValue;
  final String hexColor;

  const IconInput({
    super.key,
    required this.placeholder,
    required this.onValueChanged,
    required this.defaultValue,
    required this.hexColor,
  });

  @override
  State<IconInput> createState() => IconInputState();
}

class IconInputState extends State<IconInput> {
  @override
  Widget build(BuildContext context) {
    final AutomationMediator mediator =
        RepositoryProvider.of<AutomationMediator>(context);
    final icons = mediator.getIcons();

    return GridView.builder(
      gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 5,
        crossAxisSpacing: 12.0,
        mainAxisSpacing: 8.0,
        childAspectRatio: 1.0,
      ),
      itemCount: icons.length,
      itemBuilder: (context, index) {
        final icon = icons[index];
        return Container(
          padding: const EdgeInsets.all(8.0),
          decoration: BoxDecoration(
            color: HexColor(widget.hexColor),
            borderRadius: BorderRadius.circular(8.0),
          ),
          child: SvgPicture.asset(
            'assets/icons/$icon.svg',
            width: 30,
            height: 30,
            colorFilter: ColorFilter.mode(
              Colors.white,
              BlendMode.srcIn,
            ),
          ),
        );
      },
    );
  }
}
