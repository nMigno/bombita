using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EngineGDI.GameManager;

namespace EngineGDI
{
    public class GameOver
    {
        public int MenuIndex = 0;
        static string btnPlayAgain = "Assets/Text/BtnPlayAgain.png";
        static string btnPlayAgainSelect = "Assets/Text/BtnPlayAgain_Selected.png";
        static string btnTryAgain = "Assets/Text/BtnTryAgain.png";
        static string btnTryAgainSelect = "Assets/Text/BtnTryAgain_Selected.png";
        static string btnExit = "Assets/Text/BtnExit.png";
        static string btnExitSelect = "Assets/Text/BtnExit_Selected.png";
        static string winText = "Assets/Text/WinText.png";
        static string deadText = "Assets/Text/DeadText.png";
        static float SCREEN_WIDTH;

        public Action OnRestartGame;

        public GameOver(float Width)
        {
            SCREEN_WIDTH = Width;
        }
        public void Update()
        {
            if (CurrentState == GameState.victory || CurrentState == GameState.defeat)
            {
                if (Engine.IsKeyPressed(System.Windows.Forms.Keys.W))
                {
                    MenuIndex = 0;
                }
                else if (Engine.IsKeyPressed(System.Windows.Forms.Keys.S))
                {
                    MenuIndex = 1;
                }

                if (Engine.IsKeyPressed(System.Windows.Forms.Keys.Enter))
                {
                    if (MenuIndex == 0)
                    {
                        OnRestartGame();
                        MenuIndex = 0;
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
            if (CurrentState == GameState.victory)
            {
                backgroundMenu.Render();
                Engine.Draw(winText, SCREEN_WIDTH / 2 - 236, 200, 1f, 1f);

                if (MenuIndex == 0)
                {
                    Engine.Draw(btnPlayAgainSelect, SCREEN_WIDTH / 2 - 80, 500, 1f, 1f);
                    Engine.Draw(btnExit, SCREEN_WIDTH / 2 - 80, 600, 1f, 1f);
                }
                else
                {
                    Engine.Draw(btnPlayAgain, SCREEN_WIDTH / 2 - 80, 500, 1f, 1f);
                    Engine.Draw(btnExitSelect, SCREEN_WIDTH / 2 - 80, 600, 1f, 1f);
                }
            }
            else if (CurrentState == GameState.defeat)
            {
                backgroundMenu.Render();
                Engine.Draw(deadText, SCREEN_WIDTH / 2 - 236, 200, 1f, 1f);
                
                if (MenuIndex == 0)
                {
                    Engine.Draw(btnTryAgainSelect, SCREEN_WIDTH / 2 - 80, 500, 1f, 1f);
                    Engine.Draw(btnExit, SCREEN_WIDTH / 2 - 80, 600, 1f, 1f);
                }
                else
                {
                    Engine.Draw(btnTryAgain, SCREEN_WIDTH / 2 - 80, 500, 1f, 1f);
                    Engine.Draw(btnExitSelect, SCREEN_WIDTH / 2 - 80, 600, 1f, 1f);
                }
            }
        }
    }
}
