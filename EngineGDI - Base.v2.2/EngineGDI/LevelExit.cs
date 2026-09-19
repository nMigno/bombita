using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class LevelExit
    {
        string id;
        string texture;
        Transform transform;
        bool opened = false;
        int pixelSize = 16;
        public bool Opened => opened;
        public Transform Transform => transform;


        public LevelExit(float posX, float posY)
        {
            id = "Exit"; 
            texture = "Assets/Sprites/Icons/Exit.png";
            transform = new Transform();
            Transform.Position.x = posX;
            Transform.Position.y = posY;
            Transform.Scale.x = 2.5f;
            Transform.Scale.y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.x = 0;
            Transform.Offset.y = 0;
            Transform.RealSize.x = pixelSize * Transform.Scale.x;
            Transform.RealSize.y = pixelSize * Transform.Scale.y;
            Transform.gameId = GameId.exit;
        }

        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
