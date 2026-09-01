using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;


namespace EngineGDI.DataFiles
{
    public class Position
    {
        public float x; // { get; set; }
        public float y; // { get; set; }
    }

    public class PathData
    {
        public List<Position> path { get; set; }
        public float speed { get; set; }
        public float life { get; set; }
    }

    public class PositionData
    {
        public PathData ReadPathFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize<PathData>(json);
        }
    }
}
