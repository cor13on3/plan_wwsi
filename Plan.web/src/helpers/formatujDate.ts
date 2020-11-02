import moment from "moment";

export default function formatujDate(date: Date) {
  return moment(date).format("DD/MM/YYYY");
}
