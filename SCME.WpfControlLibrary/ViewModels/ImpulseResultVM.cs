using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCME.WpfControlLibrary.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SSRTUResultVM
    {
        public bool CanStart { get; set; } = true;

        [DependsOn(nameof(CanStart))]
        public bool CanCanselMeasument => !CanStart;


        public string BatchNumber { get; set; }
        public int SerialNumber { get; set; } = 1;
        public bool SerialNumberIsEnabled { get; set; }

    }
}
