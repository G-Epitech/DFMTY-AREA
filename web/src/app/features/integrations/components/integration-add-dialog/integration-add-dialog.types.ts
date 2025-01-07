export interface IntegrationAvailableProps {
  name: string;
  iconUri: string;
  identifier: string;
  triggers: string[];
  actions: string[];
  color: string;
}

export type LinkFunction = () => void;
