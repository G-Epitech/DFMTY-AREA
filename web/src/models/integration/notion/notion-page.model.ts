export class NotionPageModel {
  id: string;
  title: string;
  icon: string | null;
  uri: string;

  constructor(id: string, title: string, icon: string | null, uri: string) {
    this.id = id;
    this.title = title;
    this.icon = icon;
    this.uri = uri;
  }
}
