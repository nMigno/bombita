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
        public static Colider colider;
        public static Wall wall;
        public static Wall wall2;
        public static Wall wall3;
        public static LevelLoaderFromJson level;
        public static AudioManager audioManager = new AudioManager();


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
            colider = new Colider();
            wall = new Wall(100, 100, "Assets/Sprites/Players/Bombita1/wallr.png");
            wall2 = new Wall(100, 116, "Assets/Sprites/Players/Bombita1/wallr.png");
            wall3 = new Wall(100, 148, "Assets/Sprites/Players/Bombita1/wallr.png");
            level = new LevelLoaderFromJson();


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
            colider.playerWallColision(pacman.transform, wall.transform);
            colider.playerWallColision(pacman.transform, wall2.transform);
            colider.playerWallColision(pacman.transform, wall3.transform);
            level.ReadPathFromJson("DataFiles/EnemyPathing.json");

            //isColliding = colider.IsBoxColliding(pacman.transform.Position, pacman.transform.RealSize, wall.transform.Position, wall.transform.RealSize);

            if (isColliding)
            {
                colider.Render();
                
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
            pacman.Render();
            enemy.Render();
            wall.Render();
            wall2.Render();
            wall3.Render();
        }

    }
}
