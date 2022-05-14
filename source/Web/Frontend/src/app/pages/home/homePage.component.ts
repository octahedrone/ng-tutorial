import {Component} from '@angular/core';
import {DiagnosticsService} from "../../services/diagnostics.service";

@Component({
  selector: 'home-page',
  templateUrl: './homePage.component.html'
})
export class HomePageComponent {
  title = 'adventure';

  constructor(private service: DiagnosticsService) {
  }

  async callTestApi() {
    const date = await this.service.datetime();
    date.subscribe(x => console.log(x));
  }
}
