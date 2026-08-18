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

        static int SCREEN_WIDTH = 1500;
        static int SCREEN_HEIGHT = 780;

        public static Player pacman;
        public static Player pacman2;
        public static Player pacman3;

        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;
        

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);

            // pos x , pos y , velx , vely

            pacman = new Player(5.0f, 5.0f);

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
        static void Render()
        {
            pacman.Render();
        }
        static void Update()
        {
            
        }
    }
}
