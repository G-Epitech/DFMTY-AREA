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
}
