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
    
    public partial class SessionStatisticTable
    {
        public int SessionStatisticId { get; set; }
        public int SessionCoreId { get; set; }
        public int Rating { get; set; }
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int SuccessfullCoreAttacks { get; set; }
        public int NotSuccessfullCoreAttacks { get; set; }
        public int Scores { get; set; }
    }
}
