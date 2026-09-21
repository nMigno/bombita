using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;


namespace EngineGDI.DataFiles
{
    //si lo que queremos sacar el json son solo posiciones, hagmamos una sola clase
    //y hagamos que todas hereden de ella >:}
    public class ElementData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string ImagePath { get; set; }
    }

    public class WallData : ElementData { }
    public class BrickWallData : ElementData { }
    public class DoorLevelExitData : ElementData { }
    public class Position : ElementData { }

    public class Level
    {
        public List<WallData> WallsInFile { get; set; }
        public List<BrickWallData> BricksInFile { get; set; }
        public List<DoorLevelExitData> DoorsInFile { get; set; }
    }

    public class EnemyRoute
    {
        public List<Position> Path {  get; set; }
    }
    public class PathData
    {
        public List<EnemyRoute> EnemyRoutes { get; set; }
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

    public static class PositionData
    {
        public static PathData ReadPathFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize<PathData>(json);
        }
    }
}
