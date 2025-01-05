import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:triggo/app/widgets/button.triggo.dart';

void triggoButtonTest() {
  testWidgets('The TriggoButton should be rendered correctly',
      (WidgetTester tester) async {
    await tester.pumpWidget(const MaterialApp(
        home: Scaffold(
      body: TriggoButton(text: 'Triggo'),
    )));

    final titleFinder = find.text('Triggo');
    final buttonFinder = find.byType(ElevatedButton);

    expect(titleFinder, findsOneWidget);
    expect(buttonFinder, findsOneWidget);
  });
}
