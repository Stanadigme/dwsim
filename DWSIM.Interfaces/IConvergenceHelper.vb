Public Interface IFlashConvergenceHelper






End Interface

Public Interface IConvergenceHelperRequest

    Property RequestType As ConvergenceHelperRequestType

    Property ModelName As String

    Property NumberOfCompounds As Integer

    Property CompoundNames As String()

    Property Temperature As Double?

    Property Pressure As Double?

    Property MassEnthalpy As Double?

    Property MassEntropy As Double?

    Property VaporMolarFraction As Double?

    Property MixtureMolarFlows As Double()

End Interface

Public Interface IConvergenceHelperResponse

    Property RequestType As ConvergenceHelperRequestType

    Property MetaData As IConvergenceHelperMetaData

    Property ModelName As String

    Property IsValid As Boolean

    Property Reason As String

    Property InnerException As Exception

    Property Temperature As Double?

    Property Pressure As Double?

    Property MassEnthalpy As Double?

    Property MassEntropy As Double?

    Property VaporMolarFraction As Double?

    Property MixtureMolarFlows As Double()

    Property VaporMolarFlows As Double()

    Property Liquid1MolarFlows As Double()

    Property Liquid2MolarFlows As Double()

    Property SolidMolarFlows As Double()

    Property KValuesVL1 As Double()

    Property KValuesVL2 As Double()

End Interface

Public Interface IConvergenceHelperMetaData

    Property ModelName As String

    Property NumberOfCompounds As Integer

    Property CompoundNames As String()

    Property TemperatureRange As Tuple(Of Double, Double)

    Property PressureRange As Tuple(Of Double, Double)

    Property MassEnthalpyRange As Tuple(Of Double, Double)

    Property MassEntropyRange As Tuple(Of Double, Double)

    Property VaporMolarFractionRange As Tuple(Of Double, Double)

    Property MolarCompositionRange As Tuple(Of Double(), Double())

End Interface

Public Enum ConvergenceHelperRequestType

    PVFlash = 0
    TVFlash = 1
    PTFlash = 2
    PHFlash = 3
    PSFlash = 4

    GibbsReactorIsothermic = 5
    GibbsReactorAdiabatic = 6

    EquilibriumReactorIsothermic = 8
    EquilibriumReactorAdiabatic = 9

End Enum