export interface GoogleConfigurationDTO {
  scopes: string[];
  endpoint: string;
  clientIds: {
    provider: string;
    clientId: string;
  }[];
}
