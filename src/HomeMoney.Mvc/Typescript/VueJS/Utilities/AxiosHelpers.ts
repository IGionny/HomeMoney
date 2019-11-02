import {IPagedRequest} from "../../@types/IPagedRequest";
import {IPagedResponse} from "../../@types/IPagedResponse";
import axios, {AxiosResponse} from 'axios';
import {IResultModel} from "../../@types/IResultModel";

export async function GetPaged<T>(request: IPagedRequest, entityName: string): Promise<IPagedResponse<T>> {
  try {
    let result: AxiosResponse<IPagedResponse<T>> = await axios.request<IPagedResponse<T>>({
      method: 'POST',
      url: '/Api/' + entityName + '/Paged',
      data: request
    });
    return result.data;
  } catch (e) {
    //todo: show a Message Error
    throw(e);
  }
}

export async function SaveAsync<T>(item: T, entityName: string): Promise<IResultModel<T>> {
  try {
    let result = await axios.request<IResultModel<T>>({
      method: 'POST',
      url: '/Api/' + entityName,
      data: item
    });
    return result.data;
  } catch (e) {
    //todo: show a Message Error
    throw(e);
  }
}

export async function GetAsync<T>(id: string, entityName: string): Promise<T> {
  try {
    let result = await axios.request<T>({
      method: 'GET',
      url: '/Api/' + entityName + "/" + id
    });
    return result.data;
  } catch (e) {
    //todo: show a Message Error
    debugger;
    throw(e);
  }
}
