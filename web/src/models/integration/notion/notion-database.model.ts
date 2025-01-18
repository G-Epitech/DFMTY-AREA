export class NotionDatabaseModel {
  id: string;
  title: string;
  description: string | null;
  icon: string | null;
  uri: string;

  constructor(
    id: string,
    title: string,
    description: string | null,
    icon: string | null,
    uri: string
  ) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.icon = icon;
    this.uri = uri;
  }
}
