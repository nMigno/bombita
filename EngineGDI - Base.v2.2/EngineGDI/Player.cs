using System;
using System.Collections.Generic;

namespace EngineGDI
{    
    public class Player
    {                
        private enum SpriteState
        {
            idle,
            left,
            right,
            up,
            down,
            die,
            placeBomb,
        }
        private float vel;
        private float startX;
        private float startY;

        public Bomb ActiveBomb { get; private set; }
        //el radio de la bomba arranca en 1
        public int BombRadius { get; private set; }
        public Transform Transform;

        //trackeo estado del player

        public event Action OnLifeChanged;
        public bool Alive = true;
        //lo mato
        public int Lives = 2;

        private Animation sprites;
        private Animation deathSprites;
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

        private SpriteState currentState = SpriteState.idle;

        public Player(float initialx, float initialy, float speed = 200)
        {
            vel = speed;
            startX = initialx;
            startY = initialy;
            Transform = new Transform();
            Transform.Position.X = initialx;
            Transform.Position.Y = initialy;
            Transform.Scale.X = 2.5f;
            Transform.Scale.Y = 2.5f;
            Transform.Angle = 0;
            Transform.RealSize.X = 16f * Transform.Scale.X;
            Transform.RealSize.Y = 16f * Transform.Scale.Y;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;
            Transform.gameId = GameId.player;
            BombRadius = 2;
            LoadSprites();
        }
        private void LoadSprites()
        {
            sprites = new Animation("wip", upFrames, 0.1f, true, 16, 16);
            deathSprites =new Animation("die", dieFrames, 0.1f, false, 16, 16);
        }
        private void ChangeSpeed(float value)
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
            if (Alive)
            {
                Alive = false;
                currentState = SpriteState.die;
                OnLifeChanged?.Invoke();
            }
        }
        public void Inputs()
        {
            if (Alive) {                           
                if (Engine.IsKeyDown(System.Windows.Forms.Keys.W)) {
                
                    currentState = SpriteState.up;
                }
                if (Engine.IsKeyDown(System.Windows.Forms.Keys.A)) {
                
                    currentState = SpriteState.left;
                }
                if (Engine.IsKeyDown(System.Windows.Forms.Keys.S)) {
                
                    currentState = SpriteState.down;
                }
                if (Engine.IsKeyDown(System.Windows.Forms.Keys.D)) {
                
                    currentState = SpriteState.right;
                }
                if (!Engine.IsKeyDown(System.Windows.Forms.Keys.W) &&
                    !Engine.IsKeyDown(System.Windows.Forms.Keys.A) &&
                    !Engine.IsKeyDown(System.Windows.Forms.Keys.S) &&
                    !Engine.IsKeyDown(System.Windows.Forms.Keys.D))
                {
                    currentState = SpriteState.idle;
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

            ActiveBomb = new Bomb(Transform.Position.X, Transform.Position.Y, BombRadius);
            //WIP bomb player colision onPlace
            //Bomb.BombState state = Bomb.BombState.free;
        }

        public void Update(float deltaTime)
        {
            if (currentState == SpriteState.die)
            {
                deathSprites.Update();

                if (deathSprites.IsFinished) Dead = true;
            }
            else
            {
                switch (currentState)
                {
                    case SpriteState.up:
                        Transform.Position.Y -= vel * deltaTime;
                        sprites.Frames = upFrames;
                        break;
                    case SpriteState.left:
                        Transform.Position.X -= vel * deltaTime;
                        sprites.Frames = leftFrames;
                        break;
                    case SpriteState.down:
                        Transform.Position.Y += vel * deltaTime;
                        sprites.Frames = downFrames;
                        break;
                    case SpriteState.right:
                        Transform.Position.X += vel * deltaTime;
                        sprites.Frames = rightFrames;
                        break;
                    case SpriteState.idle:
                        sprites.Frames = downFrames;
                        break;
                    case SpriteState.die:
                        sprites.Frames = dieFrames;
                        break;
                }

                sprites.Update();
            }

            ActiveBomb?.Update(deltaTime);
            if (ActiveBomb != null && ActiveBomb.IsDestroyed)
            {
                ActiveBomb = null;
            }
        }
        public void Render() 
        {
            if (currentState == SpriteState.die)
            {
                Engine.Draw(deathSprites.CurrentFrame, Transform.Position.X, Transform.Position.Y,
                Transform.Scale.X, Transform.Scale.Y, Transform.Angle, Transform.Offset.X, Transform.Offset.Y);
            }
            else
            {
               Engine.Draw(sprites.CurrentFrame, Transform.Position.X, Transform.Position.Y,
               Transform.Scale.X, Transform.Scale.Y, Transform.Angle, Transform.Offset.X, Transform.Offset.Y);
            }           

            ActiveBomb?.Render();
        }

        public void Respawn()
        {
            Transform.Position.X = startX;
            Transform.Position.Y = startY;

            Alive = true;
            Dead = false;
            currentState = SpriteState.idle;
            deathSprites.Reset();
        }
    }
}