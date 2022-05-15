import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {HomePageService, HomeScreenData} from "../../../services/homePage.service";
import * as homePageActions from "./homePage.actions";
import {mergeMap, map} from "rxjs";

@Injectable()
export class HomePageEffects {
  constructor(private actions$: Actions,
              private service: HomePageService) {
  }

  loadState$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(homePageActions.initializeAction),
      mergeMap(() => this.service.loadState().pipe(
        map(response => homePageActions.initializedAction({...response}))
      ))
    );
  })
}
