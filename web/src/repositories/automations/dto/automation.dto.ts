import { TriggerDTO } from '@repositories/automations/dto/trigger.dto';
import { ActionDTO } from '@repositories/automations/dto/action.dto';

export interface AutomationDTO {
  id: string;
  label: string;
  description: string;
  ownerId: string;
  trigger: TriggerDTO;
  actions: ActionDTO[];
  enabled: boolean;
  updatedAt: Date;
}
