import 'package:flutter/material.dart';

class Automation {
  final String name;
  final String description;
  final String iconUri;
  final Color iconColor;
  final bool isActive;

  Automation(
      {required this.name,
      required this.description,
      required this.iconUri,
      required this.iconColor,
      required this.isActive});
}
