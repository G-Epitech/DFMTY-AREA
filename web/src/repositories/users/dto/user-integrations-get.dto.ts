import { PageDTO } from '@repositories/dto/page.dto';
import { IntegrationDTO } from '@repositories/dto';

export type UserIntegrationsGetResponseDTO = PageDTO<IntegrationDTO>;
