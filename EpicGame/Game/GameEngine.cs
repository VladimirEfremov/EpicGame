using System;
using System.Collections.Generic;
using EpicGame.src.Models.Game;
using EpicGameCommon.Response;

namespace EpicGame.src
{
    public class GameEngine
    {

        public GameEngine()
        {
            //
        }

        // 0 - attackers win
        // 1 - defenders win
        public static BattleResponse DuelBattle(
            ref List<GameUnitsTable> attackers,
            ref List<GameUnitsTable> defenders)
        {
            int attackersCountBefore = attackers.Count;
            int defendersCountBefore = defenders.Count;

            while (true)
            {
                if (defenders.Count <= 0 && attackers.Count > 0) {
                    var response = new BattleResponse();
                    response.WhoWonTheBattle = 0;
                    response.Message = "Attacker win the battle! " +
                        "Defenders been fully destroyed!\n" +
                        $"Before battle [def count: {defendersCountBefore}" +
                        $"att: {attackersCountBefore}]\n" +
                        $"After battle [def count: {defendersCountBefore}" +
                        $"att count: {attackersCountBefore}]";
                    return response;
                }
                if (attackers.Count <= 0 && defenders.Count > 0) {
                    var response = new BattleResponse();
                    response.WhoWonTheBattle = 1;
                    response.Message = "Defender win the battle! " +
                        "Attackers been fully destroyed!\n" +
                        $"Before battle [def count: {defendersCountBefore}" +
                        $"att: {attackersCountBefore}]\n" +
                        $"After battle [def count: {defendersCountBefore}" +
                        $"att count: {attackersCountBefore}]";
                    return response;
                }
                if (attackers.Count <= 0 && defenders.Count <= 0) {
                    var response = new BattleResponse();
                    response.WhoWonTheBattle = -1;
                    response.Message = "Draw!\n" +
                        "Attackers been fully destroyed!\n" +
                        "Defenders been fully destroyed!";
                    return response;
                }

                foreach (var attacker in attackers)
                {
                    var tempDamage = attacker.GameUnitAttack * (100 - defenders[0].GameUnitDefence) / 100;
                    defenders[0].GameUnitHP -= tempDamage;
                    if (defenders[0].GameUnitHP <= 0)
                    {
                        defenders.RemoveAt(0);
                        if (defenders.Count <= 0 && attackers.Count > 0)
                        {
                            var response = new BattleResponse();
                            response.WhoWonTheBattle = 0;
                            response.Message = "Attacker win the battle! " +
                                "Defenders been fully destroyed!\n" +
                                $"Before battle [def count: {defendersCountBefore}" +
                                $"att: {attackersCountBefore}]\n" +
                                $"After battle [def count: {defendersCountBefore}" +
                                $"att count: {attackersCountBefore}]";
                            return response;
                        }
                    }
                }

                foreach (var defender in defenders)
                {
                    var tempDamage = defender.GameUnitAttack * (100 - attackers[0].GameUnitDefence) / 100;
                    attackers[0].GameUnitHP -= tempDamage;
                    if (attackers[0].GameUnitHP <= 0)
                    {
                        attackers.RemoveAt(0);
                        if (attackers.Count <= 0 && defenders.Count > 0)
                        {
                            var response = new BattleResponse();
                            response.WhoWonTheBattle = 1;
                            response.Message = "Defender win the battle! " +
                                "Attackers been fully destroyed!\n" +
                                $"Before battle [def count: {defendersCountBefore}" +
                                $"att: {attackersCountBefore}]\n" +
                                $"After battle [def count: {defendersCountBefore}" +
                                $"att count: {attackersCountBefore}]";
                            return response;
                        }
                    }
                }

            }
        }

        public static BattleResponse CoreBattle(
            ref List<GameUnitsTable> attackers,
            ref List<GameUnitsTable> defenders,
            ref List<GameBuildingsTable> coreBuildings)
        {
            int attackersCountBefore = attackers.Count;
            int defendersCountBefore = defenders.Count;
            int coreBuildingsCountBefore = coreBuildings.Count;

            while (true)
            {
                if (defenders.Count <= 0 && coreBuildings.Count <= 0 && attackers.Count > 0)
                {
                    var response = new BattleResponse();
                    response.WhoWonTheBattle = 0;
                    response.Message = "Attacker win the battle! " +
                        "Defenders been fully destroyed!\n" +
                        "Defender core been fully destroyed!\n" +
                        $"Before battle [def count: {defendersCountBefore}, buildings: {coreBuildingsCountBefore}" +
                        $"att: {attackersCountBefore}]\n" +
                        $"After battle [def count: {defendersCountBefore}, buildings: {coreBuildings.Count}" +
                        $"att count: {attackersCountBefore}]";
                    return response;
                }
                if (attackers.Count <= 0 && (defenders.Count > 0 || coreBuildings.Count > 0))
                {
                    var response = new BattleResponse();
                    response.WhoWonTheBattle = 1;
                    response.Message = "Defender win the battle! " +
                        "Attackers been fully destroyed!\n" +
                        $"Before battle [def count: {defendersCountBefore}, buildings: {coreBuildingsCountBefore}" +
                        $"att: {attackersCountBefore}]\n" +
                        $"After battle [def count: {defendersCountBefore}, buildings: {coreBuildings.Count}" +
                        $"att count: {attackersCountBefore}]";
                    return response;
                }
                if (attackers.Count <= 0 && defenders.Count <= 0)
                {
                    var response = new BattleResponse();
                    response.WhoWonTheBattle = -1;
                    response.Message = "Draw!\n" +
                        "Attackers been fully destroyed!\n" +
                        "Defenders been fully destroyed!";
                    return response;
                }

                if (defenders.Count > 0)
                {
                    foreach (var attacker in attackers)
                    {
                        var tempDamage = attacker.GameUnitAttack * (100 - defenders[0].GameUnitDefence) / 100;
                        defenders[0].GameUnitHP -= tempDamage;
                        if (defenders[0].GameUnitHP <= 0)
                        {
                            defenders.RemoveAt(0);
                            if (defenders.Count <= 0 && coreBuildings.Count <= 0 && attackers.Count > 0)
                            {
                                var response = new BattleResponse();
                                response.WhoWonTheBattle = 0;
                                response.Message = "Attacker win the battle! " +
                                    "Defenders been fully destroyed!\n" +
                                    "Defender core been fully destroyed!\n" +
                                    $"Before battle [def count: {defendersCountBefore}, buildings: {coreBuildingsCountBefore}" +
                                    $"att: {attackersCountBefore}]\n" +
                                    $"After battle [def count: {defendersCountBefore}, buildings: {coreBuildings.Count}" +
                                    $"att count: {attackersCountBefore}]";
                                return response;
                            }
                        }
                    }
                }
                else if (coreBuildings.Count > 0)
                {
                    foreach (var attacker in attackers)
                    {
                        var tempDamage = 
                            attacker.GameUnitAttack * 
                            (100 - coreBuildings[0].GameBuildingDefence) / 100;
                        coreBuildings[0].GameBuildingHP -= tempDamage;
                        if (defenders[0].GameUnitHP <= 0)
                        {
                            defenders.RemoveAt(0);
                            if (defenders.Count <= 0 && coreBuildings.Count <= 0 
                                && attackers.Count > 0)
                            {
                                var response = new BattleResponse();
                                response.WhoWonTheBattle = 0;
                                response.Message = "Attacker win the battle! " +
                                    "Defenders been fully destroyed!\n" +
                                    "Defender core been fully destroyed!\n" +
                                    $"Before battle [def count: {defendersCountBefore}, buildings: {coreBuildingsCountBefore}" +
                                    $"att: {attackersCountBefore}]\n" +
                                    $"After battle [def count: {defendersCountBefore}, buildings: {coreBuildings.Count}" +
                                    $"att count: {attackersCountBefore}]";
                                return response;
                            }
                        }
                    }
                }

                foreach (var defender in defenders)
                {
                    var tempDamage = defender.GameUnitAttack * (100 - attackers[0].GameUnitDefence) / 100;
                    attackers[0].GameUnitHP -= tempDamage;
                    if (attackers[0].GameUnitHP <= 0)
                    {
                        attackers.RemoveAt(0);
                        if (attackers.Count <= 0 && defenders.Count > 0)
                        {
                            var response = new BattleResponse();
                            response.WhoWonTheBattle = 1;
                            response.Message = "Defender win the battle! " +
                                "Attackers been fully destroyed!\n" +
                                $"Before battle [def count: {defendersCountBefore}, buildings: {coreBuildingsCountBefore}" +
                                $"att: {attackersCountBefore}]\n" +
                                $"After battle [def count: {defendersCountBefore}, buildings: {coreBuildings.Count}" +
                                $"att count: {attackersCountBefore}]";
                            return response;
                        }
                    }
                }

                foreach (var building in coreBuildings)
                {
                    var tempDamage = building.GameBuildingAttack * 
                        (100 - attackers[0].GameUnitDefence) / 100;
                    attackers[0].GameUnitHP -= tempDamage;
                    if (attackers[0].GameUnitHP <= 0)
                    {
                        attackers.RemoveAt(0);
                        if (attackers.Count <= 0 && (defenders.Count > 0 || coreBuildings.Count > 0))
                        {
                            var response = new BattleResponse();
                            response.WhoWonTheBattle = 1;
                            response.Message = "Defender win the battle! " +
                                "Attackers been fully destroyed!\n" +
                                $"Before battle [def count: {defendersCountBefore}, buildings: {coreBuildingsCountBefore}" +
                                $"att: {attackersCountBefore}]\n" +
                                $"After battle [def count: {defendersCountBefore}, buildings: {coreBuildings.Count}" +
                                $"att count: {attackersCountBefore}]";
                            return response;
                        }
                    }
                }
            }
        }

    }

}
