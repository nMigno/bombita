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

        public BombState CurrentState { get; set; }
        public bool IsDestroyed { get; private set;  }

        private Transform transform;
        private float timer;
        private float explosionTimer;
        private bool isActive;
        private bool isExplosionActive;
        private string texture;
        private int radius;
        private int pixelSize = 16;
        private ExplosionManager explosionManager;

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
            Transform.gameId = GameId.bomb;

            timer = 3.0f;
            explosionTimer = 1.0f;
            isActive = true;
            isExplosionActive = false;
            radius = 2;

            CurrentState = BombState.colliding;
            IsDestroyed = false;
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
                {
                    explosions.Clear();
                    IsDestroyed = true;
                }
                    
            }
        }

        public void Explode()
        {
            isActive = false;
            // Acá luego metemos el efecto de la explosión que hace daño
            GenerateExplosion(transform);
            explosionTimer = 1.0f;
            isExplosionActive = true;
        }
        public void GenerateExplosion(Transform BoxOrigin)
        {
            explosionManager = GameManager.Instance.explosions;
            explosionManager.AddExplosion(new Explosion(BoxOrigin.Position.x, BoxOrigin.Position.y));

            //for para spawnear explosiones arriba
            for (int i = 1; i < radius; i++)
            {
                explosionManager.AddExplosion(new Explosion(BoxOrigin.Position.x, BoxOrigin.Position.y - i * pixelSize * 2.5f));
            }
            //for para spawnear explosiones abajo
            for (int i = 1; i < radius; i++)
            {
                explosionManager.AddExplosion(new Explosion(BoxOrigin.Position.x, BoxOrigin.Position.y + i * pixelSize * 2.5f));
            }
            //for para spawnear explosiones derecha
            for (int i = 1; i < radius; i++)
            {
                explosionManager.AddExplosion(new Explosion(BoxOrigin.Position.x + i * pixelSize * 2.5f, BoxOrigin.Position.y));
            }
            //for para spawnear explosiones izquierda
            for (int i = 1; i < radius; i++)
            {
                explosionManager.AddExplosion(new Explosion(BoxOrigin.Position.x - i * pixelSize * 2.5f, BoxOrigin.Position.y));
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

        public bool TimeOut { get; private set; }
        private float timer = 1.0f;
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

            TimeOut = false;
        }

        public void Update(float deltaTime)
        {
            timer -= deltaTime;
            if (timer <= 0) TimeOut = true;
        }
        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
