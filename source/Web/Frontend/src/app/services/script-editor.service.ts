import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";

@Injectable({providedIn: "root"})
export class AdventureScriptEditorService {
  constructor(private readonly http: HttpClient) {
  }

  loadScript = (): Observable<GetScriptResponse> => this.http.get<GetScriptResponse>("script/edit");

  saveScript = (script: string): Observable<any> =>
    this.http.post("script/edit", {script: script});
}

export interface GetScriptResponse {
  script: string;
}
