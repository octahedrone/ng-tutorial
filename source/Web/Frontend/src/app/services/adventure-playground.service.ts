import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {QueryResponse} from "./service.models";
import {AdventureState} from "./homePage.service";

@Injectable({providedIn: "root"})
export class AdventurePlaygroundService {
  constructor(private readonly http: HttpClient) {
  }

  getCurrentStep = (): Observable<QueryResponse<CurrentAdventureState>> =>
    this.http.get<QueryResponse<CurrentAdventureState>>("adventure/current-step");

  submitUserChoice = (stepId: number, optionId: number | null): Observable<QueryResponse<CurrentAdventureState>> =>
    this.http.post<QueryResponse<CurrentAdventureState>>("adventure/current-step", {
      stepId: stepId,
      optionId: optionId,
    });
}

export interface CurrentAdventureState {
  currentStepId: number;
  currentStepText: string;
  currentStepOptions: AdventureStepOption[]
}

export interface AdventureStepOption {
  id: number;
  text: string;
}
