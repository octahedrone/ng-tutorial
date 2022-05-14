import {NgModule} from '@angular/core';
import {HomePageComponent} from "./homePage.component";
import {MatButtonModule} from '@angular/material/button';

@NgModule({
  declarations: [HomePageComponent],
  imports: [
    MatButtonModule
  ],
  exports: [HomePageComponent]
})
export class HomePageModule {}
