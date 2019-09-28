import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {


  readonly base_url : string = ''; 

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  onClick() : void
  {
      alert('button clicked!');

  }

}
