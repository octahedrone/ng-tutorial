import {Component, OnInit} from '@angular/core';
import {AdventureLogRecord, AdventureLogsService} from "../../services/adventure-logs.service";

@Component({
  selector: 'adventure-log-viewer',
  templateUrl: './adventureLogViewerPage.component.html'
})
export class AdventureLogViewerPageComponent implements OnInit {
  public state: AdventureLogRecord[] | null = null;

  constructor(private service: AdventureLogsService) {
  }

  ngOnInit(): void {
    this.service.getAdventureLog().subscribe(x =>
      this.state = x.result
    );
  }
}
