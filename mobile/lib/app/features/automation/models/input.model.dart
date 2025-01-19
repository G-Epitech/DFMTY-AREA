import 'package:flutter/material.dart';

enum AutomationInputType {
  text,
  textArea,
  number,
  radio,
  date,
  boolean,
  emoji,
}

enum AutomationParameterNeedOptions {
  yes,
  no,
  blocked,
  number,
}

class AutomationRadioModel {
  final String title;
  final String description;
  final String value;

  AutomationRadioModel({
    required this.title,
    required this.description,
    required this.value,
  });
}

class AutomationCheckboxModel {
  final String title;
  final String value;
  final String description;
  final Widget? widget;

  AutomationCheckboxModel({
    required this.title,
    required this.value,
    required this.description,
    this.widget,
  });
}
