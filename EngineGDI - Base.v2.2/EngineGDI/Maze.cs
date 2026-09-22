using EngineGDI.DataFiles;
using System.Collections.Generic;

namespace EngineGDI
{
    public class Maze
    {
        private string wallSprite = "Assets/Sprites/Players/Bombita1/wall.png";
        private string brickSprite = "Assets/Sprites/Players/Bombita1/wallr.png";
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
            BrickWallsInMaze.Clear();
            foreach (WallData wall in levelData.WallsInFile)
            {
                NewWall = new Wall(wall.X, wall.Y, wallSprite);
                WallsInMaze.Add(NewWall);
            }
            foreach (BrickWallData brick in levelData.BricksInFile)
            {
                NewBrickWall = new BrickWall(brick.X, brick.Y, brickSprite);
                BrickWallsInMaze.Add(NewBrickWall);
            }
        }
        //psoible forma de romber murosdeladrillso cuando la cosita que hace click clack click haga BOOOM
        public void RemoveBrickWall(Transform ayudaMeVanADestruir)
        {
            for (int i = 0; i < BrickWallsInMaze.Count; i++)
            {
                if (BrickWallsInMaze[i].Transform.Position.X == ayudaMeVanADestruir.Position.X &&
                    BrickWallsInMaze[i].Transform.Position.Y == ayudaMeVanADestruir.Position.Y)
                {
                    BrickWallsInMaze.RemoveAt(i);
                }
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
