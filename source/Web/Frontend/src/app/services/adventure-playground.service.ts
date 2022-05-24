import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {QueryResponse} from "./service.models";

@Injectable({providedIn: "root"})
export class AdventurePlaygroundService {
  constructor(private readonly http: HttpClient) {
  }

  getCurrentStep = (): Observable<QueryResponse<AdventureStep>> =>
    this.http.get<QueryResponse<AdventureStep>>("adventure/current-step");
}

export interface AdventureStep {
  id: number;
  text: string;
  options: AdventureStepOption[]
}

export interface AdventureStepOption {
  id: number;
  text: string;
}
