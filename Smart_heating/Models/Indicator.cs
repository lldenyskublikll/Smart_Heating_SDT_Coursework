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
    
    public partial class Indicator
    {
        public int IndicatorID { get; set; }
        public int Sensor { get; set; }
        public System.DateTime IndDate { get; set; }
        public double Indicator1 { get; set; }
    
        public virtual Sensor Sensor1 { get; set; }
    }
}
