using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;


namespace EngineGDI.DataFiles
{
    public class WallData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string ImagePath { get; set; }
    }

    public class Level
    {
        public List<WallData> WallsInFile { get; set; }
    }
    
    public class LevelLoaderFromJson
    {
        public Level ReadPathFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize<Level>(json);
        }
    }
}
