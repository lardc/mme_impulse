﻿using SCME.Types.BaseTestParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCME.Types.SSRTU
{
    [DataContract(Namespace = "http://proton-electrotex.com/SCME")]
    public enum HWDeviceState
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        Fault = 1,
        [EnumMember]
        Disabled = 2,
        [EnumMember]
        Ready = 3,
        [EnumMember]
        InProcess = 4,
        [EnumMember]
        Alarm = 5
    };

    [DataContract(Namespace = "http://proton-electrotex.com/SCME")]
    public enum HWFinishedState
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        Success = 1,
        [EnumMember]
        Failed= 3,
    };



    [DataContract(Namespace = "http://proton-electrotex.com/SCME")]
    public class TestResults : BaseTestResults
    {
        [DataMember]
        public int NumberPosition { get; set; }
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public bool InputOptionsIsAmperage { get; set; }
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public TestParametersType TestParametersType { get; set; } = TestParametersType.InputOptions;

        [DataMember]
        public double AuxiliaryCurrentPowerSupply1 { get; set; }
        [DataMember]
        public double AuxiliaryCurrentPowerSupply2 { get; set; }

        [DataMember]
        public double OpenResistance { get; set; }

        [DataMember]
        public bool IsIGBTOrMosfet { get; set; }
    }

    public class CommonResult
    {
        [DataMember]
        public double? ResidualVoltage { get; set; }
        [DataMember]
        public double? LeackageCurrent { get; set; }
        [DataMember]
        public double? Voltage { get; set; }
        [DataMember]
        public double? Current { get; set; }
        [DataMember]
        public double? OpenResistance { get; set; }
        [DataMember]
        public double? AuxiliaryCurrentPowerSupply1 { get; set; }
        [DataMember]
        public double? AuxiliaryCurrentPowerSupply2 { get; set; }

 
    }
}
