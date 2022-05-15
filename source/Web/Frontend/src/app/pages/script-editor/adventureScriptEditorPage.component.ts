import {Component} from '@angular/core';

@Component({
  selector: 'adventure-log-viewer',
  templateUrl: './adventureScriptEditorPage.component.html'
})
export class AdventureScriptEditorPageComponent {
  editorOptions = {theme: 'vs-dark', language: 'yaml'};
  code: string = '';
}
