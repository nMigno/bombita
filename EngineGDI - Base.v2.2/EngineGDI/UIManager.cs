namespace EngineGDI
{
    public class UIManager
    {
        private string upTwo = "Assets/Text/UP_2.png";
        private string upOne = "Assets/Text/UP_1.png";
        private string upZero = "Assets/Text/UP_0.png";

        private float uiPosX = 10f;
        private float uiPosY = 840f;

        public void Render()
        {
            if (GameManager.CurrentState == GameManager.GameState.playing)
            {
                if (GameManager.Instance.BombitaMan.Lives == 2)
                {
                    Engine.Draw(upTwo, uiPosX, uiPosY, 1f, 1f);
                }
                else if (GameManager.Instance.BombitaMan.Lives == 1)
                {
                    Engine.Draw(upOne, uiPosX, uiPosY, 1f, 1f);
                }
                else if (GameManager.Instance.BombitaMan.Lives == 0)
                {
                    Engine.Draw(upZero, uiPosX, uiPosY, 1f, 1f);
                }
            }
        }
    }
}
