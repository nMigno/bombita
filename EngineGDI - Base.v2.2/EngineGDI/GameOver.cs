using System;

namespace EngineGDI
{
    public class GameOver
    {
        public int MenuIndex = 0;
        private static string btnPlayAgain = "Assets/Text/BtnPlayAgain.png";
        private static string btnPlayAgainSelect = "Assets/Text/BtnPlayAgain_Selected.png";
        private static string btnTryAgain = "Assets/Text/BtnTryAgain.png";
        private static string btnTryAgainSelect = "Assets/Text/BtnTryAgain_Selected.png";
        private static string btnExit = "Assets/Text/BtnExit.png";
        private static string btnExitSelect = "Assets/Text/BtnExit_Selected.png";
        private static string winText = "Assets/Text/WinText.png";
        private static string deadText = "Assets/Text/DeadText.png";
        private static float SCREEN_WIDTH;

        public Action OnRestartGame;

        public GameOver(float Width)
        {
            SCREEN_WIDTH = Width;
        }
        public void Update()
        {
            if (GameManager.CurrentState == GameManager.GameState.victory || GameManager.CurrentState == GameManager.GameState.defeat)
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
            if (GameManager.CurrentState == GameManager.GameState.victory)
            {
                GameManager.Instance.BackgroundMenu.Render();
                Engine.Draw(winText, SCREEN_WIDTH / 2 - 180, 200, 1f, 1f);

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
            else if (GameManager.CurrentState == GameManager.GameState.defeat)
            {
                GameManager.Instance.BackgroundMenu.Render();
                Engine.Draw(deadText, SCREEN_WIDTH / 2 - 120, 200, 1f, 1f);
                
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
