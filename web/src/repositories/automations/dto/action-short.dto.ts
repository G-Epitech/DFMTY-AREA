export interface ActionShortDTO {
  identifier: string;
  parameters: {
    type: string;
    identifier: string;
    value: string;
  }[];
  dependencies: string[];
}
