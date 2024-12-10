import { PageDTO } from '@repositories/dto/page.dto';
import { AutomationDTO } from '@repositories/dto';

export type UserAutomationsGetResponseDTO = PageDTO<AutomationDTO>;
