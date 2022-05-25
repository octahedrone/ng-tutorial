import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";

@Injectable({providedIn: "root"})
export class HomePageService {
  constructor(private readonly http: HttpClient) {
  }

  loadState = (): Observable<HomeScreenData> => this.http.get<HomeScreenData>("home");

  startAdventure = (): Observable<Object> => this.http.post('adventure/start', {});
}

export enum AdventureState {
  Impossible = 'Impossible',
  NotStarted = 'NotStarted',
  Pending = 'Pending',
  Finished = 'Finished'
}

export interface HomeScreenData {
  scenarioIsPresent: boolean;
  adventureState: AdventureState;
}
