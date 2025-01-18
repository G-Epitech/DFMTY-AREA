import { Injectable, signal, Type } from '@angular/core';
import { AutomationParameterValueType } from '@models/automation';
import {
  PARAMETER_EDIT_COMPONENT_MAP,
  PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP,
  ParameterEditDynamicComponent,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import {
  AutomationParameterFormatType
} from '@models/automation/automation-parameter-format-type';

@Injectable({ providedIn: 'root' })
export class AutomationParameterEditService {
  currentParameters = signal<
    {
      type: AutomationParameterFormatType;
      identifier: string;
      value: string | null;
    }[]
  >([]);

  getParameterEditComponents(
    parameterIdentifier: string,
    parameterType: AutomationParameterValueType
  ): Type<ParameterEditDynamicComponent> {
    if (
      parameterIdentifier in PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP
    ) {
      return PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP[
        parameterIdentifier
      ];
    } else {
      return PARAMETER_EDIT_COMPONENT_MAP[parameterType];
    }
  }
}
