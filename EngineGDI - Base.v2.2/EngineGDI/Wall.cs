using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Wall
    {
        public Transform transform;
        string id;
        string texture;

        public Wall(float initialX, float initialY, string path)
        {
            id = "Wall";
            texture = path;
            transform = new Transform();
            transform.Position.x = initialX;
            transform.Position.y = initialY;
            transform.Scale.x = 2.5f;
            transform.Scale.y = 2.5f;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 16 * transform.Scale.x;
            transform.RealSize.y = 16 * transform.Scale.y;
        }

        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}