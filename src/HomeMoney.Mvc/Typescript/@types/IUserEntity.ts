import {IOwner} from "./IOwner";
import {IEntity} from "./IEntity";

export interface IUserEntity extends IEntity {
  Owner: IOwner | null;
}

