using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Web.Script.Serialization;

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


        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;
        

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);

            pacman = new Player(5.0f, 5.0f);
            enemy = new Enemy(100.0f, 10.0f);
            colider = new Colider();

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
                Engine.Clear(Color.Pink);
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
            pacman.Inputs(deltaTime);
        }
        static void Update()
        {

            if (colider.IsBoxColliding(pacman.transform.Position, pacman.transform.RealSize, enemy.transform.Position, enemy.transform.RealSize))
            {
                colider.Render();
            }


        }
       

        static void Render()
        {
            pacman.Render();
            enemy.Render();
        }

    }
}
