using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EngineGDI.Program;

namespace EngineGDI
{
    public class MainMenu
    {
        public int MenuIndex = 0;
        static string title = "Assets/Text/Title.png";
        static string btnStart = "Assets/Text/BtnStart.png";
        static string btnStartSelect = "Assets/Text/BtnStart_Selected.png";
        static string btnExit = "Assets/Text/BtnExit.png";
        static string btnExitSelect = "Assets/Text/BtnExit_Selected.png";

        public void Update()
        {
            if (CurrentState == GameState.start)
            {
                if (Engine.IsKeyPressed(Keys.W))
                {
                    MenuIndex = 0;
                }
                else if (Engine.IsKeyPressed(Keys.S))
                {
                    MenuIndex = 1;
                }

                if (Engine.IsKeyPressed(Keys.Enter))
                {
                    if (MenuIndex == 0)
                    {
                        CurrentState = GameState.playing;
                    }
                    else
                    {
                        Engine.Window.Close();
                    }
                }
            }
        }
        public void Render()
        {
            if (CurrentState == GameState.start)
            {
                backgroundMenu.Render();

                Engine.Draw(title, SCREEN_WIDTH / 2 - 450, 200, 1f, 1f);

                if (MenuIndex == 0)
                {
                    Engine.Draw(btnStartSelect, SCREEN_WIDTH / 2 - 80, 500, 1f, 1f);
                    Engine.Draw(btnExit, SCREEN_WIDTH / 2 - 80, 600, 1f, 1f);
                }
                else
                {
                    Engine.Draw(btnStart, SCREEN_WIDTH / 2 - 80, 500, 1f, 1f);
                    Engine.Draw(btnExitSelect, SCREEN_WIDTH / 2 - 80, 600, 1f, 1f);
                }
            }
        }
    }
}
