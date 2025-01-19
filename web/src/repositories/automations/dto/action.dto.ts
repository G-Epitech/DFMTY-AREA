export interface ActionDTO {
  identifier: string;
  parameters: {
    type: string;
    identifier: string;
    value: string;
  }[];
  dependencies: string[];
}
