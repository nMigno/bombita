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
        float startX;
        float startY;

        public Bomb ActiveBomb { get; private set; }
        public Transform transform;
        public Transform bombTransform;

        //trackeo estado del player

        public event Action OnLifeChanged;
        public bool alive = true;
        //lo mato
        public int Lives = 2;

        Animation sprites;
        Animation deathSprites;
        float deathTimer = 0f;
        public bool Dead = false;

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
        List<string> dieFrames = new List<string> {
                "Assets/Sprites/Players/Bombita1/bd1.png" ,
                "Assets/Sprites/Players/Bombita1/bd2.png" ,
                "Assets/Sprites/Players/Bombita1/bd3.png" ,
                "Assets/Sprites/Players/Bombita1/bd4.png" ,
                "Assets/Sprites/Players/Bombita1/bd5.png" ,
            };

        SpriteState CurrentState = SpriteState.idle;

        public Player(float initialx, float initialy, float speed = 200)
        {
            id = "Player";
            vel = speed;
            startX = initialx;
            startY = initialy;
            transform = new Transform();
            transform.Position.x = initialx;
            transform.Position.y = initialy;
            transform.Scale.x = 2.5f;
            transform.Scale.y = 2.5f;
            transform.Angle = 0;
            transform.RealSize.x = 16f * transform.Scale.x;
            transform.RealSize.y = 16f * transform.Scale.y;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.gameId = GameId.player;

            LoadSprites();
        }
        void LoadSprites()
        {
            sprites = new Animation("wip", upFrames, 0.1f, true, 16, 16);
            deathSprites =new Animation("die", dieFrames, 0.1f, false, 16, 16);
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
        //metodo Die suscrito en program CLASEDELEGADO
        public void Die()
        {
            if (alive)
            {
                alive = false;
                CurrentState = SpriteState.die;
                OnLifeChanged?.Invoke();
            }
        }
        public void Inputs()
        {
            if (alive) {                           
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
                if (!Engine.IsKeyDown(System.Windows.Forms.Keys.W) &&
                    !Engine.IsKeyDown(System.Windows.Forms.Keys.A) &&
                    !Engine.IsKeyDown(System.Windows.Forms.Keys.S) &&
                    !Engine.IsKeyDown(System.Windows.Forms.Keys.D))
                {
                    CurrentState = SpriteState.idle;
                }

                if (Engine.IsKeyPressed(System.Windows.Forms.Keys.Space))
                {
                    PlaceBomb();                    
                }

                if (Engine.IsKeyPressed(System.Windows.Forms.Keys.K)){
                    ChangeSpeed(-1.0f);
                }                
            }
            if (Engine.IsKeyPressed(System.Windows.Forms.Keys.J))
            {
                //llamo al evento CLASEDELEGADO
                OnLifeChanged();
            }
    }

        public void PlaceBomb()
        {
            if (ActiveBomb != null && ActiveBomb.IsActive) return;

            float bombX = transform.Position.x;
            float bombY = transform.Position.y;
            float bombSizeX = transform.RealSize.x;
            float bombSizeY = transform.RealSize.y;

            Vector2 bombPos = new Vector2 { x = bombX, y = bombY };
            Vector2 bombSize = new Vector2 { x = bombSizeX, y = bombSizeY };

            ActiveBomb = new Bomb(bombX, bombY, "Assets/Sprites/Players/Bombita1/bomb1.png");
            Bomb.BombState state = Bomb.BombState.free;
        }

        public void Update(float deltaTime)
        {
            if (CurrentState == SpriteState.die)
            {
                deathSprites.Update();

                if (deathSprites.IsFinished) Dead = true;
            }
            else
            {
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
                    case SpriteState.idle:
                        sprites.frames = downFrames;
                        break;
                    case SpriteState.die:
                        sprites.frames = dieFrames;
                        break;
                }

                sprites.Update();
            }

            ActiveBomb?.Update(deltaTime);

            //if (ActiveBomb != null && !ActiveBomb.IsActive) ActiveBomb = null;
        }
        public void Render() 
        {
            if (CurrentState == SpriteState.die)
            {
                Engine.Draw(deathSprites.CurrentFrame, transform.Position.x, transform.Position.y,
                transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
            }
            else
            {
               Engine.Draw(sprites.CurrentFrame, transform.Position.x, transform.Position.y,
               transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
            }           

            ActiveBomb?.Render();
        }

        public void Respawn()
        {
            transform.Position.x = startX;
            transform.Position.y = startY;

            alive = true;
            Dead = false;
            CurrentState = SpriteState.idle;
            deathSprites.Reset();
        }
    }
}