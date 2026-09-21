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

        public void Render()
        {
            if (Program.CurrentState == Program.GameState.playing)
            {
                if (Program.pacman.Lives == 2)
                {
                    Engine.Draw(upTwo, uiPosX, uiPosY, 1f, 1f);
                }
                else if (Program.pacman.Lives == 1)
                {
                    Engine.Draw(upOne, uiPosX, uiPosY, 1f, 1f);
                }
                else if (Program.pacman.Lives == 0)
                {
                    Engine.Draw(upZero, uiPosX, uiPosY, 1f, 1f);
                }
            }
        }
    }
}
