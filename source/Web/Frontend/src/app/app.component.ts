import {Component} from '@angular/core';
import {DiagnosticsService} from "./services/diagnostics.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'adventure';

  constructor(private service: DiagnosticsService) {
  }

  async callTestApi() {
    const date = await this.service.datetime();
    date.subscribe(x => console.log(x));
  }
}
