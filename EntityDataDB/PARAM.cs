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

    public partial class PARAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PARAM()
        {
            this.DEV_PARAM = new HashSet<DEV_PARAM>();
            this.PROF_PARAM = new HashSet<PROF_PARAM>();
        }
        [Key]
        public int PARAM_ID { get; set; }
        public string PARAM_NAME { get; set; }
        public string PARAM_NAME_LOCAL { get; set; }
        public bool PARAM_IS_HIDE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEV_PARAM> DEV_PARAM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROF_PARAM> PROF_PARAM { get; set; }
    }
}