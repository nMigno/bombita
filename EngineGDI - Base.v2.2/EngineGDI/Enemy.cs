using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Enemy
    {
        string id;
        float vel;
        public Transform transform;
        public Enemy(float initialx, float initialy, float speed = 200)
        {
            id = "Enemy";
            vel = speed;
            transform = new Transform();
            transform.Position.x = initialx;
            transform.Position.y = initialy;
            transform.Scale.x = 1;
            transform.Scale.y = 1;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 32;
            transform.RealSize.y = 32;
        }
        public void Render()
        {
            Engine.Draw("Bomberman.png", transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
