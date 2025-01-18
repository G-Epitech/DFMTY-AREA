import { AutomationSchemaTrigger, TriggerModel } from '@models/automation';
import { PartialStateUpdater } from '@ngrx/signals';

export interface SelectTriggerSheetState {
  selectedTrigger: AutomationSchemaTrigger | null;
  trigger: TriggerModel | null;
}

export function stateUpdaterSelectTrigger(
  trigger: AutomationSchemaTrigger
): PartialStateUpdater<SelectTriggerSheetState> {
  return (state: SelectTriggerSheetState) => {
    return {
      ...state,
      selectedTrigger: trigger,
    };
  };
}
