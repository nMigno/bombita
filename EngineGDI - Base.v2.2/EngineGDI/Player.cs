using EngineGDI.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EngineGDI
{
    //encapsular los atributos del player.
    public class Player
    {
        string id;
        float vel;
        public Transform transform;
        
        public Player(float initialx, float initialy, float speed = 200)
        {         
            id = "Player";
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

        public void Inputs(float deltaTime)
        {
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.W)) {
                transform.Position.y -= vel * deltaTime;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.A)) {
                transform.Position.x -= vel * deltaTime;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.S)) {
                transform.Position.y += vel * deltaTime;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.D)) {
                transform.Position.x += vel * deltaTime;
            }
            if (Engine.IsKeyPressed(System.Windows.Forms.Keys.J)){
                ChangeSpeed(1.0f);
            }
            if (Engine.IsKeyPressed(System.Windows.Forms.Keys.K)){
                ChangeSpeed(-1.0f);
            }
        }
        void ChangeSpeed(float value)
        {
            if  (vel <= 500.0f && vel >= 100.0f)
            {
                vel = vel + (value * 20);
            }
            if (vel <= 100.0f) vel = 100.0f;
            if (vel >= 500.0f) vel = 500.0f;
        }
        public void Render() {
            Engine.Draw("Bomberman.png", transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);            
        }

    }
}