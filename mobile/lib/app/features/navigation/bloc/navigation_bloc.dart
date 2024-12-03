import 'package:flutter_bloc/flutter_bloc.dart';

import 'navigation_event.dart';
import 'navigation_state.dart';

class NavigationBloc extends Bloc<NavigationEvent, NavigationState> {
  NavigationBloc() : super(const PageState(PageIndex.home)) {
    on<NavigateToPage>((event, emit) {
      emit(PageState(event.pageIndex));
    });
  }
}
