import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {DiagnosticsService} from "./services/diagnostics.service";
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from "@angular/router";
import {ROUTES} from "./app.routes";
import {HomePageService} from "./services/homePage.service";
import {StoreModule} from '@ngrx/store';
import {EffectsModule} from '@ngrx/effects';
import {State} from "./state/app.state";
import {homePageReducer} from "./pages/home/state/homePage.reducer";
import {HomePageEffects} from "./pages/home/state/homePage.effects";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {FormsModule} from "@angular/forms";
import {AdventureScriptEditorPageModule} from "./pages/script-editor/adventureScriptEditorPage.module";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    MonacoEditorModule,
    AdventureScriptEditorPageModule,
    RouterModule.forRoot(ROUTES),
    StoreModule.forRoot<State>({
      home: homePageReducer
    }),
    EffectsModule.forRoot([
      HomePageEffects
    ]),
  ],
  providers: [
    DiagnosticsService,
    HomePageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
