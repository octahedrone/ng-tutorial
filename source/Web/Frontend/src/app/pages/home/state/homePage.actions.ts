import {createAction, props} from "@ngrx/store";

export const initializeAction = createAction("[Home Page] Initialize");

export const initializedAction = createAction(
  "[Home Page] Initialized",
  props<{
    scenarioIsPresent: boolean,
    activeAdventureIsPresent: boolean,
    adventureLogIsPresent: boolean
  }>()
);
