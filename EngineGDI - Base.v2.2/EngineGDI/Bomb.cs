using System.Collections.Generic;

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
        public List<Explosion> Explosions = new List<Explosion>();

        public bool IsActive => isActive;
        public Transform Transform => transform;

        public Bomb(float initialX, float initialY)
        {
            texture = "Assets/Sprites/Players/Bombita1/bomb1.png";
            transform = new Transform();
            Transform.Position.X = initialX;
            Transform.Position.Y = initialY;
            Transform.Scale.X = 2.5f;
            Transform.Scale.Y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;
            Transform.RealSize.X = pixelSize * Transform.Scale.X;
            Transform.RealSize.Y = pixelSize * Transform.Scale.Y;
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
                    Explosions.Clear();
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
            Explosions.Add(new Explosion(BoxOrigin.Position.X, BoxOrigin.Position.Y));

            //for para spawnear explosiones arriba
            for (int i = 1; i < radius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X, BoxOrigin.Position.Y - i * pixelSize * 2.5f));
            }
            //for para spawnear explosiones abajo
            for (int i = 1; i < radius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X, BoxOrigin.Position.Y + i * pixelSize * 2.5f));
            }
            //for para spawnear explosiones derecha
            for (int i = 1; i < radius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X + i * pixelSize * 2.5f, BoxOrigin.Position.Y));
            }
            //for para spawnear explosiones izquierda
            for (int i = 1; i < radius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X - i * pixelSize * 2.5f, BoxOrigin.Position.Y));
            }
        }

        public void Render()
        {
            if (isActive) Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Angle, transform.Offset.X, transform.Offset.Y);

            for (int i = 0; i < Explosions.Count; i++)
            {
                Explosions[i].Render();
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
            transform.Position.X = initialX;
            transform.Position.Y = initialY;
            transform.Scale.X = 2.5f;
            transform.Scale.Y = 2.5f;
            transform.Angle = 0;
            transform.Offset.X = 0;
            transform.Offset.Y = 0;
            transform.RealSize.X = 16 * transform.Scale.X;
            transform.RealSize.Y = 16 * transform.Scale.Y;
            transform.gameId = GameId.explosion;
        }
        public void Render()
        {
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Angle, transform.Offset.X, transform.Offset.Y);
        }
    }
}