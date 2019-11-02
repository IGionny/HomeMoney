import {IUserEntity} from "./IUserEntity";

export interface ICategory extends IUserEntity {
  Name: string | null;
  Color: string | null;
  Icon: string | null;
}
