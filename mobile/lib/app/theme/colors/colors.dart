import 'package:flutter/material.dart';

const ColorScheme triggoColorScheme = ColorScheme(
  primary: Color(0xFF913AEE),
  secondary: Color(0xFFFFFFFF),
  primaryContainer: Color(0xFF913AEE),
  secondaryContainer: Color(0xFFF6F6F6),
  surface: Color(0xFFFFFFFF),
  error: Color(0xFFFFFFFF),
  onPrimary: Color(0xFFFFFFFF),
  onSecondary: Color(0xFFFFFFFF),
  onPrimaryContainer: Color(0xFF5865F2),
  onSecondaryContainer: Color(0xFFFFFFFF),
  onSurface: Color(0xFF000000),
  onError: Color(0xFFDA4141),
  brightness: Brightness.light,
);

const Color textPrimaryColor = Color(0xFF3E244A);
const Color textSecondaryColor = Color(0xFF625169);
const Color lightContainer = Color(0xFFF4EBFD);

class HexColor extends Color {
  static int _getColorFromHex(String hexColor) {
    hexColor = hexColor.toUpperCase().replaceAll("#", "");
    if (hexColor.length == 6) {
      hexColor = "FF$hexColor";
    }
    return int.parse(hexColor, radix: 16);
  }

  HexColor(final String hexColor) : super(_getColorFromHex(hexColor));
}
