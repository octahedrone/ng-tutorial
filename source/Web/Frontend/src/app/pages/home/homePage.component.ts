import {Component, OnInit} from '@angular/core';
import {AdventureState, HomePageService} from "../../services/homePage.service";

@Component({
  selector: 'home-page',
  templateUrl: './homePage.component.html'
})
export class HomePageComponent implements OnInit {
  public state: HomePageState | null = null;

  constructor(private service: HomePageService) {
  }

  ngOnInit() {
    this.service.loadState().subscribe(x =>
      this.state = {
        scenarioIsPresent: x.scenarioIsPresent,
        adventureLogIsPresent: x.adventureState === AdventureState.Finished || x.adventureState === AdventureState.Pending,
        activeAdventureIsPresent: x.adventureState === AdventureState.Pending
      })
  }
}

export interface HomePageState {
  scenarioIsPresent: boolean;
  activeAdventureIsPresent: boolean;
  adventureLogIsPresent: boolean;
}
