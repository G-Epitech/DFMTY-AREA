import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

void triggoInputTest() {
  testWidgets('The TriggoInput should be rendered correctly',
      (WidgetTester tester) async {
    await tester.pumpWidget(const MaterialApp(
        home: Scaffold(
      body: TriggoInput(
        placeholder: 'Triggo',
        controller: null,
        keyboardType: TextInputType.text,
        onChanged: null,
        padding: EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
        obscureText: false,
      ),
    )));

    final textFieldFinder = find.byType(TextField);
    final titleFinder = find.text('Triggo');

    expect(textFieldFinder, findsOneWidget);
    expect(titleFinder, findsOneWidget);

    await tester.tap(textFieldFinder);
    await tester.pump();

    await tester.enterText(textFieldFinder, 'Hello, World!');
    await tester.pump();

    expect(find.text('Hello, World!'), findsOneWidget);
  });
}
