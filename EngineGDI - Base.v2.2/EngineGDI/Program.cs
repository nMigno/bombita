using System;
using System.Drawing;

namespace EngineGDI
{
    public static class Program
    {
        // mostrar debug
        public static bool ShowDebug = true;
        public static string CurrentMsg = "";

        public static int SCREEN_WIDTH = 1560;
        public static int SCREEN_HEIGHT = 880;
        public static float DeltaTime;
        private static DateTime lastFrameTime = DateTime.Now;

        [STAThread]
        private static void Main()
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
                if (ShowDebug)
                {
                    Engine.ClearDebug();
                    Engine.DebugLog(CurrentMsg);

                }
                Engine.Window.Invalidate();
                #endregion
            }
        }
        private static void calcDeltatime()
        {
            TimeSpan deltaSpan = DateTime.Now - lastFrameTime;
            DeltaTime = (float)deltaSpan.TotalSeconds;
            lastFrameTime = DateTime.Now;
        }
        private static void Input()
        {
            GameManager.Instance.Input();
        }
        private static void Update()
        {
            GameManager.Instance.Update(DeltaTime);
        }

        private static void Render()
        {
            GameManager.Instance.Render();
        }
    }
}