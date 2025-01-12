import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:triggo/app/widgets/banner.triggo.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

void triggoScaffoldTest() {
  testWidgets(
      'The TriggoScaffold should be rendered correctly without back button',
      (WidgetTester tester) async {
    await tester.pumpWidget(const MaterialApp(
        home: Scaffold(
            body: BaseScaffold(
      title: 'Triggo Scaffold',
      body: Center(
        child: Text('Triggo Scaffold Body'),
      ),
    ))));

    expect(find.text('Triggo Scaffold'), findsOneWidget);
    expect(find.text('Triggo Scaffold Body'), findsOneWidget);
    expect(find.byType(TriggoBanner), findsOneWidget);
    expect(find.byType(TriggoNavigationBar), findsOneWidget);
    expect(find.byType(GestureDetector), findsNWidgets(4));
  });

  testWidgets(
      'The TriggoScaffold should be rendered correctly with back button',
      (WidgetTester tester) async {
    await tester.pumpWidget(const MaterialApp(
        home: Scaffold(
            body: BaseScaffold(
      title: 'Triggo Scaffold',
      body: Center(
        child: Text('Triggo Scaffold Body'),
      ),
      getBack: true,
    ))));

    expect(find.text('Triggo Scaffold'), findsOneWidget);
    expect(find.text('Triggo Scaffold Body'), findsOneWidget);
    expect(find.byType(TriggoBanner), findsOneWidget);
    expect(find.byType(TriggoNavigationBar), findsOneWidget);
    expect(find.byType(GestureDetector), findsNWidgets(5));
  });
}
