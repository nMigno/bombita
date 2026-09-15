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
        public Wall NewWall;
        public BrickWall NewBrickWall;
        //public DoorLevelExit NewDoor;
        public List<Wall> WallsInMaze { get; private set; } = new List<Wall>();
        public List<BrickWall> BrickWallsInMaze { get; private set; } = new List<BrickWall>();
        public Maze(string path)
        {
            LoadLevel(path);
        }
        void LoadLevel(string filePath)
        {
            LevelLoaderFromJson loader = new LevelLoaderFromJson();
            Level levelData = loader.ReadPathFromJson(filePath);
            WallsInMaze.Clear();

            foreach (WallData wall in levelData.WallsInFile)
            {
                NewWall = new Wall(wall.X, wall.Y, wall.ImagePath);
                WallsInMaze.Add(NewWall);
            }
            foreach (BrickWallData brick in levelData.BricksInFile)
            {
                NewBrickWall = new BrickWall(brick.X, brick.Y, brick.ImagePath);
                BrickWallsInMaze.Add(NewBrickWall);
            }
        }
        void Update()
        {
            for (int i = 0; i < WallsInMaze.Count; i++)
            {
                //WallsInMaze[i].Update();
            }
            for (int i = 0; i < BrickWallsInMaze.Count; i++)
            {
                //BrickWallsInMaze[i].Update();
            }
        }

        public void Render()
        {
            for (int i = 0; i < WallsInMaze.Count; i++)
            {
                WallsInMaze[i].Render();
            }
            for (int i = 0; i < BrickWallsInMaze.Count; i++)
            {
                BrickWallsInMaze[i].Render();
            }
        }
    }
}
