import { Routes } from "@angular/router";
import {HomePageComponent} from "./pages/home/homePage.component";

export const ROUTES: Routes = [
  {
    path: "",
    component: HomePageComponent,
    children: [
      { path: "", loadChildren: () => import("./pages/home/homePage.module").then((module) => module.HomePageModule) }
    ]
  },
  {
    path: "**",
    redirectTo: ""
  },
]
