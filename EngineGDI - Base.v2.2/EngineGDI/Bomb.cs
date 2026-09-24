using System;
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

        public Bomb(float initialX, float initialY, int newRadius)
        {
            texture = "Assets/Sprites/Players/Bombita1/bomb1.png";
            transform = new Transform();


            Transform.Scale.X = 2.5f;
            Transform.Scale.Y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;

            Transform.RealSize.X = pixelSize * Transform.Scale.X;
            Transform.RealSize.Y = pixelSize * Transform.Scale.Y;

            Transform.Position.X = Transform.RealSize.X * NormalizeCoord(initialX);
            Transform.Position.Y = Transform.RealSize.Y * NormalizeCoord(initialY);
            IsDestroyed = false;

            timer = 2.0f;
            explosionTimer = 0.5f;
            isActive = true;
            isExplosionActive = false;
            radius = CheckMinimunRadius(newRadius);



        }
        // checkeo minimo para asegurarlos que la bomba explote y no sea solo en el espacio de la bomba
        private int CheckMinimunRadius(int value)
        {
            if (value <= 1) return 1;
            else return value;
        }
        private float NormalizeCoord(float value)
        {
            double normalizer = Math.Round(value / pixelSize);
            float normalizeValue = (int)normalizer;
            return (float)normalizeValue;
        }
        public void Explode()
        {
            isActive = false;
            GenerateExplosion(transform);
            explosionTimer = 1.0f;
            isExplosionActive = true;
        }
        public void GenerateExplosion(Transform BoxOrigin)
        {
            Explosions.Add(new Explosion(BoxOrigin.Position.X, BoxOrigin.Position.Y));

            int actualRadius = radius + 1;

            //for para spawnear explosiones arriba
            for (int i = 1; i < actualRadius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X, BoxOrigin.Position.Y - i * pixelSize * 2.5f));
            }
            //for para spawnear explosiones abajo
            for (int i = 1; i < actualRadius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X, BoxOrigin.Position.Y + i * pixelSize * 2.5f));
            }
            //for para spawnear explosiones derecha
            for (int i = 1; i < actualRadius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X + i * pixelSize * 2.5f, BoxOrigin.Position.Y));
            }
            //for para spawnear explosiones izquierda
            for (int i = 1; i < actualRadius; i++)
            {
                Explosions.Add(new Explosion(BoxOrigin.Position.X - i * pixelSize * 2.5f, BoxOrigin.Position.Y));
            }
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
                    Explosions.Clear();
                    //este bool lo lee el player para nullear la bomba
                    IsDestroyed = true;
                }
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