export interface TriggerShortModel {
  readonly identifier: string;
  readonly parameters: { identifier: string; value: string }[];
  readonly providers: string[];
}
