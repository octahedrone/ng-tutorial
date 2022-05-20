import {Component, OnInit} from '@angular/core';
import {AdventureScriptEditorService} from "../../services/script-editor.service";
import {Store} from "@ngrx/store";
import {State} from "../../state/app.state";

@Component({
  selector: 'adventure-log-viewer',
  templateUrl: './adventureScriptEditorPage.component.html'
})
export class AdventureScriptEditorPageComponent implements OnInit {
  editorOptions = {theme: 'vs-dark', language: 'yaml'};
  code: string = '';

  constructor(private store: Store<State>,
              private service: AdventureScriptEditorService) {
  }

  ngOnInit() {
    this.service.loadScript()
      .subscribe(x => this.code = x);
  }

  saveCurrentScript() {
    this.service.saveScript(this.code)
      .subscribe();
  }
}
