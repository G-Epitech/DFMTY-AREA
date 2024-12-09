import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';

import 'banner.triggo.dart';

class BaseScaffold extends StatelessWidget {
  final Widget? body;
  final String title;
  final bool getBack;

  const BaseScaffold(
      {super.key, this.body, required this.title, this.getBack = false});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: SafeArea(
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 8.0),
            child: Column(
              children: [
                TriggoBanner(),
                const SizedBox(height: 16.0),
                _PageTitle(title: title, getBack: getBack),
                const SizedBox(height: 4.0),
                _MainContainer(body: body!),
              ],
            ),
          ),
        ),
      ),
      bottomNavigationBar: const TriggoNavigationBar(),
    );
  }
}

class _PageTitle extends StatelessWidget {
  final String title;
  final bool getBack;
  const _PageTitle({required this.title, this.getBack = false});

  @override
  Widget build(BuildContext context) {
    if (!getBack) {
      return Align(
        alignment: Alignment.centerLeft,
        child: Text(
          title,
          style: Theme.of(context).textTheme.titleLarge,
        ),
      );
    } else {
      return Row(
        children: [
          GestureDetector(
            onTap: () {
              Navigator.of(context).pop();
            },
            child: Icon(
              Icons.arrow_back,
              size: 26.0,
              weight: 2.0,
            ),
          ),
          SizedBox(width: 10.0),
          Text(
            title,
            style: Theme.of(context).textTheme.titleLarge,
          )
        ],
      );
    }
  }
}

class _MainContainer extends StatelessWidget {
  final Widget? body;
  const _MainContainer({required this.body});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Container(
        padding: const EdgeInsets.all(8.0),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(12.0),
          color: Colors.grey[200],
        ),
        child: body!,
      ),
    );
  }
}
