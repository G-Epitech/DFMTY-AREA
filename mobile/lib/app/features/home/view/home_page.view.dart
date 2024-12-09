import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  final String title = 'Home Screen';

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: "Home Page",
      body: _HomeContainer(),
    );
  }
}

class _HomeContainer extends StatelessWidget {
  const _HomeContainer();

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          children: [
            Expanded(
              child: Padding(
                padding:
                    const EdgeInsets.symmetric(vertical: 8.0, horizontal: 4.0),
              ),
            ),
          ],
        ),
      ],
    );
  }
}
