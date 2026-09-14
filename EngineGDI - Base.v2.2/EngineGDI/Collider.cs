using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Collider
    {
        public bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            if (positionA.x + sizeA.x > positionB.x && // El borde DERECHO del PJ colisiona con el borde IZQUIERDO del objeto
                positionA.x < positionB.x + sizeB.x && // El borde IZQUIERDO del PJ colisiona con el borde DERECHO del objeto
                positionA.y + sizeA.y > positionB.y && // El borde SUPERIOR del PJ colisiona con el borde INFERIOR del objeto
                positionA.y < positionB.y + sizeB.y)   // El borde INFERIOR del PJ colisiona con el borde SUPERIOR del objeto
            {
                return true;
            }
            else
            {
                return false;
            }


            /*
            float distanceX = Math.Abs(sizeA.x - sizeB.x);
            float distanceY = Math.Abs(sizeA.y - sizeB.y);

            float sumHalfWidths = sizeA.x / 2 + sizeB.x / 2;
            float sumHalfHeights = sizeA.y / 2 + sizeB.y / 2;       

            return distanceX <= sumHalfWidths && distanceY <= sumHalfHeights;
            */
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
                    if (a.Position.x <= b.Position.x)
                    {
                        a.Position.x -= overlapX;
                    }
                    else if (a.Position.x > b.Position.x)
                    {
                        a.Position.x += overlapX;
                    }

                    if (overlapTop <= 10)
                    {
                        a.Position.y -= 2f;
                    }
                    else if (overlapBottom <= 10)
                    {
                        a.Position.y += 2f;
                    }            
                }
                else //la colison fue vertical, revisamos el sentido
                {
                    if (a.Position.y <= b.Position.y)
                    {
                        a.Position.y -= overlapY;
                    }
                    else if (a.Position.y > b.Position.y)
                    {
                        a.Position.y += overlapY;
                    }

                    if (overlapLeft <= 10)
                    {
                        a.Position.x -= 2f;
                    }
                    else if (overlapRight <= 10)
                    {
                        a.Position.x += 2f;
                    }
                }
            }
        }

        /*public bool CollidingWithBomb(Transform a, Transform b)
        {
            if (IsBoxColliding(a.Position, a.RealSize, b.Position, b.RealSize))
            {
                return true;
            }
        }*/

        /*
        public bool CanPlaceBomb(Vector2 bombPos, Vector2 bombSize, List<Wall> walls)
        {
            for (int i = 0;  i < walls.Count; i++)
            {
                if (IsBoxColliding(bombPos, bombSize, walls[i].transform.Position, walls[i].transform.RealSize))
                {
                    return false;
                }
            }

            return true;
        }
        */


        public void Render()
        {
            Engine.Draw("Bomberman.png", 100, 100, 2, 2, 0, 0.5f, 0.5f);
        }
    }

}
