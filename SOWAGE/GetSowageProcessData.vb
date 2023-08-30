﻿Imports DWSIM.Thermodynamics.Utilities.Hypos.Methods
Imports DWSIM.UnitOperations.UnitOperations
Imports MongoDB.Bson
Imports MongoDB.Driver

Module SowageProcessData

    Function GetSowageProcessData(data As List(Of BsonDocument)
                                  ) As List(Of BsonDocument)
        Dim sowageData As List(Of BsonDocument) = New List(Of BsonDocument)

        For Each element As BsonDocument In data
            Dim doc As BsonDocument
            doc.Add(element.GetElement("step"))
            doc.Add(element.GetElement("String"))

            Select Case element.GetValue("Type").AsString
                Case "DWSIM.Thermodynamics.Streams.MaterialStream"

                    doc.Add(element.GetElement("Phases"))
                    sowageData.Add(doc)
            End Select

        Next


        Return sowageData


    End Function

    Function ConvertCollections()
        Console.WriteLine(String.Format("Starting ConvertCollections for "))
        Dim dbs As List(Of String) = ListDBs()
        Console.WriteLine(String.Format("{0}", dbs.ToArray))
        Console.WriteLine(String.Format("Validate ?"))
        Console.Read()
        For Each db As String In dbs
            ConvertCollection(db)
        Next

    End Function

    Function ConvertCollection()
        Console.WriteLine(String.Format("Fichier : "))
        Dim fSName As String = "R90-600E4Aq1i5Dp10132530"

        Dim fSName2 = Console.ReadLine()

        If fSName2.Length > 0 Then fSName = fSName2

        ConvertCollection(fSName)

        Console.Read()

    End Function

    Function ConvertCollection(collName As String)

        'Dim fSName = collName

        Console.WriteLine(String.Format("Coll : {0}", collName))

        'Dim fSName As String = "R90-600E4Aq1i5Dp10132530"

        'Dim fSName2 = Console.ReadLine()

        'If fSName2.Length > 0 Then fSName = fSName2

        Dim d0, d1, d2 As Date
        Dim dt As TimeSpan
        d0 = Date.Now

        Dim client As MongoClient = New MongoClient("mongodb://sowage:sowage@localhost:27017/")
        'Dim fSNameC As String = fSName + "C"
        Dim fSNameC As String = collName
        Dim db = client.GetDatabase(fSNameC)
        Dim source = db.GetCollection(Of BsonDocument)(fSNameC)
        Dim destination = db.GetCollection(Of BsonDocument)(fSNameC + ".light")
        Dim deleteQ = New BsonDocument()
        destination.DeleteMany(deleteQ)
        Dim confColl = db.GetCollection(Of BsonDocument)("Configuration")
        Dim objQ = New BsonDocument().Add("simulation", fSNameC)

        Dim objectsConf = confColl.Find(objQ).ToList
        'Dim objects As Dictionary(Of String, String) = objectsConf.First.GetElement("objects").Value.ToBson.ToDictionary(Of String)
        Dim objects = objectsConf.First.GetElement("objects").Value.ToBsonDocument
        Dim types As String() = {"DWSIM.Thermodynamics.Streams.MaterialStream", "DWSIM.UnitOperations.UnitOperations.Pipe"}


        For Each objet As BsonElement In objects
            If types.Contains(objet.Value.AsString) Then
                Dim q = New BsonDocument()
                q.Add("String", objet.Name)
                Dim docs = source.Find(q).ToList
                Dim options = New FindOneAndReplaceOptions(Of BsonDocument)() With {
                    .IsUpsert = True,
                    .ReturnDocument = ReturnDocument.After
                }
                Dim docList As List(Of BsonDocument) = New List(Of BsonDocument)
                For Each element As BsonDocument In docs
                    Dim doc As BsonDocument = New BsonDocument
                    With doc
                        .Add(element.GetElement("step"))
                        .Add(element.GetElement("String"))
                        .Add(element.GetElement("Type"))
                    End With


                    Select Case objet.Value.AsString
                        Case "DWSIM.Thermodynamics.Streams.MaterialStream"
                            'destination.FindOneAndReplace(doc, doc.Add(element.GetElement("Phases")), options)
                            'Console.WriteLine(String.Format("{0} {1}", {element.GetElement("String").Value.AsString, element.GetElement("step").Value.AsDouble}))
                            doc.Add(element.GetElement("Phases"))
                            docList.Add(doc)
                        Case "DWSIM.UnitOperations.UnitOperations.Pipe"
                            Dim towrite = doc
                            With towrite
                                .Add(element.GetElement("Profile"))
                                .Add(element.GetElement("PressureDrop_Static"))
                                .Add(element.GetElement("PressureDrop_Friction"))
                                .Add(element.GetElement("DeltaP"))
                                .Add(element.GetElement("DeltaT"))
                            End With
                            'destination.FindOneAndReplace(doc, towrite, options)
                            'Console.WriteLine(String.Format("{0} {1}", {element.GetElement("String").Value.AsString, element.GetElement("step").Value.AsDouble}))
                            docList.Add(doc)
                        Case Else
                            Console.WriteLine(String.Format("{0} non traité", {objet.Value.AsString, element.GetElement("step").Value.AsDouble}))
                    End Select
                Next
                destination.InsertMany(docList)
                d2 = Date.Now
                dt = d2 - d0
                Console.WriteLine(String.Format("{0} traité : {1}", {objet.Name, dt.TotalSeconds}))
            End If
        Next

        d2 = Date.Now
        dt = d2 - d0

        Console.WriteLine(String.Format("Total :{0}", dt.TotalSeconds))

        Console.WriteLine(String.Format("End {0}", {collName}))
    End Function

    Function ListDBs() As List(Of String)
        Dim client As MongoClient = New MongoClient("mongodb://sowage:sowage@localhost:27017/")
        Dim _dbs = client.ListDatabaseNames.ToList
        Dim dbs As List(Of String) = New List(Of String)
        For Each dbName As String In _dbs
            Dim fSNameC As String = dbName ' + "C"
            Dim db = client.GetDatabase(fSNameC)
            Dim confColl = db.GetCollection(Of BsonDocument)("Configuration")
            Dim lightColl = db.GetCollection(Of BsonDocument)(fSNameC + ".light")
            Dim confQ = New BsonDocument()
            confQ.Add("simulation", fSNameC)
            'Dim lightQ = New BsonDocument()
            Dim confCollCount = confColl.CountDocuments(confQ)
            Dim lightCollCount = lightColl.EstimatedDocumentCount
            If confCollCount > 0 And lightCollCount = 0 Then dbs.Add(dbName)
        Next

        Return dbs
    End Function



End Module