import {Component} from '@angular/core';
import {
  Event,
  NavigationStart,
  Router,
  RouterEvent
} from "@angular/router";
import {filter} from "rxjs";

@Component({
  selector: "app-root",
  templateUrl: './app.component.html'
})
export class AppComponent {
  public showProgressBar: boolean = false;

  constructor(private router: Router) {
    this.router.events.pipe(
      filter((e: Event): e is RouterEvent => e instanceof RouterEvent)
    ).subscribe((e: RouterEvent) => {
      this.progressBarActivator(e);
    });
  }

  private progressBarActivator(event: RouterEvent): void {
    this.showProgressBar = event instanceof NavigationStart;
  }
}
