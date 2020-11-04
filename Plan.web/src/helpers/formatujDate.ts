import moment from "moment";

export default function formatujDate(date: Date) {
  if (date) return moment(new Date(date).toISOString()).format("DD/MM/YYYY");
  return "";
}
