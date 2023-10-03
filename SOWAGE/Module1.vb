Imports DWSIM.Automation.Automation3
Imports DWSIM.FlowsheetSolver.FlowsheetSolver
Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.UnitOperations.SpecialOps
Imports MongoDB.Bson
Imports MongoDB.Driver

Imports MongoDB.Bson.Serialization

Imports SOWAGE.SowageProcessData
Imports System.Xml
Imports DWSIM

Module Module1


    Function SimpleCalculate()

        Console.WriteLine(String.Format("Fichier : "))

        Dim fSName As String = "20230927_104701"

        Dim fSName2 = Console.ReadLine()

        If fSName2.Length > 0 Then fSName = fSName2

        Dim interf As New DWSIM.Automation.Automation3
        Dim solver As New DWSIM.FlowsheetSolver.FlowsheetSolver

        Dim sim As DWSIM.Interfaces.IFlowsheet
        sim = interf.LoadFlowsheet(String.Format("D:/git/sowage/{0}.dwxmz", fSName))

        Dim state = sim.GetProcessData()



        Dim Controllers = sim.SimulationObjects.Values.Where(Function(x) x.GraphicObject.ObjectType = ObjectType.Controller_PID).ToList
        Dim PyControllers = sim.SimulationObjects.Values.Where(Function(x) x.GraphicObject.ObjectType = ObjectType.Controller_Python).ToList
        Dim sch As String = fSName
        Dim schedule = sim.DynamicsManager.ScheduleList.First.Value

        Dim integrator = sim.DynamicsManager.IntegratorList(schedule.CurrentIntegrator)
        sim.DynamicsManager.CurrentSchedule = sch

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
            'For Each controller As PythonController In PyControllers
            '    If controller.Active Then
            '        controller.Calculate()
            '    End If
            'Next


            If ex.Count > 0 Then
                start = final
                Console.WriteLine(ex.ElementAt(0))
            End If
            start += integrationStep
            d2 = Date.Now
            dt = d2 - d1
            Console.WriteLine(String.Format("Step :{0} {1:n3}s", {start, dt.TotalSeconds}))

        End While


        d2 = Date.Now
        dt = d2 - d0

        Console.WriteLine(String.Format("Total :{0}", dt.TotalSeconds))
        Console.Read()
    End Function

    Function Calculate()
        Console.WriteLine(String.Format("Fichier : "))

        Dim fSName As String = "R10-120-1200-10E16r15P1DpMax_Hpress-8300"

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
        Dim initialData = ConvertSimulationData(sim.GetProcessData)

        Dim q = New BsonDocument()
        With q
            .Add("simulation", fSNameC)

        End With

        Dim confColl = db.GetCollection(Of BsonDocument)("Configuration")

        Dim options = New FindOneAndReplaceOptions(Of BsonDocument)() With {
            .IsUpsert = True,
            .ReturnDocument = ReturnDocument.After
        }
        'Dim confData = New BsonDocument()
        'With confData
        '    .Add("objects", New BsonDocument(objects))
        '    .Add("state", New BsonArray(initialData))
        'End With
        confColl.FindOneAndReplace(q, q.Add("objects", New BsonDocument(objects)).Add("state", New BsonArray(initialData)), options)

        col.DeleteMany(New BsonDocument())
        col.Indexes.DropAll()
        Dim indexes = New List(Of CreateIndexModel(Of BsonDocument))
        Dim indexKey = Builders(Of BsonDocument).IndexKeys.Ascending("step")
        Dim index = New CreateIndexModel(Of BsonDocument)(indexKey)
        indexes.Add(index)

        indexKey = Builders(Of BsonDocument).IndexKeys.Combine(Builders(Of BsonDocument).IndexKeys.Ascending("step"), Builders(Of BsonDocument).IndexKeys.Ascending("String"))
        index = New CreateIndexModel(Of BsonDocument)(indexKey)
        indexes.Add(index)

        col.Indexes.CreateMany(indexes)

        Dim Controllers = sim.SimulationObjects.Values.Where(Function(x) x.GraphicObject.ObjectType = ObjectType.Controller_PID).ToList
        Dim PyControllers = sim.SimulationObjects.Values.Where(Function(x) x.GraphicObject.ObjectType = ObjectType.Controller_Python).ToList
        Dim sch As String = fSName
        Dim schedule = sim.DynamicsManager.ScheduleList.First.Value
        'Dim schedule = sim.DynamicsManager.ScheduleList(sch)
        'Dim integrator = schedule.CurrentIntegrator
        Dim integrator = sim.DynamicsManager.IntegratorList(schedule.CurrentIntegrator)
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
            'For Each controller As PythonController In PyControllers
            '    If controller.Active Then
            '        controller.Calculate()
            '    End If
            'Next

            'Dim data As List(Of XElement) = sim.GetProcessData
            Dim data As List(Of XElement) = New List(Of XElement)
            For Each so As SharedClasses.UnitOperations.BaseClass In sim.SimulationObjects.Values
                Select Case so.GraphicObject.ObjectType
                    Case ObjectType.MaterialStream, ObjectType.Pipe, ObjectType.Heater, ObjectType.Cooler
                        data.Add(New XElement("SimulationObject", {so.SaveData().ToArray()}))
                End Select
            Next

            Dim toWriteData As List(Of BsonDocument) = ConvertSimulationData(data, start)
            '    New List(Of BsonDocument)
            'For Each doc As XElement In data
            '    Dim jsonText As String = Newtonsoft.Json.JsonConvert.SerializeXNode(doc)
            '    Dim bsonDoc As BsonDocument = BsonSerializer.Deserialize(Of BsonDocument)(jsonText)
            '    toWriteData.Add(bsonDoc.GetElement(0).Value.ToBsonDocument.Add("step", start))
            'Next
            toWriteData = StripBsonData(toWriteData)

            col.InsertMany(toWriteData)
            If ex.Count > 0 Then
                start = final
                Console.WriteLine(ex.ElementAt(0))
            End If
            start += integrationStep
            d2 = Date.Now
            dt = d2 - d1
            Console.WriteLine(String.Format("Step :{0} {1:n3}s", {start, dt.TotalSeconds}))

        End While


        d2 = Date.Now
        dt = d2 - d0

        Console.WriteLine(String.Format("Total :{0}", dt.TotalSeconds))
        Console.Read()


    End Function

    Sub Main()
        'SimpleCalculate()
        Calculate()
        'ConvertCollection()
        'ConvertCollections()

    End Sub

End Module
