import {ErrorHandler, NgModule} from '@angular/core';
import {FlexLayoutModule} from "@angular/flex-layout";
import {BrowserModule} from '@angular/platform-browser';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {DiagnosticsService} from "./services/diagnostics.service";
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from "@angular/router";
import {ROUTES} from "./app.routes";
import {HomePageService} from "./services/homePage.service";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {FormsModule} from "@angular/forms";
import {AdventureScriptEditorPageModule} from "./pages/script-editor/adventureScriptEditorPage.module";
import {AdventureScriptEditorService} from "./services/script-editor.service";
import {HomePageModule} from "./pages/home/homePage.module";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {GlobalErrorHandler} from "./services/infrastructure/globalErrorHandler";
import {NotificationService} from "./services/infrastructure/notificationService";
import {MatListModule} from '@angular/material/list';
import {MatToolbarModule} from "@angular/material/toolbar";
import {AdventurePlaygroundService} from "./services/adventure-playground.service";
import {AdventurePlaygroundPageModule} from "./pages/playground/adventurePlaygroundPage.module";
import {AdventureLogViewerPageModule} from "./pages/adventure-log-viewer/adventureLogViewerPage.module";
import {MatIconModule} from "@angular/material/icon";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    FlexLayoutModule,
    MonacoEditorModule,
    HomePageModule,
    AdventureScriptEditorPageModule,
    AdventurePlaygroundPageModule,
    AdventureLogViewerPageModule,
    RouterModule.forRoot(ROUTES),
    MatProgressBarModule,
    MatSnackBarModule,
    MatListModule,
    MatToolbarModule,
    MatIconModule
  ],
  providers: [
    NotificationService,
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
    DiagnosticsService,
    HomePageService,
    AdventureScriptEditorService,
    AdventurePlaygroundService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
