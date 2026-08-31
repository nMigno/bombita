using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    internal class Wall
    {
        public Transform transform;
        string id;
        string texture;

        public Wall(float initialx, float initialy, string path)
        {
            id = "Wall";
            texture = path;
            transform = new Transform();
            transform.Position.x = initialx;
            transform.Position.y = initialy;
            transform.Scale.x = 2;
            transform.Scale.y = 2;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 32;
            transform.RealSize.y = 32;
        }
        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
