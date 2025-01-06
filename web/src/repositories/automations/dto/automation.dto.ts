import { TriggerShortDTO } from '@repositories/automations/dto/trigger-short.dto';
import { ActionShortDTO } from '@repositories/automations/dto/action-short.dto';

export interface AutomationDTO {
  id: string;
  label: string;
  description: string;
  ownerId: string;
  trigger: TriggerShortDTO;
  actions: ActionShortDTO[];
  enabled: boolean;
  updatedAt: Date;
}
