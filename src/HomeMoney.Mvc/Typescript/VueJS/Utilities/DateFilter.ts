/**
 * Usage: Vue.filter("FormatDate", (value: any) => FormatDateFilter(value));
 * */
import moment from "moment";

export function FormatDateFilter(value: any) {
  if (value === undefined || value === null) {
    return "";
  }
  if (typeof value === "object") {

  }
  if (typeof value === "string") {
    return moment(value).format("DD-MM-YYYY");
  }
  return "";
}


/**
 * Usage: Vue.filter("FormatDateTime", (value: any) => FormatDateTimeFilter(value));
 */
export function FormatDateTimeFilter(value: any) {
  if (value === undefined || value === null) {
    return "";
  }
  if (typeof value === "object") {

  }
  if (typeof value === "string") {
    return moment(value).format("DD-MM-YYYY HH:mm");
  }
  return "";
}

