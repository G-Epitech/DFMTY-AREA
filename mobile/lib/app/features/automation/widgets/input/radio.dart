import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';

class RadioInput extends StatefulWidget {
  final String label;
  final List<AutomationRadioModel>? options;
  final void Function(String, String) onChanged;
  final String defaultValue;
  final Future<List<AutomationRadioModel>> Function()? getOptions;

  const RadioInput({
    super.key,
    required this.label,
    this.options,
    required this.onChanged,
    required this.defaultValue,
    this.getOptions,
  });

  @override
  State<RadioInput> createState() => RadioInputState();
}

class RadioInputState extends State<RadioInput> {
  late String localValue;
  late String humanReadableValue;
  late Future<List<AutomationRadioModel>> optionsFuture;

  @override
  void initState() {
    super.initState();
    localValue = widget.defaultValue;

    optionsFuture = widget.getOptions != null
        ? widget.getOptions!()
        : Future.value(widget.options ?? []);
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<AutomationRadioModel>>(
      future: optionsFuture,
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Center(child: CircularProgressIndicator());
        } else if (snapshot.hasError) {
          return Center(
            child: Text(
              'Error loading options',
              style: Theme.of(context).textTheme.bodyMedium,
            ),
          );
        } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
          return Center(
            child: Text(
              'No options available',
              style: Theme.of(context).textTheme.bodyMedium,
            ),
          );
        }

        final options = snapshot.data!;
        return SingleChildScrollView(
          child: Column(
            children: options.map((option) {
              return GestureDetector(
                behavior: HitTestBehavior.opaque,
                onTap: () {
                  setState(() {
                    localValue = option.value;
                    humanReadableValue = option.title;
                    widget.onChanged(option.value, option.title);
                  });
                },
                child: Container(
                  margin: option == options.last
                      ? const EdgeInsets.all(0)
                      : const EdgeInsets.only(bottom: 8.0),
                  padding: const EdgeInsets.symmetric(
                      vertical: 12.0, horizontal: 16.0),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    border: Border.all(
                      color: localValue == option.value
                          ? Theme.of(context).colorScheme.primary
                          : Colors.transparent,
                    ),
                    borderRadius: BorderRadius.circular(8.0),
                  ),
                  child: Row(
                    children: [
                      Container(
                        height: 22,
                        width: 22,
                        decoration: BoxDecoration(
                          shape: BoxShape.circle,
                          border: Border.all(
                            color: localValue == option.value
                                ? Theme.of(context).colorScheme.primary
                                : Colors.grey.shade400,
                            width: 2,
                          ),
                        ),
                        child: localValue == option.value
                            ? Center(
                                child: Container(
                                  height: 12,
                                  width: 12,
                                  decoration: BoxDecoration(
                                    shape: BoxShape.circle,
                                    color:
                                        Theme.of(context).colorScheme.primary,
                                  ),
                                ),
                              )
                            : null,
                      ),
                      const SizedBox(width: 16),
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(option.title,
                                style: Theme.of(context).textTheme.labelLarge),
                            if (option.description.isNotEmpty)
                              Text(option.description,
                                  style: Theme.of(context)
                                      .textTheme
                                      .labelMedium
                                      ?.copyWith(
                                        fontSize: 12,
                                      )),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              );
            }).toList(),
          ),
        );
      },
    );
  }
}
