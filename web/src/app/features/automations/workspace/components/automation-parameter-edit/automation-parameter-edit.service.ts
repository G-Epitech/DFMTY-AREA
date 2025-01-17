import { Injectable, Type } from '@angular/core';
import { AutomationParameterType } from '@models/automation';
import {
  PARAMETER_EDIT_COMPONENT_MAP,
  PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';

@Injectable()
export class AutomationParameterEditService {
  getParameterEditComponents(
    parameterIdentifier: string,
    parameterType: AutomationParameterType
  ): Type<unknown>[] {
    const components: Type<unknown>[] = [];

    components.push(PARAMETER_EDIT_COMPONENT_MAP[parameterType]);
    if (
      parameterIdentifier in PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP
    ) {
      components.push(
        PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP[parameterIdentifier]
      );
    }
    return components;
  }
}
