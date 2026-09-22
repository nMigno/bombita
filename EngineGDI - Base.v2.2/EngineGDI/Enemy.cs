using System;
using System.Collections.Generic;

namespace EngineGDI
{
    public class Enemy
    {
        private float vel;
        public Transform transform;
        List<Vector2> waypoints;
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
            transform = new Transform();
            transform.Scale.x = 2.5f;
            transform.Scale.y = 2.5f;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 16f * transform.Scale.x;
            transform.RealSize.y = 16f * transform.Scale.y;
            transform.Position.x = waypoints[0].x * transform.RealSize.x;
            transform.Position.y = waypoints[0].y * transform.RealSize.y;

            transform.gameId = GameId.enemy;
        }

        public void Update(float deltaTime)
        {
            targetX = waypoints[currentWaypointIndex].x * transform.RealSize.x;
            targetY = waypoints[currentWaypointIndex].y * transform.RealSize.y;
            directionX = targetX - transform.Position.x;
            directionY = targetY - transform.Position.y;
            distance = (float)Math.Sqrt((directionX * directionX) +
                (directionY * directionY));

            if (distance < 2.0f)
            {
                transform.Position.x = targetX;
                transform.Position.y = targetY;

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
                transform.Position.x += normalizedDirX * vel * deltaTime;
                transform.Position.y += normalizedDirY * vel * deltaTime;
            }
        }
        public void Render()
        {
            Engine.Draw("Textures/Enemy.png", transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
