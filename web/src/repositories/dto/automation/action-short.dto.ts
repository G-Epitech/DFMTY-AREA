export interface ActionShortDTO {
  id: string;
  identifier: string;
  parameters: {
    type: string;
    identifier: string;
    value: string;
  }[];
  providers: string[];
}
