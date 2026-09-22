using System.Windows.Forms;
using static EngineGDI.GameManager;

namespace EngineGDI
{
    public class MainMenu
    {
        public int MenuIndex = 0;
        private string title = "Assets/Text/Title.png";
        private static string btnStart = "Assets/Text/BtnStart.png";
        private static string btnStartSelect = "Assets/Text/BtnStart_Selected.png";
        private static string btnExit = "Assets/Text/BtnExit.png";
        private static string btnExitSelect = "Assets/Text/BtnExit_Selected.png";
        private static float SCREEN_WIDTH;
        public MainMenu(float Width)
        {
            SCREEN_WIDTH = Width;
        }
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
                GameManager.Instance.BackgroundMenu.Render();

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
