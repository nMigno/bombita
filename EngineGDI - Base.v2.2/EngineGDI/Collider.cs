using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EngineGDI.Program;

namespace EngineGDI
{
    public class Collider
    {
        public Action<Transform> OnDestroyBrickWall;

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
        }
        public void IsTransformColliding(Transform boxA, Transform boxB)
        {
            if (boxA.Position.x + boxA.RealSize.x > boxB.Position.x && // El borde DERECHO del PJ colisiona con el borde IZQUIERDO del objeto
                boxA.Position.x < boxB.Position.x + boxB.RealSize.x && // El borde IZQUIERDO del PJ colisiona con el borde DERECHO del objeto
                boxA.Position.y + boxA.RealSize.y > boxB.Position.y && // El borde SUPERIOR del PJ colisiona con el borde INFERIOR del objeto
                boxA.Position.y < boxB.Position.y + boxB.RealSize.y)   // El borde INFERIOR del PJ colisiona con el borde SUPERIOR del objeto
            {
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.wall) 
                {
                    playerPushOutColision(boxA, boxB);
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.brickWall)
                {
                    playerPushOutColision(boxA, boxB);
                }
                if (boxA.gameId == GameId.brickWall)
                {
                    if (boxB.gameId == GameId.explosion)
                    {
                        //llamar aca al delegado y mandarle la boxB
                        //el delegado es void y recibe un objeto de clase Transform
                        OnDestroyBrickWall(boxA);
                    }
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.exit)
                {
                    if (Program.exit.Opened)
                    {
                        // Calculamos el centro de ambos sprites para asegurarnos de que el
                        // player pise lo suficiente la salida para triggerear la victoria
                        float pacmanCenterX = pacman.transform.Position.x + (pacman.transform.RealSize.x / 2);
                        float pacmanCenterY = pacman.transform.Position.y + (pacman.transform.RealSize.y / 2);
                        float exitCenterX = exit.Transform.Position.x + (exit.Transform.RealSize.x / 2);
                        float exitCenterY = exit.Transform.Position.y + (exit.Transform.RealSize.y / 2);
                        float distanceX = pacmanCenterX - exitCenterX;
                        float distanceY = pacmanCenterY - exitCenterY;
                        float distance = (float)Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

                        if (distance < 8.0f) CurrentState = GameState.victory;
                    }
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.enemy)
                {
                    if (Program.pacman.alive) Program.pacman.Die();
                }
            }                        
        }
        //si primero se detecta colision Player, cualquierotroobjeto, lo empujamos
        public void playerPushOutColision(Transform a, Transform b)
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
}
