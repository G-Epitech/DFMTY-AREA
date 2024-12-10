import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationScreen extends StatefulWidget {
  const AutomationScreen({super.key});

  @override
  State<AutomationScreen> createState() => _AutomationScreenState();
}

class _AutomationScreenState extends State<AutomationScreen> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      body: Container(
        decoration: BoxDecoration(
          color: Colors.grey[200],
        ),
        child: Center(
          child: Container(
            margin: EdgeInsets.symmetric(horizontal: 16.0),
            child: _DropdownExample(),
          ),
        ),
      ),
    );
  }
}

class _DropdownExample extends StatefulWidget {
  @override
  _DropdownExampleState createState() => _DropdownExampleState();
}

class _DropdownExampleState extends State<_DropdownExample> {
  String _selectedItem = 'Select Guild';
  bool _error = true;

  List<String> get _guildItems {
    if (_selectedItem == 'Select Guild') {
      return ['Select Guild', 'Guild 1', 'Guild 2', 'Guild 3'];
    } else {
      return ['Guild 1', 'Guild 2', 'Guild 3'];
    }
  }

  @override
  Widget build(BuildContext context) {
    return DropdownButtonHideUnderline(
      child: Container(
        padding: EdgeInsets.symmetric(horizontal: 16.0),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(12.0),
          color: Theme.of(context).colorScheme.surface,
        ),
        child: Row(
          children: [
            SvgPicture.asset(
              'assets/icons/people.svg',
              colorFilter:
                  ColorFilter.mode(textSecondaryColor, BlendMode.srcIn),
              width: 18,
            ),
            SizedBox(width: 8.0),
            Expanded(
              child: DropdownButton<String>(
                value: _selectedItem,
                isExpanded: true,
                items:
                    _guildItems.map<DropdownMenuItem<String>>((String value) {
                  return DropdownMenuItem<String>(
                    value: value,
                    child: Row(
                      children: [
                        Text(
                          value,
                          style: const TextStyle(
                            fontFamily: 'Onest',
                            fontSize: 14,
                            fontWeight: FontWeight.w500,
                            letterSpacing: -0.2,
                            color: textSecondaryColor,
                          ),
                        ),
                      ],
                    ),
                  );
                }).toList(),
                borderRadius: BorderRadius.circular(12.0),
                onChanged: (String? newValue) {
                  setState(() {
                    _selectedItem = newValue!;
                    _error = _selectedItem == 'Select Guild';
                  });
                },
                icon: _error
                    ? Icon(Icons.warning_amber_rounded,
                        color: Theme.of(context).colorScheme.onError)
                    : Icon(Icons.arrow_drop_down, color: textSecondaryColor),
                style: TextStyle(color: Colors.grey),
                dropdownColor: Colors.white,
                underline: Container(),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
