export interface Blad {
  Status: number;
  Tresc: string;
}

const url = "http://localhost:60211";

async function handleResponse(response: Response) {
  const text = await response.text();
  const data = text ? JSON.parse(text) : null;
  if (!response.ok) {
    let error = data.detail;
    return Promise.reject({ Status: response.status, Tresc: error });
  }
  return data;
}

async function POST(path: string, body: any) {
  const bodyValue = body != null ? JSON.stringify(body) : null;
  const response = await fetch(url + path, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: bodyValue,
  });
  return handleResponse(response);
}

export const httpClient = {
  POST,
};
