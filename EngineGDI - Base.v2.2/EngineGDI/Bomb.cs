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
        private float explosionTimer;
        private bool isActive;
        private bool isExplosionActive;
        private string texture;
        private int radius;
        private int pixelSize = 16;
        public BombState CurrentState;

        public bool IsDestroyed;

        //la bomba va a tener explosiones, y si me das tiempo, una mecha tambien 
        public List<Explosion> explosions = new List<Explosion>();

        public bool IsActive => isActive;
        public Transform Transform => transform;

        public Bomb(float initialX, float initialY)
        {
            texture = "Assets/Sprites/Players/Bombita1/bomb1.png";
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
            IsDestroyed = false;

            timer = 2.0f;
            explosionTimer = 0.5f;
            isActive = true;
            isExplosionActive = false;
            radius = 2;
        }

        public void Update(float deltaTime)
        {
            if (isActive)
            {
                timer -= deltaTime;
                if (timer <= 0)
                    Explode();
            }
            if (isExplosionActive)
            {
                explosionTimer -= deltaTime;
                if (explosionTimer <= 0) 
                    explosions.Clear();
            }
        }

        public void Explode()
        {
            isActive = false;
            // Acá luego metemos el efecto de la explosión que hace daño
            GenerateExplosion(transform);
            explosionTimer = 1.0f;
            isExplosionActive = true;
            IsDestroyed = false;
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
            if (isActive) Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);

            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Render();
            }
        }
    }
    public class Explosion
    {
        private Transform transform;
        private string texture;
        public Transform Transform => transform;
        public Explosion(float initialX, float initialY)
        {
            transform = new Transform();
            texture = "Assets/Sprites/Players/Bombita1/explosion.png";
            transform.Position.x = initialX;
            transform.Position.y = initialY;
            transform.Scale.x = 2.5f;
            transform.Scale.y = 2.5f;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 16 * transform.Scale.x;
            transform.RealSize.y = 16 * transform.Scale.y;
            transform.gameId = GameId.explosion;
        }
        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}