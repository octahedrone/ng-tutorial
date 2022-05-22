import {NgModule} from '@angular/core';
import {RouterModule} from "@angular/router";
import {CommonModule} from "@angular/common";
import {HomePageComponent} from "./homePage.component";
import {MatButtonModule} from '@angular/material/button';
import {MatListModule} from "@angular/material/list";
import {MatCardModule} from "@angular/material/card";

@NgModule({
  declarations: [HomePageComponent],
    imports: [
        CommonModule,
        RouterModule,
        MatButtonModule,
        MatListModule,
        MatCardModule,
    ],
  exports: [HomePageComponent]
})
export class HomePageModule {
}
