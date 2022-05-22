import {NgModule} from '@angular/core';
import {AdventureScriptEditorPageComponent} from "./adventureScriptEditorPage.component";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {FormsModule} from "@angular/forms";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";

@NgModule({
  declarations: [AdventureScriptEditorPageComponent],
  imports: [
    FormsModule,
    MonacoEditorModule,
    MatCardModule,
    MatButtonModule
  ],
  exports: [AdventureScriptEditorPageComponent]
})
export class AdventureScriptEditorPageModule {}
