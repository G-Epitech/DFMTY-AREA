import { AutomationStepSelectionStep } from '@features/automations/workspace/components/edit-sheets/edit-sheet.types';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';

export interface EditSheetStateInterface {
  selectionStep: AutomationStepSelectionStep;
  stepHistory: AutomationStepSelectionStep[];
  selectedIntegration: AvailableIntegrationType | null;
  selectedLinkedIntegration: IntegrationModel | null;
}
