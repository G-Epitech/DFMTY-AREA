export class TokensModel {
  readonly #accessToken: string | null;
  readonly #refreshToken: string | null;

  constructor(accessToken: string | null, refreshToken: string | null) {
    this.#accessToken = accessToken;
    this.#refreshToken = refreshToken;
  }

  get accessToken(): string | null {
    return this.#accessToken;
  }

  get refreshToken(): string | null {
    return this.#refreshToken;
  }

  get isAccessTokenValid(): boolean {
    // TODO: Add more logic to check if the access token is valid once the api is ready
    return this.#accessToken !== null;
  }
}
