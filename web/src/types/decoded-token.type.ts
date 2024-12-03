export interface DecodedTokenType {
  sub: string;
  given_name: string;
  family_name: string;
  jti: string;
  token_type: string;
  exp: number;
  iss: string;
  aud: string;
}
