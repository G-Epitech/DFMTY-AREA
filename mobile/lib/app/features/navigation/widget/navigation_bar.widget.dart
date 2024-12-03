import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/home/home.dart';
import 'package:triggo/app/features/navigation/bloc/navigation_bloc.dart';
import 'package:triggo/app/features/navigation/bloc/navigation_event.dart';
import 'package:triggo/app/features/navigation/bloc/navigation_state.dart';

class MainWidget extends StatelessWidget {
  const MainWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<NavigationBloc, NavigationState>(
      builder: (context, state) {
        if (state is PageState) {
          switch (state.pageIndex) {
            case PageIndex.home:
              return const MyHomePage(title: 'Test Page');
            default:
              return const MyHomePage(title: 'Triggo Page');
          }
        }
        return Container();
      },
    );
  }
}
