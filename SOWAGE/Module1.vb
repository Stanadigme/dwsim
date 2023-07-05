Imports DWSIM.Automation.Automation3
Imports DWSIM.FlowsheetSolver.FlowsheetSolver
Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.UnitOperations.SpecialOps

Module Module1

    Sub Main()

        Dim path As String = "D:/git/sowage/"
        Dim interf As New DWSIM.Automation.Automation3
        Dim solver As New DWSIM.FlowsheetSolver.FlowsheetSolver

        Dim sim As DWSIM.Interfaces.IFlowsheet
        sim = interf.LoadFlowsheet("D:/git/sowage/test.dwxmz")

        Dim Controllers = sim.SimulationObjects.Values.Where(Function(x) x.GraphicObject.ObjectType = ObjectType.Controller_PID).ToList
        Dim sch As String = "hour"
        Dim schedule = sim.DynamicsManager.ScheduleList(sch)
        Dim integrator = sim.DynamicsManager.IntegratorList(sch)
        sim.DynamicsManager.CurrentSchedule = sch

        Dim ms(3) As MaterialStream
        ms(0) = sim.GetObject("1_flash_pipe_out").GetAsObject()
        'ms(0) = sim.GetObject("calo_hx_in").GetAsObject()
        'ms(1) = sim.GetObject("calo_hx_out").GetAsObject()
        'ms(2) = sim.GetObject("edm_hx_in").GetAsObject()
        'ms(3) = sim.GetObject("edm_hx_out").GetAsObject()

        sim.DynamicMode = True
        sim.LoadProcessData(sim.StoredSolutions(schedule.InitialFlowsheetStateID))
        solver.InitFlowsheet(sim)

        Dim start As Double = 0
        Dim final = integrator.Duration.TotalSeconds
        Dim integrationStep = integrator.IntegrationStep.TotalSeconds
        integrator.ShouldCalculateControl = True
        integrator.ShouldCalculateEquilibrium = True
        integrator.ShouldCalculatePressureFlow = True
        Dim d0, d1, d2 As Date
        Dim dt As TimeSpan
        d0 = Date.Now
        While start < final
            d1 = Date.Now
            For Each controller As PIDController In Controllers
                If controller.Active Then
                    Try
                        controller.Calculate()
                    Catch exc As Exception
                        Throw exc
                    End Try
                End If
            Next
            Dim ex = interf.CalculateFlowsheet4(sim)
            d2 = Date.Now
            dt = d2 - d1
            Console.WriteLine(String.Format("Step :{0} {1:n3}s", {start, dt.TotalSeconds}))
            Dim i As Integer = 0
            Dim _i As Integer = 0
            For Each _ms In ms
                If _ms IsNot Nothing Then Console.WriteLine((_ms.ToResume))
            Next
            'Console.WriteLine((ms.ToResume + String.Format(" {0:n2}s", dt.TotalSeconds)))
            If ex.Count > 0 Then
                start = final
                Console.WriteLine(ex.ElementAt(0))
            End If
            start += integrationStep
        End While
        d2 = Date.Now
        dt = d2 - d0
        Console.WriteLine(String.Format("Total :{0}", dt.TotalSeconds))
        Console.Read()

    End Sub

End Module
