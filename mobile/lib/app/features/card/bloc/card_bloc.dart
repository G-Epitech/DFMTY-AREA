import 'package:bloc/bloc.dart';

part 'card_event.dart';

class CardBloc extends Bloc<CardEvent, bool> {
  CardBloc() : super(false) {
    on<CardEvent>((event, emit) {
      switch (event) {
        case CardEvent.press:
          emit(true);
          break;
        case CardEvent.release:
          emit(false);
          break;
      }
    });
  }
}
