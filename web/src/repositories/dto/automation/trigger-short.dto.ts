export interface TriggerShortDTO {
  id: string;
  identifier: string;
  parameters: { identifier: string; value: string }[];
  providers: string[];
}
