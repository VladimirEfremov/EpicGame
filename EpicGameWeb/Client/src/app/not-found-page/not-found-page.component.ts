import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-not-found-page',
  templateUrl: './not-found-page.component.html',
  styleUrls: ['./not-found-page.component.css']
})
export class NotFoundPageComponent implements OnInit {

  wastedSeconds: number;

  constructor() { 
    setInterval(() => { this.incriment(); }, 1000);
  }

  ngOnInit() {
    this.wastedSeconds=0;
  }

  incriment() : void {
    this.wastedSeconds++;
  }
}
