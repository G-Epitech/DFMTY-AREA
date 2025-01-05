import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/theme/colors/colors.dart';

class DropdownContainer extends StatelessWidget {
  final TriggerPropertiesFields field;
  final ValueChanged<String?> onChanged;

  const DropdownContainer({
    super.key,
    required this.field,
    required this.onChanged,
  });

  List<DropdownMenuItem<String>> _buildDropdownItems() {
    List<DropdownMenuItem<String>> items = field.values.map((option) {
      return DropdownMenuItem<String>(
        value: option.id,
        child: Text(
          option.name,
          style: _textStyle(),
        ),
      );
    }).toList();

    if (field.selectedValue == null) {
      items.insert(
        0,
        DropdownMenuItem<String>(
          value: null,
          child: Text(
            'Select a Guild',
            style: _textStyle(),
          ),
        ),
      );
    }
    return items;
  }

  @override
  Widget build(BuildContext context) {
    List<DropdownMenuItem<String>> items = _buildDropdownItems();

    return _DropdownButton(
      field: field,
      items: items,
      onChanged: onChanged,
      context: context,
    );
  }
}

class _DropdownButton extends StatelessWidget {
  final TriggerPropertiesFields field;
  final List<DropdownMenuItem<String>> items;
  final ValueChanged<String?> onChanged;
  final BuildContext context;

  const _DropdownButton({
    required this.field,
    required this.items,
    required this.onChanged,
    required this.context,
  });

  @override
  Widget build(BuildContext context) {
    return DropdownButtonHideUnderline(
      child: Container(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(12.0),
          color: Theme.of(context).colorScheme.surface,
        ),
        child: Row(
          children: [
            _Icon(),
            const SizedBox(width: 8.0),
            _Dropdown(field: field, items: items, onChanged: onChanged),
          ],
        ),
      ),
    );
  }
}

class _Icon extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return SvgPicture.asset(
      'assets/icons/people.svg',
      colorFilter: const ColorFilter.mode(
        textSecondaryColor,
        BlendMode.srcIn,
      ),
      width: 18,
    );
  }
}

class _Dropdown extends StatelessWidget {
  final TriggerPropertiesFields field;
  final List<DropdownMenuItem<String>> items;
  final ValueChanged<String?> onChanged;

  const _Dropdown({
    required this.field,
    required this.items,
    required this.onChanged,
  });

  Icon _getDropdownIcon(BuildContext context) {
    return Icon(
      field.selectedValue == null
          ? Icons.warning_amber_rounded
          : Icons.arrow_drop_down,
      color: field.selectedValue == null
          ? Theme.of(context).colorScheme.onError
          : textSecondaryColor,
    );
  }

  Text _hintText(String text) {
    return Text(
      text,
      style: _textStyle(),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: DropdownButton<String>(
        value: field.selectedValue,
        isExpanded: true,
        items: items,
        borderRadius: BorderRadius.circular(12.0),
        onChanged: onChanged,
        icon: _getDropdownIcon(context),
        dropdownColor: Colors.white,
        underline: Container(),
        hint: field.selectedValue == null ? _hintText('Select a Guild') : null,
      ),
    );
  }
}

TextStyle _textStyle() {
  return const TextStyle(
    fontFamily: 'Onest',
    fontSize: 14,
    fontWeight: FontWeight.w500,
    letterSpacing: -0.2,
    color: textSecondaryColor,
  );
}
