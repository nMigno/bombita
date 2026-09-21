using EngineGDI.DataFiles;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
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

        public static int SCREEN_WIDTH = 1560;
        public static int SCREEN_HEIGHT = 880;

        public static GameManager gameloop;
        public static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;

        [STAThread]
        static void Main()
        {
            Engine.Initialize("IERVA ENGINE", SCREEN_WIDTH, SCREEN_HEIGHT, false);
            
            gameloop = new GameManager(SCREEN_WIDTH, SCREEN_HEIGHT);
            
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
            gameloop.Input();
        }
        static void Update()
        {
            gameloop.Update(deltaTime);
        }

        static void Render()
        {
            gameloop.Render();
        }
    }
}