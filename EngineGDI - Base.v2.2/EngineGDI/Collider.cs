using System;

namespace EngineGDI
{
    public class Collider
    {
        public Action<Transform> OnDestroyBrickWall;
        public Action OnPlayerColisionWithSomethingThatKillsIt;
        public Action<Transform> OnDestroyEnemy;
        public Action OnPlayerExitColision;
        public bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            if (positionA.X + sizeA.X > positionB.X && // El borde DERECHO del PJ colisiona con el borde IZQUIERDO del objeto
                positionA.X < positionB.X + sizeB.X && // El borde IZQUIERDO del PJ colisiona con el borde DERECHO del objeto
                positionA.Y + sizeA.Y > positionB.Y && // El borde SUPERIOR del PJ colisiona con el borde INFERIOR del objeto
                positionA.Y < positionB.Y + sizeB.Y)   // El borde INFERIOR del PJ colisiona con el borde SUPERIOR del objeto
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
            if (boxA.Position.X + boxA.RealSize.X > boxB.Position.X && // El borde DERECHO del PJ colisiona con el borde IZQUIERDO del objeto
                boxA.Position.X < boxB.Position.X + boxB.RealSize.X && // El borde IZQUIERDO del PJ colisiona con el borde DERECHO del objeto
                boxA.Position.Y + boxA.RealSize.Y > boxB.Position.Y && // El borde SUPERIOR del PJ colisiona con el borde INFERIOR del objeto
                boxA.Position.Y < boxB.Position.Y + boxB.RealSize.Y)   // El borde INFERIOR del PJ colisiona con el borde SUPERIOR del objeto
            {
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.wall)
                {
                    PlayerPushOutColision(boxA, boxB);
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.brickWall)
                {
                   PlayerPushOutColision(boxA, boxB);
                }
                if (boxA.gameId == GameId.brickWall && boxB.gameId == GameId.explosion)
                {
                    //llamar aca al delegado y mandarle la boxA que son las coordenadas
                    //el delegado es void y recibe un objeto de clase Transform
                    if (boxA != null) OnDestroyBrickWall(boxA);

                }
                if (boxA.gameId == GameId.enemy && boxB.gameId == GameId.explosion)
                {
                    if (boxB != null) OnDestroyEnemy(boxA);
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.exit)
                {
                    // Calculamos el centro de ambos sprites para asegurarnos de que el
                    // player pise lo suficiente la salida para triggerear la victoria
                    float pacmanCenterX = boxA.Position.X + (boxA.RealSize.X / 2);
                    float pacmanCenterY = boxA.Position.Y + (boxA.RealSize.Y / 2);
                    float exitCenterX = boxB.Position.X + (boxB.RealSize.X / 2);
                    float exitCenterY = boxB.Position.Y + (boxB.RealSize.Y / 2);
                    float distanceX = pacmanCenterX - exitCenterX;
                    float distanceY = pacmanCenterY - exitCenterY;
                    float distance = (float)Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

                    if (distance < 8.0f) OnPlayerExitColision();
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.enemy)
                {
                    OnPlayerColisionWithSomethingThatKillsIt();
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.explosion)
                {
                    OnPlayerColisionWithSomethingThatKillsIt();
                }
                if (boxA.gameId == GameId.player && boxB.gameId == GameId.bomb)
                {
                    PlayerPushOutColision(boxA, boxB);
                }
            }
        }
        //si primero se detecta colision Player, cualquierotroobjeto, lo empujamos
        public void PlayerPushOutColision(Transform a, Transform b)
        {
            float aPOSx2 = a.Position.X + a.RealSize.X;
            float aPOSy2 = a.Position.Y + a.RealSize.Y;

            float bPOSx2 = b.Position.X + b.RealSize.X;
            float bPOSy2 = b.Position.Y + b.RealSize.Y;

            //sobreposicion horizontal
            float overlapLeft = aPOSx2 - b.Position.X;
            float overlapRight = bPOSx2 - a.Position.X;

            //sobrposicion vertical
            float overlapTop = aPOSy2 - b.Position.Y;
            float overlapBottom = bPOSy2 - a.Position.Y;

            float overlapX = Math.Min(overlapLeft, overlapRight);
            float overlapY = Math.Min(overlapTop, overlapBottom);

            //el valor mas chico determina el eje de la colision
            if (overlapX < overlapY)
            {
                if (a.Position.X <= b.Position.X)
                {
                    a.Position.X -= overlapX;
                }
                else if (a.Position.X > b.Position.X)
                {
                    a.Position.X += overlapX;
                }

                if (overlapTop <= 10)
                {
                    a.Position.Y -= 2f;
                }
                else if (overlapBottom <= 10)
                {
                    a.Position.Y += 2f;
                }
            }
            else //la colison fue vertical, revisamos el sentido
            {
                if (a.Position.Y <= b.Position.Y)
                {
                    a.Position.Y -= overlapY;
                }
                else if (a.Position.Y > b.Position.Y)
                {
                    a.Position.Y += overlapY;
                }

                if (overlapLeft <= 10)
                {
                    a.Position.X -= 2f;
                }
                else if (overlapRight <= 10)
                {
                    a.Position.X += 2f;
                }
            }
        }
    }
}