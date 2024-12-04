import 'package:flutter/material.dart';
import 'package:triggo/app/theme/colors/colors.dart';

TextStyle baseTextStyle = const TextStyle(
  fontFamily: 'Onest',
  color: textColor,
  letterSpacing: -0.2,
);

final TextStyle titleLarge = baseTextStyle.copyWith(
  fontFamily: 'Mada',
  fontSize: 40,
  fontWeight: FontWeight.w800, // Extra bold
);

final TextStyle titleMedium = baseTextStyle.copyWith(
  fontFamily: 'Mada',
  fontSize: 30,
  fontWeight: FontWeight.bold,
);

final TextStyle large = baseTextStyle.copyWith(
  fontSize: 15,
  fontWeight: FontWeight.w600, // Semi bold
);

final TextStyle medium = baseTextStyle.copyWith(
  fontSize: 10,
  fontWeight: FontWeight.w400, // Regular
);

final TextStyle small = baseTextStyle.copyWith(
  fontSize: 6,
  fontWeight: FontWeight.w400, // Regular
);

final TextTheme triggoTextTheme = TextTheme(
  titleLarge: titleLarge,
  titleMedium: titleMedium,
  labelLarge: large,
  labelMedium: medium,
  labelSmall: small,
);

final TextStyle subTitle = baseTextStyle.copyWith(
  fontSize: 22.5,
  fontWeight: FontWeight.w400, // Regular
  letterSpacing: 0,
);

final TextStyle containerTitle = baseTextStyle.copyWith(
  fontFamily: 'Inter',
  fontSize: 15,
  fontWeight: FontWeight.w500, // Medium
);
