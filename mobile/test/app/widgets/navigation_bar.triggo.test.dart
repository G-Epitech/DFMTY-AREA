import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';

void triggoNavigationBarTest() {
  testWidgets('The TriggoNavigationBar should be rendered correctly',
      (WidgetTester tester) async {
    await tester.pumpWidget(const MaterialApp(
        home: Scaffold(
      body: TriggoNavigationBar(),
    )));

    final navigationBar = find.byType(TriggoNavigationBar);
    expect(navigationBar, findsOneWidget);

    final card = find.byType(Card);
    expect(card, findsNWidgets(3));

    final gesture = find.byType(GestureDetector);
    expect(gesture, findsNWidgets(3));

    final svg = find.byType(SvgPicture);
    expect(svg, findsNWidgets(3));
  });
}
