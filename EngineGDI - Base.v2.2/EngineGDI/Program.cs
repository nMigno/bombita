using System;
using System.Drawing;

namespace EngineGDI
{
    public static class Program
    {
        // mostrar debug
        public static bool showDebug = true;
        public static string currentMsg = "";

        public static int SCREEN_WIDTH = 1560;
        public static int SCREEN_HEIGHT = 880;
        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);
            
            GameManager.Initialize(SCREEN_WIDTH, SCREEN_HEIGHT);
            
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
            GameManager.Instance.Input();
        }
        static void Update()
        {
            GameManager.Instance.Update(deltaTime);
        }

        static void Render()
        {
            GameManager.Instance.Render();
        }
    }
}