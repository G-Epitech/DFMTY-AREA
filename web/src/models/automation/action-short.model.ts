export interface ActionShortModel {
  readonly id: string;
  readonly identifier: string;
  readonly parameters: { type: string; identifier: string; value: string }[];
  readonly providers: string[];
}
