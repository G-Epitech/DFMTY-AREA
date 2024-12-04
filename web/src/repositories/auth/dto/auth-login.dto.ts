export interface AuthLoginRequestDTO {
  email: string;
  password: string;
}

export interface AuthLoginResponseDTO {
  accessToken: string;
  refreshToken: string;
}
