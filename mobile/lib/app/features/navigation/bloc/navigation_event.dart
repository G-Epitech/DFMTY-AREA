import 'package:equatable/equatable.dart';

abstract class NavigationEvent extends Equatable {
  const NavigationEvent();

  @override
  List<Object> get props => [];
}

enum PageIndex { home, page1 }

class NavigateToPage extends NavigationEvent {
  final PageIndex pageIndex;

  const NavigateToPage(this.pageIndex);

  @override
  List<Object> get props => [pageIndex];
}
