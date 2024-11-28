export type AuthRegisterRequestDTO = {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

export type AuthRegisterResponseDTO = {
  accessToken: string;
  refreshToken: string;
}
