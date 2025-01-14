export class DiscordGuildModel {
  readonly id: string;
  readonly name: string;
  readonly iconUri: string;
  readonly approximateMemberCount: number;
  readonly linked: boolean;

  constructor(
    id: string,
    name: string,
    iconUri: string,
    approximateMemberCount: number,
    linked: boolean
  ) {
    this.id = id;
    this.name = name;
    this.iconUri = iconUri;
    this.approximateMemberCount = approximateMemberCount;
    this.linked = linked;
  }
}
