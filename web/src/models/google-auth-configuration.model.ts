export class GoogleAuthConfigurationModel {
  readonly #callbackUrl = 'oauth2/google';
  readonly #accessType = 'offline';
  readonly #responseType = 'code';
  readonly #prompt = 'consent';

  readonly scopes: string[];
  readonly clientId: string;
  readonly endpoint: string;

  constructor(scopes: string[], clientId: string, endpoint: string) {
    this.scopes = scopes;
    this.clientId = clientId;
    this.endpoint = endpoint;
  }

  get #redirectUri(): string {
    return `${window.location.origin}/${this.#callbackUrl}`;
  }

  constructAuthUrl(state: string): string {
    const params = new URLSearchParams({
      redirect_uri: this.#redirectUri,
      prompt: this.#prompt,
      response_type: this.#responseType,
      client_id: this.clientId,
      scope: this.scopes.join(' '),
      access_type: this.#accessType,
      state,
    });

    return `${this.endpoint}?${params.toString()}`;
  }
}
