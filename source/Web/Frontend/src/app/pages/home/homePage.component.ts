import {Component, OnInit} from '@angular/core';
import {HomePageService, HomeScreenData} from "../../services/homePage.service";

@Component({
  selector: 'home-page',
  templateUrl: './homePage.component.html'
})
export class HomePageComponent implements OnInit {
  public state: HomeScreenData | null;

  constructor(private service: HomePageService) {
    this.state = null;
  }

  ngOnInit() {
    this.service.load().subscribe(x => this.state = x);
  }
}
