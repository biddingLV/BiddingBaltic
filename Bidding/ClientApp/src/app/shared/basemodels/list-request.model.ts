export interface IListRequest {
  SortByColumn: string;
  SortingDirection: string;
  SearchValue: string;
  // AllData?: boolean;
  OffsetEnd: number;
  OffsetStart: number;
}
