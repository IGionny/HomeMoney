import {IPagedRequest} from "../../@types/IPagedRequest";
import {IPagedResponse} from "../../@types/IPagedResponse";
import axios from 'axios';
export async function GetPaged<T>(request: IPagedRequest, entityName: string): Promise<IPagedResponse<T>> {
  try {
    let result = await axios.request<IPagedRequest, IPagedResponse<T>>({
      method: 'POST',
      url: '/Api/' + entityName + '/Paged',
      data: request
    })
    return result;
  } catch (e) {


    throw(e);
  }

}
