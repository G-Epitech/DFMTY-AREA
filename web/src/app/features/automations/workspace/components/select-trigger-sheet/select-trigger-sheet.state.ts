import {
  AutomationParameterType,
  AutomationSchemaTrigger,
  TriggerModel,
  TriggerParameter,
} from '@models/automation';
import { PartialStateUpdater } from '@ngrx/signals';

export interface SelectTriggerSheetState {
  selectedTrigger: AutomationSchemaTrigger | null;
  trigger: TriggerModel | null;
  selectedParameter: TriggerParameter | null;
  selecterParameterType: AutomationParameterType | null;
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
