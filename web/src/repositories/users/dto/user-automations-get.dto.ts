import { PageDTO } from '@repositories/utils/dto/page.dto';
import { AutomationDTO } from '@repositories/automations/dto';

export type UserAutomationsGetResponseDTO = PageDTO<AutomationDTO>;
