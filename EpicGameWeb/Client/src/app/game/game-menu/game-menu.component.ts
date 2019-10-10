import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router/router'

import {GameService} from '../../shared/Game.service';

@Component({
  selector: 'app-game-menu',
  templateUrl: './game-menu.component.html',
  styleUrls: ['./game-menu.component.css'],
  providers: [GameService]
})
export class GameMenuComponent implements OnInit {

  constructor(private gameService: GameService) { }

  ngOnInit() {
  }
  
  OnBtnPlayClick() : void
  {
      this.gameService.SwitchToGame();
  }

  OnBtnAccountClick() : void
  {
      //
  }

  OnBtnPeopleClick() : void
  {
      //
  }

}
