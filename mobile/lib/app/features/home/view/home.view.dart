import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/env.dart';
import 'package:triggo/utils/launch_url.dart';

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
                  padding: const EdgeInsets.symmetric(
                      vertical: 8.0, horizontal: 4.0),
                  child: TriggoCard(
                      customWidget: Column(
                    children: [
                      _DocumentationTitle(),
                      SizedBox(height: 8.0),
                      _DocumentationText(),
                      SizedBox(height: 16.0),
                      _DocumentationButton(),
                    ],
                  ))),
            ),
          ],
        ),
      ],
    );
  }
}

class _DocumentationTitle extends StatelessWidget {
  const _DocumentationTitle();

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        SvgPicture.asset(
          './assets/icons/info_regular.svg',
          height: 40.0,
          width: 40.0,
          colorFilter: ColorFilter.mode(
              Theme.of(context).colorScheme.primary, BlendMode.srcIn),
        ),
        SizedBox(width: 8.0),
        Expanded(
          child: Text(
            'Documentation',
            style: Theme.of(context).textTheme.titleMedium,
          ),
        ),
      ],
    );
  }
}

class _DocumentationText extends StatelessWidget {
  const _DocumentationText();

  @override
  Widget build(BuildContext context) {
    return Text(
      'You don\'t know how to use the application, or you want to discover other usages ?\nGo and have a look at the documentation.',
      style: Theme.of(context).textTheme.labelLarge,
    );
  }
}

class _DocumentationButton extends StatelessWidget {
  const _DocumentationButton();

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Expanded(
          child: TriggoButton(
            text: 'Documentation',
            onPressed: () {
              launchURL('${Env.webUrl}/faq');
            },
          ),
        ),
      ],
    );
  }
}
