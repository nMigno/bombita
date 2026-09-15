using EngineGDI.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineGDI
{
    public class Transform
    {
        public Vector2 Position;
        public Vector2 Scale;
        public Vector2 Offset;
        public Vector2 RealSize;

        public float Angle;
        public GameId gameId;
    }
    public enum GameId
    {
        player,
        enemy,
        wall,
        brickWall,
        bomb,
        explosion,
        speedPickUp,
        exit,
    }
    public class ObjectType
    {
        
    }
}
