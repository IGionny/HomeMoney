export interface IResultModel<T> {
  Value: T;
  Messages: IResultMessageModel[]
  IsValid: boolean;
}

export interface IResultMessageModel {
  Level: number;
  Message: string;
  Property: string;
  Code: number;
}
