import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class HomePageService {
  constructor(
    private readonly http: HttpClient) {
  }

  load = () => this.http.get<HomeScreenData>("home");
}

export interface HomeScreenData {
  scenarioIsPresent: boolean;
  activeAdventureIsPresent: boolean;
  adventureLogIsPresent: boolean;
}
