//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCME.EntityDataDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DEV_PARAM
    {
        [Key]
        public int DEV_PARAM_ID { get; set; }

        public int DEV_ID { get; set; }
        public int PARAM_ID { get; set; }
        public decimal VALUE { get; set; }
        public int TEST_TYPE_ID { get; set; }

        
    
        public virtual DEVICE DEVICE { get; set; }
        public virtual PARAM PARAM { get; set; }
    }
}