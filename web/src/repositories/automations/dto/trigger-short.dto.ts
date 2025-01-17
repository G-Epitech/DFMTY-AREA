export interface TriggerShortDTO {
  identifier: string;
  parameters: { identifier: string; value: string }[];
  dependencies: string[];
}
