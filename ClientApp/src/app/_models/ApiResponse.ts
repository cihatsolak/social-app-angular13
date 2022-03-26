export class ApiResponse<TModel> {
  statusCode!: number;
  message!: string;
  data!: TModel;
}
