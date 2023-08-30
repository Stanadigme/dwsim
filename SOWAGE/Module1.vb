Imports DWSIM.Automation.Automation3
Imports DWSIM.FlowsheetSolver.FlowsheetSolver
Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.UnitOperations.SpecialOps
Imports MongoDB.Bson
Imports MongoDB.Driver

Imports MongoDB.Bson.Serialization

Imports SOWAGE.SowageProcessData




Module Module1


    Function Calculate()
        Console.WriteLine(String.Format("Fichier : "))

        Dim fSName As String = "test_pid_circ"

        Dim fSName2 = Console.ReadLine()

        If fSName2.Length > 0 Then fSName = fSName2

        Dim client As MongoClient = New MongoClient("mongodb://sowage:sowage@localhost:27017/")
        Dim fSNameC As String = fSName + "C"
        Dim db = client.GetDatabase(fSNameC)
        Dim col = db.GetCollection(Of BsonDocument)(fSNameC)

        'col.DeleteMany(Builders(Of XElement).Filter.Where(expression:=XElement.)
        '    )

        Dim interf As New DWSIM.Automation.Automation3
        Dim solver As New DWSIM.FlowsheetSolver.FlowsheetSolver

        Dim sim As DWSIM.Interfaces.IFlowsheet
        sim = interf.LoadFlowsheet(String.Format("D:/git/sowage/{0}.dwxmz", fSName))

        Dim state = sim.GetProcessData()

        Dim objects As Dictionary(Of String, String) = New Dictionary(Of String, String)

        For Each kvp As KeyValuePair(Of String, ISimulationObject) In sim.SimulationObjects
            objects.Add(kvp.Value.GraphicObject.Tag, kvp.Value.GetType.ToString)
        Next
        Dim q = New BsonDocument()
        With q
            .Add("simulation", fSNameC)

        End With

        Dim confColl = db.GetCollection(Of BsonDocument)("Configuration")

        Dim options = New FindOneAndReplaceOptions(Of BsonDocument)() With {
            .IsUpsert = True,
            .ReturnDocument = ReturnDocument.After
        }
        confColl.FindOneAndReplace(q, q.Add("objects", New BsonDocument(objects)), options)



        Dim Controllers = sim.SimulationObjects.Values.Where(Function(x) x.GraphicObject.ObjectType = ObjectType.Controller_PID).ToList
        Dim sch As String = fSName
        Dim schedule = sim.DynamicsManager.ScheduleList(sch)
        Dim integrator = sim.DynamicsManager.IntegratorList(sch)
        sim.DynamicsManager.CurrentSchedule = sch

        'Dim ms(3) As MaterialStream
        'ms(0) = sim.GetObject("1_flash_pipe_out").GetAsObject()
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

            Dim ex = interf.CalculateFlowsheet4(sim)
            For Each controller As PIDController In Controllers
                If controller.Active Then
                    controller.Calculate()
                End If
            Next

            Dim data = sim.GetProcessData
            Dim toWriteData As List(Of BsonDocument) = New List(Of BsonDocument)
            For Each doc As XElement In data
                Dim jsonText As String = Newtonsoft.Json.JsonConvert.SerializeXNode(doc)
                Dim bsonDoc As BsonDocument = BsonSerializer.Deserialize(Of BsonDocument)(jsonText)
                toWriteData.Add(bsonDoc.GetElement(0).Value.ToBsonDocument.Add("step", start))
            Next
            col.InsertMany(toWriteData)

            d2 = Date.Now
            dt = d2 - d1
            Console.WriteLine(String.Format("Step :{0} {1:n3}s", {start, dt.TotalSeconds}))
            Dim i As Integer = 0
            Dim _i As Integer = 0
            'For Each _ms In ms
            '    If _ms IsNot Nothing Then Console.WriteLine((_ms.ToResume))
            'Next
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


    End Function

    Sub Main()

        Calculate()
        'ConvertCollection()
        'ConvertCollections()

    End Sub

End Module
