export interface BaseListRequestModel {
  sortByColumn: string;
  sortingDirection: string;
  searchValue: string;
  offsetStart: number;
  offsetEnd: number;
  currentPage: number;
}
