//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Smart_heating.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Maintenance
    {
        public int MaintID { get; set; }
        public int Staff { get; set; }
        public int MaintAddress { get; set; }
        public string MaintType { get; set; }
        public string MaintStatus { get; set; }
        public Nullable<System.DateTime> MaintStartDate { get; set; }
        public Nullable<System.DateTime> MaintEndDate { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Staff Staff1 { get; set; }
    }
}
