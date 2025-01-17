import { EventEmitter, Type } from '@angular/core';
import { AutomationParameterType } from '@models/automation';
import { AutomationParameterEditStringComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-string/automation-parameter-edit-string.component';
import { AutomationParameterEditIntegerComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-integer/automation-parameter-edit-integer.component';
import { AutomationParameterEditBooleanComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-boolean/automation-parameter-edit-boolean.component';
import { AutomationParameterEditDatetimeComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-datetime/automation-parameter-edit-datetime.component';
import { DiscordGuildIdParameterComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-plugins/discord-guild-id-parameter/discord-guild-id-parameter.component';
import {
  DiscordChannelIdParameterComponent
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-plugins/discord-channel-id-parameter/discord-channel-id-parameter.component';

export interface ParameterEditDynamicComponent {
  parameter: { identifier: string; value: string | null };
  parameterType: AutomationParameterType;
  valueChange?: EventEmitter<ParameterEditOutput>;
  integrationId?: string;
}

export interface ParameterEditOutput {
  rawValue: string;
}

export const PARAMETER_EDIT_COMPONENT_MAP: Record<
  AutomationParameterType,
  Type<ParameterEditDynamicComponent>
> = {
  [AutomationParameterType.STRING]: AutomationParameterEditStringComponent,
  [AutomationParameterType.INTEGER]: AutomationParameterEditIntegerComponent,
  [AutomationParameterType.BOOLEAN]: AutomationParameterEditBooleanComponent,
  [AutomationParameterType.DATETIME]: AutomationParameterEditDatetimeComponent,
};

export const PARAMETER_EDIT_INTEGRATION_SPECIFIC_COMPONENT_MAP: Record<
  string,
  Type<ParameterEditDynamicComponent>
> = {
  GuildId: DiscordGuildIdParameterComponent,
  ChannelId: DiscordChannelIdParameterComponent,
};
