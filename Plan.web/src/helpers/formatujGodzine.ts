import moment from "moment";

export default function formatujGodzine(godzina: string) {
  return moment("1970-01-01:" + godzina).format("hh:mm");
}
