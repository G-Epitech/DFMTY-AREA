import {
  ActionModel,
  AutomationSchemaAction,
} from '@models/automation';
import { PartialStateUpdater } from '@ngrx/signals';

export interface SelectActionSheetState {
  selectedAction: AutomationSchemaAction | null;
  action: ActionModel | null;
}

export function stateUpdaterSelectAction(
  action: AutomationSchemaAction
): PartialStateUpdater<SelectActionSheetState> {
  return (state: SelectActionSheetState) => {
    return {
      ...state,
      selectedAction: action,
    };
  };
}
