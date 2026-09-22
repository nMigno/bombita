using EngineGDI.DataFiles;
using System;
using System.Collections.Generic;
using System.IO;

namespace EngineGDI
{
    public class EnemyManager
    {
        public PathData Pathing;
        public string EnemyPath;
        public float EnemySpeed = 100.0f;
        public List<Vector2> Routes;
        public Enemy NewEnemy;
        public List<Enemy> Enemies = new List<Enemy>();
        public EnemyManager()
        {
            EnemyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "DataFiles", "EnemyPathing.json");
            // Line below generates random positioning of enemies. We
            // deactivate ir for avoiding constant random enemy positioning
            // every time we open the game
            //PathGenerator.GeneratePaths(levelPath, EnemyPath);

            Pathing = PositionData.ReadPathFromJson(EnemyPath);
            LoadEnemies();

        }
        public void LoadEnemies()
        {
            Enemies.Clear();

            foreach (var route in Pathing.EnemyRoutes)
            {
                Routes = new List<Vector2>();

                foreach (var pathing in route.Path)
                {
                    Routes.Add(new Vector2 { X = pathing.X, Y = pathing.Y });
                }

                NewEnemy = new Enemy(EnemySpeed, Routes);
                Enemies.Add(NewEnemy);
            }
        }
        public void RemoveEnemy(Transform ayudaMeVanADestruir)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].Transform.Position.X == ayudaMeVanADestruir.Position.X &&
                    Enemies[i].Transform.Position.Y == ayudaMeVanADestruir.Position.Y)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }
        public void Render()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Render();
            }
        }
        public void Update(float deltaTime)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update(deltaTime);
            }
        }

    }
}



