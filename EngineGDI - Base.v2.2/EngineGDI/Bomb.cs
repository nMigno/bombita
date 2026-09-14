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
            Transform.RealSize.x = 16 * Transform.Scale.x;
            Transform.RealSize.y = 16 * Transform.Scale.y;

            timer = 3.0f;
            isActive = true;
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
        }

        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, 
                transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
