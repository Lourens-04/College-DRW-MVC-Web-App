//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomingoRoofWorksWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Job_Material
    {
        public string JobMaterials_ID { get; set; }
        public string Material_ID { get; set; }
        public string Job_Card_No { get; set; }
        public int Quantity { get; set; }
    
        public virtual Job Job { get; set; }
        public virtual Material Material { get; set; }
    }
}