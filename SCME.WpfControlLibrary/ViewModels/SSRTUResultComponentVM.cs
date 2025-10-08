using PropertyChanged;
using SCME.Types;
using System.Collections.Generic;
using System.Linq;

namespace SCME.WpfControlLibrary.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SSRTUResultComponentVM
    {
        public string ErrorCode { get; set; }
        
        public bool IsEmpty => LeakageCurrentsIsEmpty && InputAmperagesIsEmpty && InputVoltagesIsEmpty && ResidualVoltagesIsEmpty
            && IGBTOrMosfetLeakageCurrentsIsEmpty && IGBTOrMosfetInputAmperagesIsEmpty && IGBTOrMosfetInputVoltagesIsEmpty && IGBTOrMosfetResidualVoltagesIsEmpty
            && AuxiliaryCurrentPowerSupply1 == null && AuxiliaryCurrentPowerSupply2 == null;
        public bool IsGood => LeakageCurrentsIsGood && InputAmperagesIsGood && InputVoltagesIsGood && ResidualVoltagesIsGood
            && IGBTOrMosfetLeakageCurrentsIsGood && IGBTOrMosfetInputAmperagesIsGood && IGBTOrMosfetInputVoltagesIsGood && IGBTOrMosfetResidualVoltagesIsGood
            && (AuxiliaryCurrentPowerSupply1IsOk ?? true) && (AuxiliaryCurrentPowerSupply2IsOk ?? true);

        public SSRTUResultComponentVM()
        {
            LeakageCurrents = new List<Result>()
            {
                LeakageCurrent1,
                LeakageCurrent2,
                LeakageCurrent3,
                LeakageCurrent4
            };
            ResidualVoltages = new List<ResultResidualVoltage>()
            {
                ResidualVoltage1,
                ResidualVoltage2,
                ResidualVoltage3,
                ResidualVoltage4
            };
            InputAmperages = new List<Result>()
            {
                InputAmperage1,
                InputAmperage2,
                InputAmperage3,
                InputAmperage4,
            };
            InputVoltages = new List<Result>()
            {
                InputVoltage1,
                InputVoltage2,
                InputVoltage3,
                InputVoltage4
            };

            IGBTOrMosfetLeakageCurrents = new List<Result>()
            {
                IGBTOrMosfetLeakageCurrent1,
                IGBTOrMosfetLeakageCurrent2,
                IGBTOrMosfetLeakageCurrent3,
                IGBTOrMosfetLeakageCurrent4
            };
            IGBTOrMosfetResidualVoltages = new List<ResultResidualVoltage>()
            {
                IGBTOrMosfetResidualVoltage1,
                IGBTOrMosfetResidualVoltage2,
                IGBTOrMosfetResidualVoltage3,
                IGBTOrMosfetResidualVoltage4
            };
            IGBTOrMosfetInputAmperages = new List<Result>()
            {
                IGBTOrMosfetInputAmperage1,
                IGBTOrMosfetInputAmperage2,
                IGBTOrMosfetInputAmperage3,
                IGBTOrMosfetInputAmperage4,
            };
            IGBTOrMosfetInputVoltages = new List<Result>()
            {
                IGBTOrMosfetInputVoltage1,
                IGBTOrMosfetInputVoltage2,
                IGBTOrMosfetInputVoltage3,
                IGBTOrMosfetInputVoltage4
            };
        }

        public int Positition { get; set; }
        public DutPackageType DutPackageType { get; set; }
        public int SerialNumber { get; set; }

        public bool ShowAuxiliaryCurrentPowerSupply1 => DutPackageType == DutPackageType.B5 || DutPackageType == DutPackageType.V108;

        public bool ShowAuxiliaryCurrentPowerSupply2 => DutPackageType == DutPackageType.V108;

        public bool ShowInputAmperage { get; set; }
        [DependsOn(nameof(ShowInputAmperage))]
        public bool ShowInputVoltage => !ShowInputAmperage;


        [AddINotifyPropertyChangedInterface]
        public class Result
        {
            public int Index { get; set; }
            public Result(int index)
            {
                Index = index;
            }

            public double? Value { get; set; }
            public double? Min { get; set; }
            public double? Max { get; set; }

            [DependsOn(nameof(Value))]
            public bool IsEmpty => Value == null;
            [DependsOn(nameof(Value))]
            public bool IsGood => (IsOk ?? true);

            public bool IsAmp {  get; set; } = false;

            [DependsOn(nameof(Value), nameof(Min), nameof(Max))]
            public bool? IsOk => Min == null || Value?.CompareTo(double.Epsilon) == 0 ? (bool?)null : (Min <= Value && Value < Max);
        }

        [AddINotifyPropertyChangedInterface]
        public class ResultResidualVoltage : Result
        {
            public ResultResidualVoltage(int index) : base(index)
            {
            }

            public double? ValueEx { get; set; }
            public double? MinEx { get; set; }
            public double? MaxEx { get; set; }

            [DependsOn(nameof(MinEx))]
            public bool UseEx => MinEx != null;
            [DependsOn(nameof(ValueEx))]
            public bool IsEmptyEx => ValueEx == null;
            [DependsOn(nameof(ValueEx))]
            public bool IsGoodEx => (IsOkEx ?? true);

            [DependsOn(nameof(ValueEx), nameof(MinEx), nameof(MaxEx))]
            public bool? IsOkEx => MinEx == null || ValueEx?.CompareTo(double.Epsilon) == 0 ? (bool?)null : (MinEx < ValueEx && ValueEx < MaxEx);
        }

        public Result LeakageCurrent1 { get; set; } = new Result(1);
        public Result LeakageCurrent2 { get; set; } = new Result(2);
        public Result LeakageCurrent3 { get; set; } = new Result(3);
        public Result LeakageCurrent4 { get; set; } = new Result(4);
        public List<Result> LeakageCurrents { get; set; }

        public ResultResidualVoltage ResidualVoltage1 { get; set; } = new ResultResidualVoltage(1);
        public ResultResidualVoltage ResidualVoltage2 { get; set; } = new ResultResidualVoltage(2);
        public ResultResidualVoltage ResidualVoltage3 { get; set; } = new ResultResidualVoltage(3);
        public ResultResidualVoltage ResidualVoltage4 { get; set; } = new ResultResidualVoltage(4);
        public List<ResultResidualVoltage> ResidualVoltages { get; set; }

        public Result InputAmperage1 { get; set; } = new Result(1);
        public Result InputAmperage2 { get; set; } = new Result(2);
        public Result InputAmperage3 { get; set; } = new Result(3);
        public Result InputAmperage4 { get; set; } = new Result(4);
        public List<Result> InputAmperages { get; set; }

        public Result InputVoltage1 { get; set; } = new Result(1);
        public Result InputVoltage2 { get; set; } = new Result(2);
        public Result InputVoltage3 { get; set; } = new Result(3);
        public Result InputVoltage4 { get; set; } = new Result(4);
        public List<Result> InputVoltages { get; set; }

        public Result IGBTOrMosfetLeakageCurrent1 { get; set; } = new Result(1);
        public Result IGBTOrMosfetLeakageCurrent2 { get; set; } = new Result(2);
        public Result IGBTOrMosfetLeakageCurrent3 { get; set; } = new Result(3);
        public Result IGBTOrMosfetLeakageCurrent4 { get; set; } = new Result(4);
        public List<Result> IGBTOrMosfetLeakageCurrents { get; set; }

        public ResultResidualVoltage IGBTOrMosfetResidualVoltage1 { get; set; } = new ResultResidualVoltage(1);
        public ResultResidualVoltage IGBTOrMosfetResidualVoltage2 { get; set; } = new ResultResidualVoltage(2);
        public ResultResidualVoltage IGBTOrMosfetResidualVoltage3 { get; set; } = new ResultResidualVoltage(3);
        public ResultResidualVoltage IGBTOrMosfetResidualVoltage4 { get; set; } = new ResultResidualVoltage(4);
        public List<ResultResidualVoltage> IGBTOrMosfetResidualVoltages { get; set; }

        public Result IGBTOrMosfetInputAmperage1 { get; set; } = new Result(1);
        public Result IGBTOrMosfetInputAmperage2 { get; set; } = new Result(2);
        public Result IGBTOrMosfetInputAmperage3 { get; set; } = new Result(3);
        public Result IGBTOrMosfetInputAmperage4 { get; set; } = new Result(4);
        public List<Result> IGBTOrMosfetInputAmperages { get; set; }

        public Result IGBTOrMosfetInputVoltage1 { get; set; } = new Result(1);
        public Result IGBTOrMosfetInputVoltage2 { get; set; } = new Result(2);
        public Result IGBTOrMosfetInputVoltage3 { get; set; } = new Result(3);
        public Result IGBTOrMosfetInputVoltage4 { get; set; } = new Result(4);
        public List<Result> IGBTOrMosfetInputVoltages { get; set; }

        public double? ManualAmperage1 { get; set; } = null;
        public double? ManualAmperage2 { get; set; } = null;
        public double? ManualAmperage3 { get; set; } = null;
        public double? ManualAmperage4 { get; set; } = null;

        public bool NeedsManualAmperage1 { get; set; } = false;
        public bool NeedsManualAmperage2 { get; set; } = false;
        public bool NeedsManualAmperage3 { get; set; } = false;
        public bool NeedsManualAmperage4 { get; set; } = false;

        public double? ManualVoltage1 { get; set; } = null;
        public double? ManualVoltage2 { get; set; } = null;
        public double? ManualVoltage3 { get; set; } = null;
        public double? ManualVoltage4 { get; set; } = null;

        public bool NeedsManualVoltage1 { get; set; } = false;
        public bool NeedsManualVoltage2 { get; set; } = false;
        public bool NeedsManualVoltage3 { get; set; } = false;
        public bool NeedsManualVoltage4 { get; set; } = false;

        public bool LeakageCurrentsIsEmpty => LeakageCurrents.FirstOrDefault(m => !m.IsEmpty) == null;
        public bool ResidualVoltagesIsEmpty => ResidualVoltages.FirstOrDefault(m => !m.IsEmpty) == null && ResidualVoltages.FirstOrDefault(m => !m.IsEmptyEx) == null;
        public bool InputAmperagesIsEmpty => InputAmperages.FirstOrDefault(m => !m.IsEmpty) == null;
        public bool InputVoltagesIsEmpty => InputVoltages.FirstOrDefault(m => !m.IsEmpty) == null;
        public bool IGBTOrMosfetLeakageCurrentsIsEmpty => IGBTOrMosfetLeakageCurrents.FirstOrDefault(m => !m.IsEmpty) == null;
        public bool IGBTOrMosfetResidualVoltagesIsEmpty => IGBTOrMosfetResidualVoltages.FirstOrDefault(m => !m.IsEmpty) == null && IGBTOrMosfetResidualVoltages.FirstOrDefault(m => !m.IsEmptyEx) == null;
        public bool IGBTOrMosfetInputAmperagesIsEmpty => IGBTOrMosfetInputAmperages.FirstOrDefault(m => !m.IsEmpty) == null;
        public bool IGBTOrMosfetInputVoltagesIsEmpty => IGBTOrMosfetInputVoltages.FirstOrDefault(m => !m.IsEmpty) == null;

        public bool LeakageCurrentsIsGood => LeakageCurrents.FirstOrDefault(m => !m.IsGood) == null;
        public bool ResidualVoltagesIsGood => ResidualVoltages.FirstOrDefault(m => !m.IsGood) == null && ResidualVoltages.FirstOrDefault(m => !m.IsGoodEx) == null;
        public bool InputAmperagesIsGood => InputAmperages.FirstOrDefault(m => !m.IsGood) == null;
        public bool InputVoltagesIsGood => InputVoltages.FirstOrDefault(m => !m.IsGood) == null;
        public bool IGBTOrMosfetLeakageCurrentsIsGood => IGBTOrMosfetLeakageCurrents.FirstOrDefault(m => !m.IsGood) == null;
        public bool IGBTOrMosfetResidualVoltagesIsGood => IGBTOrMosfetResidualVoltages.FirstOrDefault(m => !m.IsGood) == null && IGBTOrMosfetResidualVoltages.FirstOrDefault(m => !m.IsGoodEx) == null;
        public bool IGBTOrMosfetInputAmperagesIsGood => IGBTOrMosfetInputAmperages.FirstOrDefault(m => !m.IsGood) == null;
        public bool IGBTOrMosfetInputVoltagesIsGood => IGBTOrMosfetInputVoltages.FirstOrDefault(m => !m.IsGood) == null;

        public double? AuxiliaryCurrentPowerSupply1 { get; set; }
        public double? AuxiliaryCurrentPowerSupply2 { get; set; }

        public double? AuxiliaryCurrentPowerSupplyMin1 { get; set; }
        public double? AuxiliaryCurrentPowerSupplyMin2 { get; set; }

        public double? AuxiliaryCurrentPowerSupplyMax1 { get; set; }
        public double? AuxiliaryCurrentPowerSupplyMax2 { get; set; }

        [DependsOn(nameof(AuxiliaryCurrentPowerSupply1), nameof(AuxiliaryCurrentPowerSupplyMin1), nameof(AuxiliaryCurrentPowerSupplyMax1))]
        public bool? AuxiliaryCurrentPowerSupply1IsOk => AuxiliaryCurrentPowerSupplyMin1 == null || AuxiliaryCurrentPowerSupply1?.CompareTo(double.Epsilon) == 0 ? (bool?)null : AuxiliaryCurrentPowerSupplyMin1 < AuxiliaryCurrentPowerSupply1 && AuxiliaryCurrentPowerSupply1 < AuxiliaryCurrentPowerSupplyMax1;

        [DependsOn(nameof(AuxiliaryCurrentPowerSupply2), nameof(AuxiliaryCurrentPowerSupplyMin2), nameof(AuxiliaryCurrentPowerSupplyMax2))]
        public bool? AuxiliaryCurrentPowerSupply2IsOk => AuxiliaryCurrentPowerSupplyMin2 == null || AuxiliaryCurrentPowerSupply2?.CompareTo(double.Epsilon) == 0 ? (bool?)null : AuxiliaryCurrentPowerSupplyMin2 < AuxiliaryCurrentPowerSupply2 && AuxiliaryCurrentPowerSupply2 < AuxiliaryCurrentPowerSupplyMax2;

        public bool? HaveError
        {
            get
            {
                if (ErrorCode == null)
                    return null;
                else
                    return string.IsNullOrEmpty(ErrorCode);
            }
        }
    }
}
