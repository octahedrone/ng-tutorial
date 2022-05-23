import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";

@Injectable({providedIn: "root"})
export class HomePageService {
  constructor(private readonly http: HttpClient) {
  }

  loadState = (): Observable<HomeScreenData> => this.http.get<HomeScreenData>("home");
}

export enum AdventureState {
  Impossible,
  NotStarted,
  Pending,
  Finished
}

export interface HomeScreenData {
  scenarioIsPresent: boolean;
  adventureState: AdventureState;
}
