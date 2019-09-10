import {IUserEntity} from "./IUserEntity";

export interface IAccount extends IUserEntity {
  Name: string | null;
  IsArchived: boolean;
  FirstBalance: number;
  FromAt: string | null;
  ToAt: string | null;
}
