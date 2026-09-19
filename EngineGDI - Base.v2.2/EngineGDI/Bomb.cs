using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Bomb
    {
        public enum BombState
        {
            colliding,
            free
        };

        private Transform transform;
        private float timer;
        private bool isActive;
        private string texture;
        private int radius;
        private int pixelSize = 16;


        //la bomba va a tener explosiones, y si me das tiempo, una mecha tambien 
        public List<Explosion> explosions = new List<Explosion>();

        public bool IsActive => isActive;
        public Transform Transform => transform;

        public Bomb(float initialX, float initialY, string path)
        {
            texture = path;
            transform = new Transform();
            Transform.Position.x = initialX;
            Transform.Position.y = initialY;
            Transform.Scale.x = 2.5f;
            Transform.Scale.y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.x = 0;
            Transform.Offset.y = 0;
            Transform.RealSize.x = pixelSize * Transform.Scale.x;
            Transform.RealSize.y = pixelSize * Transform.Scale.y;

            timer = 3.0f;
            isActive = true;
            radius = 2;
        }

        public void Update(float deltaTime)
        {
            if (!isActive) return;

            timer -= deltaTime;
            if (timer <= 0) Explode();            
        }

        public void Explode()
        {
            isActive = false;
            // Acá luego metemos el efecto de la explosión que hace daño
            GenerateExplosion(transform);
        }
        public void GenerateExplosion(Transform BoxOrigin)
        {
            explosions.Add(new Explosion(BoxOrigin.Position.x, BoxOrigin.Position.y));

            //for para spawnear explosiones arriba
            for (int i = 1; i < radius; i++)
            {
                explosions.Add(new Explosion(BoxOrigin.Position.x, BoxOrigin.Position.y - i * pixelSize * 2.5f));
            }
            //for para spawnear explosiones abajo
            for (int i = 1; i < radius; i++)
                {
                    explosions.Add(new Explosion(BoxOrigin.Position.x, BoxOrigin.Position.y + i * pixelSize * 2.5f));
                }
            //for para spawnear explosiones derecha
            for (int i = 1; i < radius; i++)
                {
                    explosions.Add(new Explosion(BoxOrigin.Position.x + i * pixelSize * 2.5f, BoxOrigin.Position.y));
                }
            //for para spawnear explosiones izquierda
            for (int i = 1; i < radius; i++)
                {
                    explosions.Add(new Explosion(BoxOrigin.Position.x - i * pixelSize * 2.5f, BoxOrigin.Position.y));
                }
        }

        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);

            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Render();
            }
        }
    }
    public class Explosion
    {
        public Transform transform;
        private string texture;
        Transform Transform => transform;
        public Explosion(float initialX, float initialY)
        {
            transform = new Transform();
            texture = "Assets/Sprites/Players/Bombita1/explosion.png";
            transform.Position.x = initialX;
            Transform.Position.y = initialY;
            Transform.Scale.x = 2.5f;
            Transform.Scale.y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.x = 0;
            Transform.Offset.y = 0;
            Transform.RealSize.x = 16 * Transform.Scale.x;
            Transform.RealSize.y = 16 * Transform.Scale.y;
            transform.gameId = GameId.explosion;
        }
        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
