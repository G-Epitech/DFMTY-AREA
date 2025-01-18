import { AutomationStepSelectionType } from '@features/automations/workspace/components/edit-sheets/edit-sheet.types';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';
import {
  ActionParameter,
  AutomationParameterValueType,
  TriggerParameter,
} from '@models/automation';

export interface EditSheetStateInterface {
  selectionStep: AutomationStepSelectionType;
  stepHistory: AutomationStepSelectionType[];
  selectedIntegration: AvailableIntegrationType | null;
  selectedLinkedIntegration: IntegrationModel | null;
  selectedParameter: TriggerParameter | ActionParameter | null;
  selecterParameterType: AutomationParameterValueType | null;
}
