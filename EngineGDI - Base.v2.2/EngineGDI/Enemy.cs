using System;
using System.Collections.Generic;

namespace EngineGDI
{
    public class Enemy
    {
        private float vel;
        public Transform Transform;
        private List<Vector2> waypoints;
        private int currentWaypointIndex = 0;
        private float targetX;
        private float targetY;
        private float directionX;
        private float directionY;
        private float distance;
        private float normalizedDirX;
        private float normalizedDirY;

        public Enemy(float speed, List<Vector2> pathNodes)
        {
            vel = speed;
            waypoints = pathNodes;
            Transform = new Transform();
            Transform.Scale.X = 2.5f;
            Transform.Scale.Y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;
            Transform.RealSize.X = 16f * Transform.Scale.X;
            Transform.RealSize.Y = 16f * Transform.Scale.Y;
            Transform.Position.X = waypoints[0].X * Transform.RealSize.X;
            Transform.Position.Y = waypoints[0].Y * Transform.RealSize.Y;

            Transform.gameId = GameId.enemy;
        }

        public void Update(float deltaTime)
        {
            targetX = waypoints[currentWaypointIndex].X * Transform.RealSize.X;
            targetY = waypoints[currentWaypointIndex].Y * Transform.RealSize.Y;
            directionX = targetX - Transform.Position.X;
            directionY = targetY - Transform.Position.Y;
            distance = (float)Math.Sqrt((directionX * directionX) +
                (directionY * directionY));

            if (distance < 2.0f)
            {
                Transform.Position.X = targetX;
                Transform.Position.Y = targetY;

                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Count)
                {
                    currentWaypointIndex = 0;
                }
            }
            else
            {
                normalizedDirX = directionX / distance;
                normalizedDirY = directionY / distance;
                Transform.Position.X += normalizedDirX * vel * deltaTime;
                Transform.Position.Y += normalizedDirY * vel * deltaTime;
            }
        }
        public void Render()
        {
            Engine.Draw("Textures/Enemy.png", Transform.Position.X, Transform.Position.Y, Transform.Scale.X, Transform.Scale.Y, Transform.Angle, Transform.Offset.X, Transform.Offset.Y);
        }
    }
}
