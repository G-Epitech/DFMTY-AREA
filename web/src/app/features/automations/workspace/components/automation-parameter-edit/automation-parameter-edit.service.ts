import { Injectable, Type } from '@angular/core';
import { AutomationParameterType } from '@models/automation';
import {
  PARAMETER_EDIT_COMPONENT_MAP,
  PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP,
  ParameterEditDynamicComponent,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';

@Injectable()
export class AutomationParameterEditService {
  getParameterEditComponents(
    parameterIdentifier: string,
    parameterType: AutomationParameterType
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
