import { PageOptions } from '@models/page';

export function pageOptionsToParams(pageOptions: PageOptions): string {
  const params = new URLSearchParams();
  params.set('page', pageOptions.page.toString());
  params.set('size', pageOptions.size.toString());
  return params.toString();
}
