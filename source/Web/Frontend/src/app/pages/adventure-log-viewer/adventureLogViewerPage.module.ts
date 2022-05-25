import {NgModule} from '@angular/core';
import {AdventureLogViewerPageComponent} from "./adventureLogViewerPage.component";
import {MatCardModule} from "@angular/material/card";
import {BrowserModule} from "@angular/platform-browser";
import {MatListModule} from "@angular/material/list";
import {MatIconModule} from "@angular/material/icon";
import {FlexLayoutModule} from "@angular/flex-layout";

@NgModule({
  declarations: [AdventureLogViewerPageComponent],
  imports: [
    MatCardModule,
    BrowserModule,
    MatListModule,
    MatIconModule,
    FlexLayoutModule
  ],
  exports: [AdventureLogViewerPageComponent]
})
export class AdventureLogViewerPageModule {}
