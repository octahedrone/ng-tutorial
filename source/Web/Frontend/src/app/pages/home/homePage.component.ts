import {Component, OnInit} from '@angular/core';
import {AdventureState, HomePageService} from "../../services/homePage.service";
import {Router} from "@angular/router";

@Component({
  selector: 'home-page',
  templateUrl: './homePage.component.html'
})
export class HomePageComponent implements OnInit {
  public state: HomePageState | null = null;

  constructor(private service: HomePageService,
              private router: Router) {
  }

  ngOnInit() {
    this.service.loadState().subscribe(x =>
      this.state = {
        scenarioIsPresent: x.scenarioIsPresent,
        adventureLogIsPresent: x.adventureState === AdventureState.Finished || x.adventureState === AdventureState.Pending,
        activeAdventureIsPresent: x.adventureState === AdventureState.Pending
      })
  }

  startAdventure() {
    this.service.startAdventure().subscribe( _=>{
      this.router.navigate(['play']);
    })
  }
}

export interface HomePageState {
  scenarioIsPresent: boolean;
  activeAdventureIsPresent: boolean;
  adventureLogIsPresent: boolean;
}
