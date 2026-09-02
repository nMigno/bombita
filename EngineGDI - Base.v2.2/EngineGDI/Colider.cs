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
        public void playerWallColision(Transform a, Transform b)
        {
            if (IsBoxColliding(a.Position, a.RealSize, b.Position, b.RealSize))
            {
                float aPOSx2 = a.Position.x + a.RealSize.x;
                float aPOSy2 = a.Position.y + a.RealSize.y;

                float bPOSx2 = b.Position.x + b.RealSize.x;
                float bPOSy2 = b.Position.y + b.RealSize.y;

                //sobreposicion horizontal
                float overlapLeft = aPOSx2 - b.Position.x;
                float overlapRight = bPOSx2 - a.Position.x;

                //sobrposicion vertical
                float overlapTop = aPOSy2 - b.Position.y;
                float overlapBottom = bPOSy2 - a.Position.y;

                float overlapX = Math.Min(overlapLeft, overlapRight);
                float overlapY = Math.Min(overlapTop, overlapBottom);

                //el valor mas chico determina el eje de la colision
                if (overlapX < overlapY)
                {
                    //la colision fue horizontal, revisamos el sentido 
                    if (a.Position.x < b.Position.x)
                        
                        a.Position.x -= overlapX; //colision A izquierda de B
                    else                        
                        a.Position.x += overlapX; //colision A derecha de B                                                             
                }
                else //la colison fue vertical, revisamos el sentido
                {                    
                    if (a.Position.y < b.Position.y)

                        a.Position.y -= overlapY; //colision A arriba de B                     
                    else
                        a.Position.y += overlapY;//colision A abajo de B
                }
            }
        }


        public void Render()
        {
            Engine.Draw("Bomberman.png", 100, 100, 2, 2, 0, 0.5f, 0.5f);
        }
    }

}
