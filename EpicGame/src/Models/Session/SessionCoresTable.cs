//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EpicGame.src.Models.Session
{
    using System;
    using System.Collections.Generic;
    
    public partial class SessionCoresTable
    {
        public int SessionCoreId { get; set; }
        public int UserId { get; set; }
        public int CoreMapId { get; set; }
        public int Money { get; set; }
        public int HPUpgrade { get; set; }
        public int AttackUpgrade { get; set; }
        public int DefenceUpgrade { get; set; }
        public int CapacityUpgrade { get; set; }
        public int WarriorsNumber { get; set; }
        public int AttackAircraftNumber { get; set; }
        public int WorkerNumber { get; set; }
        public int Casern { get; set; }
        public int DefenceTower { get; set; }
        public int GoldMining { get; set; }
    }
}