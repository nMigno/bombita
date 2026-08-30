using EngineGDI.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EngineGDI
{    
    public class Player
    {
        enum SpriteState
        {
            idle,
            left,
            right,
            up,
            down,
            die,
            placeBomb,
        }
        string id;
        float vel;
        public Transform transform;

        Animation sprites;

        //pendiente manejo de carga via json

        List<string> leftFrames = new List<string> {
                "Assets/Sprites/Players/Bombita1/b1.png" ,
                "Assets/Sprites/Players/Bombita1/b2.png" ,
                "Assets/Sprites/Players/Bombita1/b3.png" ,
            };
        List<string> downFrames = new List<string> {
                "Assets/Sprites/Players/Bombita1/b4.png" ,
                "Assets/Sprites/Players/Bombita1/b5.png" ,
                "Assets/Sprites/Players/Bombita1/b6.png" ,
            };
        List<string> rightFrames = new List<string> {
                "Assets/Sprites/Players/Bombita1/b7.png" ,
                "Assets/Sprites/Players/Bombita1/b8.png" ,
                "Assets/Sprites/Players/Bombita1/b9.png" ,
            };
        List<string> upFrames = new List<string> {
                "Assets//Sprites//Players//Bombita1//b10.png" ,
                "Assets//Sprites//Players//Bombita1//b11.png" ,
                "Assets//Sprites//Players//Bombita1//b12.png" ,
            };

        SpriteState CurrentState = SpriteState.idle;

        public Player(float initialx, float initialy, float speed = 200)
        {
            id = "Player";
            vel = speed;
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

            LoadSprites();
        }
        void LoadSprites()
        {
            sprites = new Animation("wip", upFrames, 0.1f, true, 16, 16);
        }
        void ChangeSpeed(float value)
        {
            if (vel <= 500.0f && vel >= 100.0f)
            {
                vel = vel + (value * 20);
            }
            if (vel <= 100.0f) vel = 100.0f;
            if (vel >= 500.0f) vel = 500.0f;
        }
        public void Inputs()
        {                          
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.W)) {
                
                CurrentState = SpriteState.up;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.A)) {
                
                CurrentState = SpriteState.left;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.S)) {
                
                CurrentState = SpriteState.down;
            }
            if (Engine.IsKeyDown(System.Windows.Forms.Keys.D)) {
                
                CurrentState = SpriteState.right;
            }
            if (Engine.IsKeyPressed(System.Windows.Forms.Keys.J)){
        
                ChangeSpeed(1.0f);
            }
            if (Engine.IsKeyPressed(System.Windows.Forms.Keys.K)){
                ChangeSpeed(-1.0f);
            }            
        }        
        public void Update(float deltaTime)
        {
            sprites.Update();

            switch (CurrentState)
            {
                case SpriteState.up:
                    transform.Position.y -= vel * deltaTime;
                    sprites.frames = upFrames;
                    break;
                case SpriteState.left:
                    transform.Position.x -= vel * deltaTime;
                    sprites.frames = leftFrames;
                    break;
                case SpriteState.down:
                    transform.Position.y += vel * deltaTime;
                    sprites.frames = downFrames;
                    break;
                case SpriteState.right:
                    transform.Position.x += vel * deltaTime;
                    sprites.frames = rightFrames;
                    break;
            }
        }
        public void Render() {
            Engine.Draw(sprites.CurrentFrame, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }

    }
}