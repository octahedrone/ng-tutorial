import {NgModule} from '@angular/core';
import {AdventureScriptEditorPageComponent} from "./adventureScriptEditorPage.component";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [AdventureScriptEditorPageComponent],
  imports: [
    FormsModule,
    MonacoEditorModule
  ],
  exports: [AdventureScriptEditorPageComponent]
})
export class AdventureScriptEditorPageModule {}
