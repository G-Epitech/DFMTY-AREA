export interface TriggerShortModel {
  readonly id: string;
  readonly identifier: string;
  readonly parameters: { identifier: string; value: string }[];
  readonly providers: string[];
}
