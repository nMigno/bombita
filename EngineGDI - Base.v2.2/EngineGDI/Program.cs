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
        public static Wall wall;
        public static Wall wall2;
        public static Wall wall3;
        public static AudioManager audioManager = new AudioManager();
        public static Maze maze;
        

        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;

        static bool isColliding = false;
        static bool wasColliding = false;

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);

            pacman = new Player(20.0f, 20.0f);
            enemy = new Enemy(200.0f, 44.4f);
            collider = new Collider();
            background = new Background(0, 0, "Textures/bg-lv0.png");
            //wall = new Wall(100, 100, "Assets/Sprites/Players/Bombita1/wallr.png");
            //wall2 = new Wall(100, 200, "Assets/Sprites/Players/Bombita1/wallr.png");
            //wall3 = new Wall(100, 300, "Assets/Sprites/Players/Bombita1/wallr.png");
            //maze = new Maze("DataFiles/level0.json");
            maze = new Maze("DataFiles/level0.json");

            //suscripcios CLASEDELEGADO
            pacman.OnLifeChanged += pacman.Die;
            pacman.OnLifeChanged += audioManager.PlayPlayerDie;

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
            //collider.playerWallColision(pacman.transform, wall.transform);
            //collider.playerWallColision(pacman.transform, wall2.transform);
            //collider.playerWallColision(pacman.transform, wall3.transform);
            //pacman.OnLifeChanged(1);

            for (int i = 0; i < maze.WallsInMaze.Count; i++)
            {
                collider.playerWallColision(pacman.transform, maze.WallsInMaze[i].transform);
            }

            //isColliding = collider.IsBoxColliding(pacman.transform.Position, pacman.transform.RealSize, wall.transform.Position, wall.transform.RealSize);           
        }       

        static void Render()
        {
            background.Render();
            pacman.Render();
            enemy.Render();
            //wall.Render();
            //wall2.Render();
            //wall3.Render();
            maze.Render();
        }

    }
}
