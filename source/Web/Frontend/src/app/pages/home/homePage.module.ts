import {NgModule} from '@angular/core';
import {RouterModule} from "@angular/router";
import {CommonModule} from "@angular/common";
import {HomePageComponent} from "./homePage.component";
import {MatButtonModule} from '@angular/material/button';

@NgModule({
  declarations: [HomePageComponent],
  imports: [
    CommonModule,
    RouterModule,
    MatButtonModule,
  ],
  exports: [HomePageComponent]
})
export class HomePageModule {
}
