using System;
using System.Drawing;
using System.Windows.Forms;


namespace EngineGDI
{
    public static class Program
    {
        // mostrar debug
        private static bool showDebug = true;
        private static string currentMsg = "";

        private static int SCREEN_WIDTH = 1024;
        private static int SCREEN_HEIGHT = 780;

        private static DateTime startTime;
        private static float deltaTime;
        private static float lastFrameTime;

        private static Player pacman;
        
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);

            startTime = DateTime.Now;

            pacman = new Player("test.png", 0, 0, 50, 1, 1);

            while (Engine.IsWindowOpen)
            {
                #region Engine Window Control
                Engine.UpdateWindow();
                #endregion

                var currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                deltaTime = currentTime - lastFrameTime;

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

        static void Input()
        {
            if (Engine.IsKeyDown(Keys.D))
            {
                pacman.posx += pacman.speed * deltaTime;
            }
        }

        static void Update()
        {

        }

        static void Render()
        {
                            Engine.Draw("test.png", 100, 50, 1, 1, 0, 0, 0);
        }
    }
}
