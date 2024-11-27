Imports DWSIM.Interfaces.Enums

Public Class Dimension

    Implements IDimension, ICustomXMLSerialization

    Public Property ID As String = "" Implements IDimension.ID

    Public Property Name As DimensionName = DimensionName.NotDefined Implements IDimension.Name

    Public Property Value As Double Implements IDimension.Value

    Public Property IsUserDefined As Boolean = False Implements IDimension.IsUserDefined

    Public Sub New()

        ID = Guid.NewGuid().ToString()

    End Sub

    Public Function SaveData() As List(Of XElement) Implements ICustomXMLSerialization.SaveData
        Return XMLSerializer.XMLSerializer.Serialize(Me)
    End Function

    Public Function LoadData(data As List(Of XElement)) As Boolean Implements ICustomXMLSerialization.LoadData
        Return XMLSerializer.XMLSerializer.Deserialize(Me, data)
    End Function

End Class
