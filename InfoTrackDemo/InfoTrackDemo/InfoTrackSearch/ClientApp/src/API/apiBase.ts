export default class ApiBase {
  public static async ajax (
    method: string,
    path: string,
    data: any
  ){
    const headers: any = {
      "content-type": "application/json",
      accept: "application/json",
    };

    const request: RequestInit = {
      headers,
      method,
      mode:"cors",
    };

    if(data) request.body = JSON.stringify(data);
    
    return new Promise<any>((resolve, reject) => {
      fetch(path, request)
        .then(async (response) => {
          let responseBody: any = await response.text();
          try{
            responseBody = JSON.parse(responseBody);
          } catch {}
          if(response.ok) {
            resolve(responseBody);
          } else {
            reject ({
              status: response.status,
              statusText: response.statusText,
              body: responseBody,
            } as ExceptionInformation);
          }
        })
        .catch((e) => {
          if(e.name === "AbortError") return;
          reject(e);
        });
    });
  }
}