export interface QueryResponse<TResult> {
  result: TResult;
  errorMessage: string;
  success: boolean;
}
