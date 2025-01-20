using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWSIM.Interfaces;

namespace DWSIM.AI.ConvergenceHelper
{
    public class ConvergenceHelper : IConvergenceHelper
    {
        public IConvergenceHelperResponse GetEstimates(ConvergenceHelperRequestType RequestType, string ModelName, double[] MixtureMolarFlows, double? Temperature, double? Pressure, double? VaporMolarFraction, double? MassEnthalpy, double? MassEntropy)
        {
            return new ConvergenceHelperResponse();
        }

        public void StoreResults(ConvergenceHelperRequestType RequestType, string ModelName, double[] MixtureMolarFlows, double? Temperature, double? Pressure, double? VaporMolarFraction, double? MassEnthalpy, double? MassEntropy, double[] VaporMolarFlows, double[] Liquid1MolarFlows, double[] Liquid2MolarFlows, double[] SolidMolarFlows, double[] KValuesVL1, double[] KValuesVL2)
        {
            throw new NotImplementedException();
        }
    }

    public class ConvergenceHelperTrainingData : IConvergenceHelperTrainingData
    {
        public ConvergenceHelperRequestType RequestType { get; set; }
        public string ModelName { get; set; }
        public int NumberOfCompounds { get; set; }
        public string[] CompoundNames { get; set; }
        public float? Temperature { get; set; }
        public float? Temperature2 { get; set; }
        public float? Pressure { get; set; }
        public float? MassEnthalpy { get; set; }
        public float? MassEntropy { get; set; }
        public float? VaporMolarFraction { get; set; }
        public float[] MixtureMolarFlows { get; set; }
        public float[] MixtureMolarFlows2 { get; set; }
        public float[] VaporMolarFlows { get; set; }
        public float[] Liquid1MolarFlows { get; set; }
        public float[] Liquid2MolarFlows { get; set; }
        public float[] SolidMolarFlows { get; set; }
        public float[] KValuesVL1 { get; set; }
        public float[] KValuesVL2 { get; set; }
    }

    public class ConvergenceHelperRequest : IConvergenceHelperRequest
    {
        public ConvergenceHelperRequestType RequestType { get; set; }
        public string ModelName { get; set; } = "";
        public int NumberOfCompounds { get; set; }
        public string[] CompoundNames { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? MassEnthalpy { get; set; }
        public double? MassEntropy { get; set; }
        public double? VaporMolarFraction { get; set; }
        public double[] MixtureMolarFlows { get; set; }
    }

    public class ConvergenceHelperResponse : IConvergenceHelperResponse
    {
        public ConvergenceHelperRequestType RequestType { get; set; } = ConvergenceHelperRequestType.PVFlash;
        public IConvergenceHelperMetaData MetaData { get; set; } = new ConvergenceHelperMetaData();
        public string ModelName { get; set; } = "";
        public bool IsValid { get; set; }
        public string Reason { get; set; } = "";
        public Exception InnerException { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? MassEnthalpy { get; set; }
        public double? MassEntropy { get; set; }
        public double? VaporMolarFraction { get; set; }
        public double[] MixtureMolarFlows { get; set; }
        public double[] VaporMolarFlows { get; set; }
        public double[] Liquid1MolarFlows { get; set; }
        public double[] Liquid2MolarFlows { get; set; }
        public double[] SolidMolarFlows { get; set; }
        public double[] KValuesVL1 { get; set; }
        public double[] KValuesVL2 { get; set; }
    }

    public class ConvergenceHelperMetaData : IConvergenceHelperMetaData
    {
        public string ModelName { get; set; } = "";
        public int NumberOfCompounds { get; set; }
        public string[] CompoundNames { get; set; }
        public Tuple<double, double> TemperatureRange { get; set; }
        public Tuple<double, double> PressureRange { get; set; }
        public Tuple<double, double> MassEnthalpyRange { get; set; }
        public Tuple<double, double> MassEntropyRange { get; set; }
        public Tuple<double, double> VaporMolarFractionRange { get; set; }
        public Tuple<double[], double[]> MolarCompositionRange { get; set; }
    }
}
