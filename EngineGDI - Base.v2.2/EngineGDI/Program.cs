using EngineGDI.DataFiles;
using System;
using System.Drawing;
using System.Media;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static EngineGDI.Player;

namespace EngineGDI
{


    public static class Program
    {
        // mostrar debug
        public static bool showDebug = true;
        public static string currentMsg = "";

        static int SCREEN_WIDTH = 1560;
        static int SCREEN_HEIGHT = 840;

        public static Player pacman;
        public static Enemy enemy;
        public static Collider collider;
        public static Background background;
        public static AudioManager audioManager = new AudioManager();
        public static Maze maze;
        public static LevelExit exit;
        

        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;

        static bool isColliding = false;
        static bool wasColliding = false;

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);

            exit = new LevelExit(40.0f, 720.0f);
            pacman = new Player(41.0f, 16.0f);
            enemy = new Enemy(200.0f, 44.4f);
            collider = new Collider();
            background = new Background(0, 0, "Textures/bg-lv0.png");
            maze = new Maze("DataFiles/level0.json");

            //suscripcios CLASEDELEGADO
            pacman.OnLifeChanged += pacman.Die;
            pacman.OnLifeChanged += audioManager.PlayPlayerDie;
            collider.OnDestroyBrickWall += maze.RemoveBrickWall;

            while (Engine.IsWindowOpen)
            {
                #region Engine Window Control
                Engine.UpdateWindow();
                #endregion

                calcDeltatime();


                Input();
                Update();
                Render();

                #region Engine Window Control
                Engine.Clear(Color.Black);
                // mensajes de debug
                if (showDebug)
                {
                    Engine.ClearDebug();
                    Engine.DebugLog(currentMsg);

                }
                Engine.Window.Invalidate();
                #endregion
            }
            //desuscriptions CLASEDELEGADO
            pacman.OnLifeChanged -= pacman.Die;
            pacman.OnLifeChanged -= audioManager.PlayPlayerDie;
        }
        static void calcDeltatime()
        {
            TimeSpan deltaSpan = DateTime.Now - lastFrameTime;
            deltaTime = (float)deltaSpan.TotalSeconds;
            lastFrameTime = DateTime.Now;
        }
        static void Input()
        {
            pacman.Inputs();
        }
        static void Update()
        {
            pacman.Update(deltaTime);

            for (int i = 0; i < maze.WallsInMaze.Count; i++)
            {
                collider.IsTransformColliding(pacman.transform, maze.WallsInMaze[i].transform);
            }
            for (int i = 0; i < maze.BrickWallsInMaze.Count; i++)
            {
                collider.IsTransformColliding(pacman.transform, maze.BrickWallsInMaze[i].transform);
            }
            if (pacman.ActiveBomb != null)
            {
                for (int i = 0; i < pacman.ActiveBomb.explosions.Count; i++)
                {
                    for (int j = 0; j < maze.BrickWallsInMaze.Count; j++)
                    {
                        collider.IsTransformColliding(maze.BrickWallsInMaze[j].transform, pacman.ActiveBomb.explosions[i].transform);
                    }
                }
            }
        }       

        static void Render()
        {
            background.Render();
            exit.Render();
            pacman.Render();
            enemy.Render();
            maze.Render();
        }

    }
}
