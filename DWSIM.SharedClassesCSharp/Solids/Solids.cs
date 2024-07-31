﻿using DWSIM.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DWSIM.SharedClassesCSharp.Solids
{
    public class AdditionalSolidPhaseProperties : IAdditionalSolidPhaseProperties, ICustomXMLSerialization
    {
        public bool LoadData(List<XElement> data)
        {
            XMLSerializer.XMLSerializer.Deserialize(this, data);
            return true;
        }

        public List<XElement> SaveData()
        {
            return XMLSerializer.XMLSerializer.Serialize(this);
        }
    }

    public class SolidParticleSize : ISolidParticleSize, ICustomXMLSerialization
    {
        public double Size { get; set; } = 0.0;

        public double MassFraction { get; set; } = 0.0;

        public bool LoadData(List<XElement> data)
        {
            XMLSerializer.XMLSerializer.Deserialize(this, data);
            return true;
        }

        public List<XElement> SaveData()
        {
            return XMLSerializer.XMLSerializer.Serialize(this);
        }
    }

    public class SolidShapeCurve : ISolidShapeCurve, ICustomXMLSerialization
    {
        public string UniqueID { get; set; } = System.Guid.NewGuid().ToString();

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public string Shape { get; set; } = "";

        public List<ISolidParticleSize> Data { get; set; } = new List<ISolidParticleSize>();
       
        public bool LoadData(List<XElement> data)
        {
            XMLSerializer.XMLSerializer.Deserialize(this, data);
            if (data.Last().Name == "Data")
            {
                var cdata = data.Last().Elements().ToList();
                foreach (XElement xel in cdata)
                {
                    try
                    {
                        var obj = new SolidParticleSize();
                        obj.LoadData(xel.Elements().ToList());
                        Data.Add(obj);
                    }
                    catch { }
                }
            }
            return true;
        }

        public List<XElement> SaveData()
        {
            var data = XMLSerializer.XMLSerializer.Serialize(this);
            data.Add(new XElement("Data"));
            var cx = data.Last();
            foreach (var d in Data)
            {
                cx.Add(new XElement("Data", ((ICustomXMLSerialization)d).SaveData().ToArray()));
            }
            return data;
        }
    }

    public class SolidParticleSizeDistribution : ISolidParticleSizeDistribution, ICustomXMLSerialization
    {
        public string UniqueID { get; set; } = System.Guid.NewGuid().ToString();

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public List<ISolidShapeCurve> Curves { get; set; } = new List<ISolidShapeCurve>();

        public bool LoadData(List<XElement> data)
        {
            XMLSerializer.XMLSerializer.Deserialize(this, data);
            if (data.Last().Name == "Curves")
            {
                var cdata = data.Last().Elements().ToList();
                foreach (XElement xel in cdata)
                {
                    try
                    {
                        var obj = new SolidShapeCurve();
                        obj.LoadData(xel.Elements().ToList());
                        Curves.Add(obj);
                    }
                    catch{ }
                }
            }
            return true;
        }

        public List<XElement> SaveData()
        {
            var data = XMLSerializer.XMLSerializer.Serialize(this);
            data.Add(new XElement("Curves"));
            var cx = data.Last();
            foreach (var curve in Curves)
            {
                cx.Add(new XElement("Curve", ((ICustomXMLSerialization)curve).SaveData().ToArray()));
            }
            return data;
        }
    }

    public class SolidParticleData : ISolidParticleData, ICustomXMLSerialization
    {
        public string UniqueID { get; set; } = System.Guid.NewGuid().ToString();

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public Dictionary<string, string> Distributions { get; set; } = new Dictionary<string, string>();

        public ISolidParticleData Clone()
        {
            var clone = new SolidParticleData();
            clone.LoadData(SaveData());
            return clone;
        }

        public bool LoadData(List<XElement> data)
        {
            XMLSerializer.XMLSerializer.Deserialize(this, data);
            return true;
        }

        public List<XElement> SaveData()
        {
            return XMLSerializer.XMLSerializer.Serialize(this);
        }
    }

}