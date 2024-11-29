export interface AuthRegisterRequestDTO {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

export interface AuthRegisterResponseDTO {
  accessToken: string;
  refreshToken: string;
}
