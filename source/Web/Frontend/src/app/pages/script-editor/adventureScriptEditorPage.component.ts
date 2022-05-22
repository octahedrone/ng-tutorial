// https://materiahq.github.io/ngx-monaco-editor/api-reference/

import {Component, OnInit} from '@angular/core';
import {AdventureScriptEditorService} from "../../services/script-editor.service";
import {Store} from "@ngrx/store";
import {State} from "../../state/app.state";
import {NotificationService} from "../../services/infrastructure/notificationService";

@Component({
  selector: 'adventure-log-viewer',
  templateUrl: './adventureScriptEditorPage.component.html'
})
export class AdventureScriptEditorPageComponent implements OnInit {
  editorOptions = {
    theme: 'vs-dark',
    language: 'yaml'
  };
  code: string = '';

  constructor(private store: Store<State>,
              private service: AdventureScriptEditorService,
              private notificationService: NotificationService) {
  }

  ngOnInit() {
    this.service.loadScript()
      .subscribe(x => this.code = x);
  }

  saveCurrentScript() {
    this.service
      .saveScript(this.code)
      .subscribe(success => this.notificationService.success('Script saved successfully'));
  }
}
