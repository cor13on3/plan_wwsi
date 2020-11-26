export interface Blad {
  Status: number;
  Tresc: string;
}

const url =
  !process.env.NODE_ENV || process.env.NODE_ENV === "development"
    ? "http://localhost:60211"
    : "";

async function handleResponse(response: Response) {
  const text = await response.text();
  const data = text ? JSON.parse(text) : null;
  if (!response.ok) {
    if (response.status === 401) window.location.reload();
    else {
      let error = data.detail;
      return Promise.reject({ Status: response.status, Tresc: error });
    }
  }
  return data;
}

async function POST(path: string, body: any) {
  const bodyValue = body != null ? JSON.stringify(body) : null;
  try {
    const response = await fetch(url + path, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: bodyValue,
      credentials: "include",
    });
    return handleResponse(response);
  } catch (e) {
    return Promise.reject({
      Tresc: "Ups..coś poszło nie tak. Spróbuj jeszcze raz.",
    });
  }
}

async function PUT(path: string, body: any) {
  try {
    const bodyValue = body != null ? JSON.stringify(body) : null;
    const response = await fetch(url + path, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: bodyValue,
      credentials: "include",
    });
    return handleResponse(response);
  } catch (e) {
    return Promise.reject({
      Tresc: "Ups..coś poszło nie tak. Spróbuj jeszcze raz.",
    });
  }
}

async function GET(path: string) {
  try {
    const response = await fetch(url + path, {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      credentials: "include",
    });
    return handleResponse(response);
  } catch (e) {
    return Promise.reject({
      Tresc: "Ups..coś poszło nie tak. Spróbuj jeszcze raz.",
    });
  }
}

async function DELETE(path: string) {
  try {
    const response = await fetch(url + path, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      credentials: "include",
    });
    return handleResponse(response);
  } catch (e) {
    return Promise.reject({
      Tresc: "Ups..coś poszło nie tak. Spróbuj jeszcze raz.",
    });
  }
}

export const httpClient = {
  POST,
  PUT,
  GET,
  DELETE,
};
