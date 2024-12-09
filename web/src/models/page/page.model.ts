export class PageModel<T> {
  readonly pageNumber: number;
  readonly pageSize: number;
  readonly totalPages: number;
  readonly totalRecords: number;
  readonly data: T[];

  constructor(
    pageNumber: number,
    pageSize: number,
    totalPages: number,
    totalRecords: number,
    data: T[]
  ) {
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
    this.totalPages = totalPages;
    this.totalRecords = totalRecords;
    this.data = data;
  }
}
