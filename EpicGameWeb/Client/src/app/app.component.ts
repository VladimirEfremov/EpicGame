import { Component, ViewEncapsulation, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit {

  title: string = 'Angular works correctly with nodejs';
  isRegistered : boolean = false;

  ngOnInit()
  {
      console.log("App-root init");
  }

}
