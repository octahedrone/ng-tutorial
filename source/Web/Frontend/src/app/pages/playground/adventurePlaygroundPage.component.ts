import {Component} from '@angular/core';
import {
  AdventurePlaygroundService,
  CurrentAdventureState,
  AdventureStepOption
} from "../../services/adventure-playground.service";

@Component({
  selector: 'adventure-log-viewer',
  templateUrl: './adventurePlaygroundPage.component.html'
})
export class AdventurePlaygroundPageComponent {
  public state: CurrentAdventureState | null = null;
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
    if (!this.state || !this.selectedOptions || !this.selectedOptions.length) {
      return;
    }
    const stepId = this.state.currentStepId;
    const optionId = this.selectedOptions[0].id;
    this.service.submitUserChoice(stepId, optionId)
      .subscribe(x => {
        if (x.success) {
          this.state = {...x.result};
        }
      });
  }
}
