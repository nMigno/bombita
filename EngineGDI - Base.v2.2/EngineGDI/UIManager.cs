using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class UIManager
    {
        string upTwo = "Assets/Text/UP_2.png";
        string upOne = "Assets/Text/UP_1.png";
        string upZero = "Assets/Text/UP_0.png";

        float uiPosX = 10f;
        float uiPosY = 840f;

        public void Render(int playerLives)
        {
            if (CurrentState == GameState.playing)
            {
                if (playerLives == 2)
                {
                    Engine.Draw(upTwo, uiPosX, uiPosY, 1f, 1f);
                }
                else if (playerLives == 1)
                {
                    Engine.Draw(upOne, uiPosX, uiPosY, 1f, 1f);
                }
                else if (playerLives == 0)
                {
                    Engine.Draw(upZero, uiPosX, uiPosY, 1f, 1f);
                }
            }
        }
    }
}
