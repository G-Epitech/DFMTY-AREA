import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class HomeView extends StatefulWidget {
  const HomeView({super.key});

  final String title = 'Home Screen';

  @override
  State<HomeView> createState() => _HomeViewState();
}

class _HomeViewState extends State<HomeView> {
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
