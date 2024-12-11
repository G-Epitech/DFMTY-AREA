import { TriggerShortDTO } from '@repositories/dto/automation/trigger-short.dto';
import { ActionShortDTO } from '@repositories/dto/automation/action-short.dto';

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
