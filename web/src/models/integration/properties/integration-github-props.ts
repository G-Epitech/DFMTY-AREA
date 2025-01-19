export interface IntegrationGithubProps {
  id: string;
  name: string;
  email: string | null;
  bio: string | null;
  avatarUri: string;
  profileUri: string;
  company: string | null;
  blog: string | null;
  location: string | null;
  followers: number;
  following: number;
}
