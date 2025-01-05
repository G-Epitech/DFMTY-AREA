import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:triggo/app/widgets/banner.triggo.dart';

void triggoBannerTest() {
  testWidgets('The TriggoBanner widget has no parameters',
      (WidgetTester tester) async {
    await tester.pumpWidget(const MaterialApp(
        home: Scaffold(
      body: TriggoBanner(),
    )));

    final titleFinder = find.byType(Text);
    final svgFinder = find.byType(SvgPicture);

    expect(titleFinder, findsOneWidget);
    expect(svgFinder, findsOneWidget);
  });
}
