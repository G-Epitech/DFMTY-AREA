import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationInputView extends StatefulWidget {
  final AutomationInputEnum type;
  final String label;
  final String? placeholder;
  final List<AutomationRadioModel>? options;
  final List<AutomationCheckboxModel>? checkboxes;
  final void Function(String, String)? onSave;
  final void Function(List<String>, List<String>)? onCheckboxesSave;
  final String? value;
  final List<String>? checkboxesValues;
  final String? humanReadableValue;
  final List<String>? humanReadableCheckboxesValues;
  final String routeToGoWhenSave;
  final Future<List<AutomationRadioModel>> Function()? getOptions;
  final Future<List<AutomationCheckboxModel>> Function()? getCheckboxes;

  const AutomationInputView({
    super.key,
    required this.type,
    required this.label,
    this.placeholder,
    this.options,
    this.checkboxes,
    this.onSave,
    this.onCheckboxesSave,
    this.value,
    this.checkboxesValues,
    this.humanReadableValue,
    this.humanReadableCheckboxesValues,
    required this.routeToGoWhenSave,
    this.getOptions,
    this.getCheckboxes,
  });

  @override
  State<AutomationInputView> createState() => _AutomationInputViewState();
}

class _AutomationInputViewState extends State<AutomationInputView> {
  late String localValue;
  late List<String> checkboxesValues;
  late String humanReadableValue;
  late List<String> humanReadableCheckboxesValues;

  @override
  void initState() {
    super.initState();
    localValue = widget.value ?? "";
    checkboxesValues = widget.checkboxesValues ?? [];
    humanReadableValue = widget.humanReadableValue ?? "";
    humanReadableCheckboxesValues = widget.humanReadableCheckboxesValues ?? [];
  }

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: 'Edit ${widget.label}',
      getBack: true,
      body: Padding(
        padding: const EdgeInsets.all(4.0),
        child: Column(
          children: [
            Expanded(child: _buildInput()),
            _OKButton(
              onSave: widget.onSave,
              onCheckboxesSave: widget.onCheckboxesSave,
              value: localValue,
              checkboxesValues: checkboxesValues,
              humanReadableValue: humanReadableValue,
              humanReadableCheckboxesValues: humanReadableCheckboxesValues,
              routeToGoWhenSave: widget.routeToGoWhenSave,
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildInput() {
    switch (widget.type) {
      case AutomationInputEnum.text:
        return _TextInput(
          label: widget.label,
          placeholder: widget.placeholder,
          defaultValue: localValue,
          onValueChanged: (value) {
            setState(() {
              localValue = value;
              humanReadableValue = value;
            });
          },
        );
      case AutomationInputEnum.textArea:
        return _TextAreaInput(
          label: widget.label,
          placeholder: widget.placeholder,
          defaultValue: localValue,
          onChanged: (value) {
            setState(() {
              localValue = value;
              humanReadableValue = value;
            });
          },
        );
      case AutomationInputEnum.radio:
        return _RadioInput(
          label: widget.label,
          options: widget.options,
          defaultValue: localValue,
          onChanged: (value, humanValue) {
            setState(() {
              localValue = value;
              humanReadableValue = humanValue;
            });
          },
          getOptions: widget.getOptions,
        );
      case AutomationInputEnum.emoji:
        return _EmojiInput(
          label: widget.label,
          placeholder: widget.placeholder ?? 'Enter an emoji',
          defaultValue: localValue,
          onValueChanged: (value) {
            setState(() {
              localValue = value;
              humanReadableValue = value;
            });
          },
        );
      default:
        return Container();
    }
  }
}

class _TextInput extends StatefulWidget {
  final String label;
  final String? placeholder;
  final void Function(String) onValueChanged;
  final String defaultValue;

  const _TextInput({
    required this.label,
    this.placeholder,
    required this.onValueChanged,
    required this.defaultValue,
  });

  @override
  State<_TextInput> createState() => _TextInputState();
}

class _TextInputState extends State<_TextInput> {
  late TextEditingController _controller;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController(text: widget.defaultValue);
    _controller.addListener(() {
      widget.onValueChanged(_controller.text);
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      controller: _controller,
      placeholder: widget.placeholder,
      keyboardType: TextInputType.text,
      backgroundColor: Colors.white,
    );
  }
}

class _TextAreaInput extends StatefulWidget {
  final String label;
  final String? placeholder;
  final String? defaultValue;
  final void Function(String)? onChanged;

  const _TextAreaInput({
    required this.label,
    this.placeholder,
    this.defaultValue,
    this.onChanged,
  });

  @override
  State<_TextAreaInput> createState() => _TextAreaInputState();
}

class _TextAreaInputState extends State<_TextAreaInput> {
  late TextEditingController _controller;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController(text: widget.defaultValue);
    _controller.addListener(() {
      if (widget.onChanged != null) {
        widget.onChanged!(_controller.text);
      }
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      controller: _controller,
      placeholder: widget.placeholder,
      keyboardType: TextInputType.multiline,
      padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
      backgroundColor: Colors.white,
      maxLines: 5,
    );
  }
}

class _OKButton extends StatelessWidget {
  final void Function(String, String)? onSave;
  final void Function(List<String>, List<String>)? onCheckboxesSave;
  final String value;
  final List<String> checkboxesValues;
  final String humanReadableValue;
  final List<String> humanReadableCheckboxesValues;
  final String routeToGoWhenSave;

  const _OKButton({
    required this.onSave,
    required this.onCheckboxesSave,
    required this.value,
    required this.checkboxesValues,
    required this.humanReadableValue,
    required this.humanReadableCheckboxesValues,
    required this.routeToGoWhenSave,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(top: 8.0),
      child: Row(
        children: [
          Expanded(
            child: TriggoButton(
              text: "OK",
              padding:
                  const EdgeInsets.symmetric(horizontal: 20.0, vertical: 12.0),
              onPressed: () {
                if (onCheckboxesSave != null) {
                  onCheckboxesSave!(
                      checkboxesValues, humanReadableCheckboxesValues);
                } else {
                  onSave!(value, humanReadableValue);
                }
                if (routeToGoWhenSave == RoutesNames.popOneTime) {
                  Navigator.of(context).pop();
                } else if (routeToGoWhenSave == RoutesNames.popTwoTimes) {
                  Navigator.of(context)
                    ..pop()
                    ..pop();
                } else if (routeToGoWhenSave == RoutesNames.popThreeTimes) {
                  Navigator.of(context)
                    ..pop()
                    ..pop()
                    ..pop();
                } else {
                  Navigator.of(context).pushNamedAndRemoveUntil(
                    routeToGoWhenSave,
                    (route) => route.settings.name == routeToGoWhenSave,
                  );
                }
              },
            ),
          ),
        ],
      ),
    );
  }
}

class _RadioInput extends StatefulWidget {
  final String label;
  final List<AutomationRadioModel>? options;
  final void Function(String, String) onChanged;
  final String defaultValue;
  final Future<List<AutomationRadioModel>> Function()? getOptions;

  const _RadioInput({
    required this.label,
    this.options,
    required this.onChanged,
    required this.defaultValue,
    this.getOptions,
  });

  @override
  State<_RadioInput> createState() => _RadioInputState();
}

class _RadioInputState extends State<_RadioInput> {
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

class _NumberInput extends StatefulWidget {
  final String label;
  final String? placeholder;
  final void Function(String) onValueChanged;
  final String defaultValue;

  const _NumberInput({
    required this.label,
    this.placeholder,
    required this.onValueChanged,
    required this.defaultValue,
  });

  @override
  State<_NumberInput> createState() => _NumberInputState();
}

class _NumberInputState extends State<_NumberInput> {
  late TextEditingController _controller;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController(text: widget.defaultValue);
    _controller.addListener(() {
      widget.onValueChanged(_controller.text);
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      controller: _controller,
      placeholder: widget.placeholder,
      keyboardType: TextInputType.number,
      backgroundColor: Colors.white,
    );
  }
}

class _EmojiInput extends StatefulWidget {
  final String label;
  final String placeholder;
  final void Function(String) onValueChanged;
  final String defaultValue;

  const _EmojiInput({
    required this.label,
    required this.placeholder,
    required this.onValueChanged,
    required this.defaultValue,
  });

  @override
  State<_EmojiInput> createState() => _EmojiInputState();
}

class _EmojiInputState extends State<_EmojiInput> {
  late TextEditingController _controller;

  bool isSingleEmoji(String input) {
    final RegExp regexEmoji = RegExp(
        r'^(\u00a9|\u00ae|[\u2000-\u3300]|\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff])$');
    return regexEmoji.hasMatch(input);
  }

  void _validateInput(String input) {
    final graphemes = input.characters.toList();

    if (input.isEmpty) {
      widget.onValueChanged(input);
      return;
    }

    if (graphemes.length == 1 && isSingleEmoji(graphemes.first)) {
      widget.onValueChanged(input);
    } else {
      ScaffoldMessenger.of(context)
        ..removeCurrentSnackBar()
        ..showSnackBar(SnackBar(
          content: const Text('Please enter a single emoji'),
        ));
      _controller.text = widget.defaultValue;
      _controller.selection =
          TextSelection.collapsed(offset: _controller.text.characters.length);
    }
  }

  @override
  void initState() {
    super.initState();
    print(widget.defaultValue);
    _controller = TextEditingController(text: widget.defaultValue);

    _controller.addListener(() {
      final input = _controller.text;
      final graphemes = input.characters.toList();

      if (graphemes.length > 1) {
        _controller.text = graphemes.first;
        _controller.selection =
            TextSelection.collapsed(offset: _controller.text.characters.length);
      } else {
        _validateInput(input);
      }
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      controller: _controller,
      placeholder: widget.placeholder,
      keyboardType: TextInputType.text,
      backgroundColor: Colors.white,
    );
  }
}
