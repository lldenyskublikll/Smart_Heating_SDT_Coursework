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
    
    public partial class ORS_Participants
    {
        public int ORSUSerID { get; set; }
        public int PersonID { get; set; }
    
        public virtual PersonData PersonData { get; set; }
    }
}