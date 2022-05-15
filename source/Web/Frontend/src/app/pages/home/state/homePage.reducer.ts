import {createFeatureSelector, createReducer, createSelector, on} from "@ngrx/store";
import * as homePageActions from "./homePage.actions";

export const HomePageStateProperty = 'home';

export interface HomePageState {
  scenarioIsPresent: boolean;
  activeAdventureIsPresent: boolean;
  adventureLogIsPresent: boolean;
}

/* Selectors */

const homePageStateSelector = createFeatureSelector<HomePageState>(HomePageStateProperty);

export const getHomePageState = createSelector(
  homePageStateSelector,
  state => state
)

/* Reducers */

export const homePageReducer = createReducer<HomePageState>(
  {
    scenarioIsPresent: false,
    activeAdventureIsPresent: false,
    adventureLogIsPresent: false
  },
  on(homePageActions.initializedAction, (state, action): HomePageState => ({...action}))
);

