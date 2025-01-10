export class GoogleAuthConfigurationModel {
  readonly scopes: string[];
  readonly clientId: string;
  readonly endpoint: string;

  constructor(scopes: string[], clientId: string, endpoint: string) {
    this.scopes = scopes;
    this.clientId = clientId;
    this.endpoint = endpoint;
  }
}
