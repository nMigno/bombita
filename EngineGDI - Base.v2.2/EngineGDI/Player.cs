using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    //encapsular los atributos del player.
    public class Player
    {
        string id;
        float posx;
        float posy;
        float vel;

        public Player(int number)
        {
            id = "Bot" + number;
            posx = 50;
            posy = 50;
        }

        public Player(float initialx, float initialy, float speed = 200)
        {
            id = "Player";
            posx = initialx;
            posy = initialy;
            vel = speed;

        }
        public void Inputs(float deltaTime)
        {
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.W)) {
                posx -= vel * deltaTime;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.A)) {
                posy -= vel * deltaTime;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.S)) {
                posx += vel * deltaTime;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.D)) {
                posy += vel * deltaTime;
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
            Engine.Draw("Bomberman.png", posy, posx, 1, 1, 0, 0, 0);
            
        }
    }
}