import {Component} from '@angular/core';
import {
  AdventurePlaygroundService,
  CurrentAdventureState,
  AdventureStepOption
} from "../../services/adventure-playground.service";
import {Router} from "@angular/router";
import {QueryResponse} from "../../services/service.models";

@Component({
  selector: 'adventure-log-viewer',
  templateUrl: './adventurePlaygroundPage.component.html'
})
export class AdventurePlaygroundPageComponent {
  public state: CurrentAdventureState | null = null;
  public isLastStep: boolean = false;
  public selectedOptions: AdventureStepOption[] = [];

  constructor(private service: AdventurePlaygroundService,
              private router: Router) {
    this.service.getCurrentStep()
      .subscribe(x => this.processResponse(x));
  }

  submitCurrentSelection() {
    if (!this.state) {
      return;
    }
    const stepId = this.state.currentStepId;
    const optionId = this.isLastStep ? null : this.selectedOptions[0].id;
    this.service.submitUserChoice(stepId, optionId)
      .subscribe(x => this.processResponse(x));
  }

  private processResponse(x: QueryResponse<CurrentAdventureState>): void {
    if (x.success) {
      if (this.isLastStep) {
        this.router.navigate(['']);
      } else {
        this.state = {...x.result};
        this.isLastStep = !this.state.currentStepOptions || this.state.currentStepOptions.length === 0
      }
    }
  }
}
