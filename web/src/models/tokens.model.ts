import { DecodedTokenType } from '../types';

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

  isAccessTokenValid(): boolean {
    if (!this.#accessToken) {
      return false;
    }

    try {
      return !this.isTokenExpired(this.#accessToken);
    } catch {
      return false;
    }
  }

  get userId(): string | null {
    if (!this.#accessToken) {
      return null;
    }

    try {
      const payload = this.#decodeJwtPayload(this.#accessToken);
      return payload.sub;
    } catch {
      return null;
    }
  }

  #decodeJwtPayload(token: string): DecodedTokenType {
    const payload = token.split('.')[1];
    const payloadDecoded = atob(payload.replace(/-/g, '+').replace(/_/g, '/'));
    return JSON.parse(payloadDecoded) as DecodedTokenType;
  }

  isTokenExpired(token: string): boolean {
    try {
      const payload = this.#decodeJwtPayload(token);
      const currentTime = Math.floor(Date.now() / 1000);

      if (!payload.exp) {
        return false;
      }
      return payload.exp < currentTime;
    } catch (error) {
      console.error('Error checking token expiration:', error);
      return true;
    }
  }
}
