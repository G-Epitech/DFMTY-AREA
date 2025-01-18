export interface TriggerDTO {
  identifier: string;
  parameters: { identifier: string; value: string }[];
  dependencies: string[];
}
