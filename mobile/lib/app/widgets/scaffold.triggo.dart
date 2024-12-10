import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';

import 'banner.triggo.dart';

class BaseScaffold extends StatelessWidget {
  final Widget body;
  final String title;
  final Widget? header;
  final bool getBack;

  const BaseScaffold(
      {super.key,
      required this.body,
      required this.title,
      this.getBack = false,
      this.header});

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
                _PageTitle(title: title, getBack: getBack, header: header),
                const SizedBox(height: 4.0),
                _MainContainer(body: body),
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
  final Widget? header;
  const _PageTitle({required this.title, this.getBack = false, this.header});

  @override
  Widget build(BuildContext context) {
    if (header != null) {
      return header!;
    } else if (!getBack) {
      return Align(
        alignment: Alignment.centerLeft,
        child: Text(
          title,
          style: Theme.of(context).textTheme.titleLarge,
          maxLines: 1,
          overflow: TextOverflow.ellipsis,
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
            style: Theme.of(context).textTheme.titleMedium,
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
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
