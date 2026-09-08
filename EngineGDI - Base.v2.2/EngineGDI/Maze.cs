using EngineGDI.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Maze
    {
        public List<Wall> WallsInMaze { get; private set; } = new List<Wall>();
        public void LoadLevel(string filePath)
        {
            LevelLoaderFromJson loader = new LevelLoaderFromJson();
            Level levelData = loader.ReadPathFromJson(filePath);
            WallsInMaze.Clear();

            foreach (WallData wallData in levelData.WallsInFile)
            {
                Wall newWall = new Wall(wallData.X, wallData.Y, wallData.ImagePath);
                WallsInMaze.Add(newWall);
            }
        }

        public void Render()
        {
            for (int i = 0; i < WallsInMaze.Count; i++)
            {
                WallsInMaze[i].Render();
            }
        }
    }
}
