using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using EngineGDI.DataFiles;

namespace EngineGDI
{


    static class Program
    {
        // mostrar debug
        public static bool showDebug = true;
        public static string currentMsg = "";

        static int SCREEN_WIDTH = 1600;
        static int SCREEN_HEIGHT = 900;

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

            pacman = new Player(5.0f, 5.0f);
            enemy = new Enemy(100.0f, 10.0f);
            collider = new Collider();
            background = new Background(0, 0, "Assets/Maps/Background.png");
            wall = new Wall(100, 100, "Assets/Sprites/Players/Bombita1/wallr.png");
            wall2 = new Wall(100, 200, "Assets/Sprites/Players/Bombita1/wallr.png");
            wall3 = new Wall(100, 300, "Assets/Sprites/Players/Bombita1/wallr.png");
            maze = new Maze("DataFiles/level1.json");


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
            collider.playerWallColision(pacman.transform, wall.transform);
            collider.playerWallColision(pacman.transform, wall2.transform);
            collider.playerWallColision(pacman.transform, wall3.transform);
            

            //isColliding = colider.IsBoxColliding(pacman.transform.Position, pacman.transform.RealSize, wall.transform.Position, wall.transform.RealSize);

            if (isColliding)
            {
                collider.Render();
                
                if (!wasColliding)
                {
                    audioManager.PlayPlayerDie();
                    wasColliding = true;
                }
            }
            else
            {
                wasColliding = false;
            }
        }       

        static void Render()
        {
            background.Render();
            pacman.Render();
            enemy.Render();
            wall.Render();
            wall2.Render();
            wall3.Render();
            maze.Render();
        }

    }
}
