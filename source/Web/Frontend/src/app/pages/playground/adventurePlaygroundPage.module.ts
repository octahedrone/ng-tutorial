import {NgModule} from '@angular/core';
import {AdventurePlaygroundPageComponent} from "./adventurePlaygroundPage.component";
import {MatCardModule} from "@angular/material/card";
import {MatListModule} from "@angular/material/list";
import {FormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {MatButtonModule} from "@angular/material/button";

@NgModule({
  declarations: [AdventurePlaygroundPageComponent],
  imports: [
    MatCardModule,
    CommonModule,
    MatListModule,
    FormsModule,
    MatButtonModule
  ],
  exports: [AdventurePlaygroundPageComponent]
})
export class AdventurePlaygroundPageModule {}
