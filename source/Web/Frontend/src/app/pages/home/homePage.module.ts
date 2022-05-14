import {NgModule} from '@angular/core';
import {HomePageComponent} from "./homePage.component";
import {MatButtonModule} from '@angular/material/button';
import {RouterModule} from "@angular/router";

@NgModule({
  declarations: [HomePageComponent],
  imports: [
    MatButtonModule,
    RouterModule,
  ],
  exports: [HomePageComponent]
})
export class HomePageModule {}
