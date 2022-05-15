import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import * as homePageActions from "./state/homePage.actions";
import {getHomePageState, HomePageState} from "./state/homePage.reducer";
import {Observable} from "rxjs";
import {State} from "../../state/app.state";

@Component({
  selector: 'home-page',
  templateUrl: './homePage.component.html'
})
export class HomePageComponent implements OnInit {
  public state$: Observable<HomePageState>;

  constructor(private store: Store<State>) {
    this.state$ = this.store.select(getHomePageState);
  }

  ngOnInit() {
    this.store.dispatch(homePageActions.initializeAction());
  }
}

