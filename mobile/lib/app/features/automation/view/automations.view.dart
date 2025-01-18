import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automations/automations_bloc.dart';
import 'package:triggo/app/features/automation/view/singleton/main.view.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/routes/route_observer.router.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationsView extends StatefulWidget {
  const AutomationsView({super.key});

  @override
  State<AutomationsView> createState() => _AutomationsPageState();
}

class _AutomationsPageState extends State<AutomationsView> with RouteAware {
  late AutomationsBloc _automationsBloc;

  @override
  void didPopNext() {
    _automationsBloc.add(ReloadAutomations());
  }

  @override
  void initState() {
    super.initState();

    final automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    _automationsBloc = AutomationsBloc(automationMediator: automationMediator)
      ..add(LoadAutomations());
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    routeObserver.subscribe(this, ModalRoute.of(context) as PageRoute<dynamic>);
  }

  @override
  void dispose() {
    routeObserver.unsubscribe(this);
    _automationsBloc.close();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocProvider.value(
      value: _automationsBloc,
      child: BaseScaffold(
        title: 'Automations',
        body: BlocBuilder<AutomationsBloc, AutomationsState>(
          builder: (context, state) {
            return _StateManager(
              state: state,
            );
          },
        ),
      ),
    );
  }
}

class _StateManager extends StatelessWidget {
  final AutomationsState state;

  const _StateManager({required this.state});

  @override
  Widget build(BuildContext context) {
    if (state is AutomationsLoading) {
      return Center(child: CircularProgressIndicator());
    } else if (state is AutomationsLoaded) {
      return _AutomationContainer(
        automations: (state as AutomationsLoaded).automations,
      );
    } else if (state is AutomationsError) {
      return _ErrorView(error: (state as AutomationsError).message);
    } else {
      return const _NoDataView();
    }
  }
}

class _AutomationContainer extends StatelessWidget {
  final List<Automation> automations;

  const _AutomationContainer({required this.automations});

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
                child: _AutomationButton(),
              ),
            ),
          ],
        ),
        Expanded(child: _AutomationViewContent(automations: automations)),
      ],
    );
  }
}

class _AutomationButton extends StatelessWidget {
  const _AutomationButton();

  @override
  Widget build(BuildContext context) {
    return TriggoButton(
      text: "Create Automation",
      onPressed: () {
        try {
          // automationMediator.createAutomation();
        } catch (e) {
          ScaffoldMessenger.of(context)
            ..removeCurrentSnackBar()
            ..showSnackBar(
                SnackBar(content: Text('Could not create automation')));
        }
        Navigator.pushNamed(context, RoutesNames.automationCreation);
      },
    );
  }
}

class _ErrorView extends StatelessWidget {
  final Object error;

  const _ErrorView({required this.error});

  @override
  Widget build(BuildContext context) {
    return Text('Error: $error');
  }
}

class _NoDataView extends StatelessWidget {
  const _NoDataView();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Text('No automation',
          textAlign: TextAlign.center,
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _AutomationViewContent extends StatelessWidget {
  final List<Automation> automations;

  const _AutomationViewContent({required this.automations});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: automations.length,
      itemBuilder: (context, index) {
        return _AutomationListItem(automation: automations[index]);
      },
    );
  }
}

class _AutomationListItem extends StatelessWidget {
  final Automation automation;

  const _AutomationListItem({required this.automation});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        Navigator.push(context,
            customScreenBuilder(AutomationMainView(automation: automation)));
      },
      child: TriggoCard(
        customWidget: Row(
          children: [
            Stack(
              clipBehavior: Clip.none,
              children: [
                Container(
                  width: 60,
                  height: 60,
                  decoration: BoxDecoration(
                    color: Color(automation.iconColor),
                    borderRadius: BorderRadius.circular(12.0),
                  ),
                  child: Center(
                    child: SvgPicture.asset(
                      automation.iconUri,
                      width: 30,
                      height: 30,
                      colorFilter: ColorFilter.mode(
                        Colors.white,
                        BlendMode.srcIn,
                      ),
                    ),
                  ),
                ),
                Positioned(
                  bottom: -5,
                  right: -5,
                  child: _ActivityIcon(state: automation.enabled),
                )
              ],
            ),
            SizedBox(width: 10),
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    children: [
                      Expanded(
                        child: Text(
                          automation.label,
                          style: Theme.of(context).textTheme.labelLarge,
                          maxLines: 1,
                          overflow: TextOverflow.ellipsis,
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 5),
                  Row(
                    children: [
                      Expanded(
                        child: Text(
                          automation.description,
                          style: Theme.of(context).textTheme.labelMedium,
                          maxLines: 2,
                          overflow: TextOverflow.ellipsis,
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}

class _ActivityIcon extends StatelessWidget {
  final bool state;
  final Color color;

  _ActivityIcon({required this.state}) : color = _getColor(state);

  static Color _getColor(bool state) {
    return state ? Colors.green : Colors.grey;
  }

  @override
  Widget build(BuildContext context) {
    return CircleAvatar(
      radius: 10,
      backgroundColor: color,
    );
  }
}
