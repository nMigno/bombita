using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Colider
    {

        public bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            float distanceX = Math.Abs(positionA.x - positionB.x);
            float distanceY = Math.Abs(positionA.y - positionB.y);

            float sumHalfWidths = sizeA.x / 2 + sizeB.x / 2;
            float sumHalfHeights = sizeA.y / 2 + sizeB.y / 2;

            return distanceX <= sumHalfWidths && distanceY <= sumHalfHeights;
        }
        public void Render()
        {
            Engine.Draw("Bomberman.png", 100, 100, 2, 2, 0, 0.5f, 0.5f);
        }
    }

}
