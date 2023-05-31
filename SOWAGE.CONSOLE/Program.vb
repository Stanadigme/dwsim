Imports System
Imports DWSIM
Imports DWSIM.Automation.Automation3
Imports DWSIM.UnitOperations.UnitOperations.Auxiliary

Module Module1

    Sub Main()

        System.IO.Directory.SetCurrentDirectory("C:/Program Files/DWSIM8") ' replace with DWSIM's installation directory on your computer

        'create automation manager
        Dim interf As New DWSIM.Automation.Automation3

        Dim sim As Interfaces.IFlowsheet

        'load Cavett's Problem simulation file
        sim = interf.LoadFlowsheet("samples" & IO.Path.DirectorySeparatorChar & "Cavett's Problem.dwxml")

        '(optional) set a listener to catch solver messages
        'sim.SetMessageListener(Sub(msg As String) CONSOLE.WriteLine(msg)End Sub)

        'use CAPE-OPEN interfaces to manipulate objects
        'Dim feed, vap_out, liq_out As CapeOpen.ICapeThermoMaterialObject

        'feed = sim.GetFlowsheetSimulationObject1("2")
        'vap_out = sim.GetFlowsheetSimulationObject1("8")
        'liq_out = sim.GetFlowsheetSimulationObject1("18")

        'mass flow rate values in kg/s
        Dim flows(3) As Double

        flows(0) = 170.0#
        flows(1) = 180.0#
        flows(2) = 190.0#
        flows(3) = 200.0#

        'vapor and liquid flows
        Dim vflow, lflow As Double

        'For i = 0 To flows.Length - 1
        '    'set feed mass flow
        '    feed.SetProp("totalflow", "overall", Nothing, "", "mass", New Double() {flows(i)})
        '    'calculate the flowsheet (run the simulation)
        '    CONSOLE.WriteLine("Running simulation with F = " & flows(i) & " kg/s, please wait...")
        '    interf.CalculateFlowsheet(sim, Nothing)
        '    'check for errors during the last run
        '    If sim.Solved = False Then
        '        CONSOLE.WriteLine("Error solving flowsheet: " & sim.ErrorMessage)
        '    End If
        '    'get vapor outlet mass flow value
        '    vflow = vap_out.GetProp("totalflow", "overall", Nothing, "", "mass")(0)
        '    'get liquid outlet mass flow value
        '    lflow = liq_out.GetProp("totalflow", "overall", Nothing, "", "mass")(0)
        '    'display results
        '    CONSOLE.WriteLine("Simulation run #" & (i + 1) & " results:" & vbCrLf & "Feed: " & flows(i) & ", Vapor: " & vflow & ", Liquid: " & lflow & " kg/s" & vbCrLf & "Mass balance error: " & (flows(i) - vflow - lflow) & " kg/s")
        'Next

        'CONSOLE.WriteLine("Finished OK! Press any key to close.")
        'CONSOLE.ReadKey()

    End Sub

End Module