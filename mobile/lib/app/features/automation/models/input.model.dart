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

enum AutomationParameterType {
  restrictedRadio,
  restrictedRadioBlocked,
  number,
  emoji,
  choice,
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

class AutomationParameterModel {
  late final String value;
  late final String type;

  AutomationParameterModel({
    required this.value,
    required this.type,
  });
}
