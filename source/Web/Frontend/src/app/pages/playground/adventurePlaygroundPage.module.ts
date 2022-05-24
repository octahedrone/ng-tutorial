import {NgModule} from '@angular/core';
import {AdventurePlaygroundPageComponent} from "./adventurePlaygroundPage.component";
import {MatCardModule} from "@angular/material/card";
import {MatListModule} from "@angular/material/list";
import {FormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {MatButtonModule} from "@angular/material/button";
import {RouterModule} from "@angular/router";
import {FlexModule} from "@angular/flex-layout";

@NgModule({
  declarations: [AdventurePlaygroundPageComponent],
  imports: [
    MatCardModule,
    CommonModule,
    MatListModule,
    FormsModule,
    MatButtonModule,
    RouterModule,
    FlexModule
  ],
  exports: [AdventurePlaygroundPageComponent]
})
export class AdventurePlaygroundPageModule {}
