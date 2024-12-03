import 'package:equatable/equatable.dart';
import 'package:triggo/app/features/navigation/bloc/navigation_event.dart';

abstract class NavigationState extends Equatable {
  const NavigationState();

  @override
  List<Object> get props => [];
}

class PageState extends NavigationState {
  final PageIndex pageIndex;

  const PageState(this.pageIndex);

  @override
  List<Object> get props => [pageIndex];
}
