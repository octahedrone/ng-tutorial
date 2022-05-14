import {Routes} from "@angular/router";
import {HomePageComponent} from "./pages/home/homePage.component";
import {AdventureLogViewerPageComponent} from "./pages/adventure-log-viewer/adventureLogViewerPage.component";
import {AdventureScriptEditorPageComponent} from "./pages/script-editor/adventureScriptEditorPage.component";
import {AdventurePlaygroundPageComponent} from "./pages/playground/adventurePlaygroundPage.component";

export const ROUTES: Routes = [
  {
    path: "",
    component: HomePageComponent,
    children: [
      {path: "", loadChildren: () => import("./pages/home/homePage.module").then((module) => module.HomePageModule)}
    ]
  },
  {path: "edit", component: AdventureScriptEditorPageComponent},
  {path: "play", component: AdventurePlaygroundPageComponent},
  {path: "log", component: AdventureLogViewerPageComponent},
  {
    path: "**",
    redirectTo: ""
  },
]
