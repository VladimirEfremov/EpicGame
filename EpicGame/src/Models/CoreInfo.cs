using System;

namespace EpicGame.src.Models
{
    public class CoreInfo
    {
        public int CoreId;
        public int CoreMapId;

        public int Money;

        public int CasernLevel;
        public int GoldMiningLevel;
        public int DefenceTowerLevel;

        public int BaseCapacity;
        public int CasernCapacity;
        public int GoldMiningCapacity;

        //BUildings
        public bool Casern;
        public bool GoldMining;
        public bool DefenceTower;
        public int NumberOfDefenceTower;

        public int NumberOfWorkersInBase;
        public int NumberOfWorkersInGoldMining;

        public int NumberOfWarriors;
        public int NumberOfAttackAircraft;

    }
}
