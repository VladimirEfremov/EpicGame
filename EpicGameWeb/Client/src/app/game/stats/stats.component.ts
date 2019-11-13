import { Component, OnInit } from '@angular/core';
import { StatInfo } from '../game-structures/StatInfo';
import { GameService } from '../../shared/Game.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css'],
  providers: [GameService]
})
export class StatsComponent implements OnInit {

  table:StatInfo[] = [];

  constructor(private httpGameService: GameService) { }

  ngOnInit() {
    this.httpGameService.GetAllStats()
      .subscribe(
        (data )=> {
          console.log("[success] GetAllStats");
          this.table = data;
        },
        (error)=>{
          console.log("[error] GetAllStats: "+error);
        });
  }
  public SwitchToMenu():void{
    this.httpGameService.SwitchToMenu();
  }
}
