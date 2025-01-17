import { Type } from '@angular/core';
import { AutomationParameterType } from '@models/automation';
import { AutomationParameterEditStringComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-string/automation-parameter-edit-string.component';
import { AutomationParameterEditIntegerComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-integer/automation-parameter-edit-integer.component';

export const PARAMETER_EDIT_COMPONENT_MAP: Record<
  AutomationParameterType,
  Type<unknown>
> = {
  [AutomationParameterType.STRING]: AutomationParameterEditStringComponent,
  [AutomationParameterType.INTEGER]: AutomationParameterEditIntegerComponent,
  [AutomationParameterType.BOOLEAN]: AutomationParameterEditStringComponent,
  [AutomationParameterType.DATETIME]: AutomationParameterEditStringComponent,
};

export const PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP: Record<
  string,
  Type<unknown>
> = {
  MessageReceivedInChannel: AutomationParameterEditStringComponent,
};
