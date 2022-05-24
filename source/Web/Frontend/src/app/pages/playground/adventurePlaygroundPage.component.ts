import {Component} from '@angular/core';
import {
  AdventurePlaygroundService,
  AdventureStep,
  AdventureStepOption
} from "../../services/adventure-playground.service";

@Component({
  selector: 'adventure-log-viewer',
  templateUrl: './adventurePlaygroundPage.component.html'
})
export class AdventurePlaygroundPageComponent {
  public state: AdventureStep | null = null;
  public selectedOptions: AdventureStepOption[] = [];

  constructor(private service: AdventurePlaygroundService) {
    this.service.getCurrentStep()
      .subscribe(x => {
        if (x.success) {
          this.state = {...x.result};
        }
      });
  }

  submitCurrentSelection() {
    console.log(this.selectedOptions);
  }
}
