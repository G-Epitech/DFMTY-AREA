import { PageDTO } from '@repositories/utils/dto/page.dto';
import { IntegrationDTO } from '@repositories/integrations/dto/integration.dto';

export type UserIntegrationsGetResponseDTO = PageDTO<IntegrationDTO>;
