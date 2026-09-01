using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Colider
    {
        //wall section
        public void FourDirPlayerPusher(ref Vector2 playerPosition, Vector2 playerSize, Vector2 wallPosition, Vector2 wallSize)
        {
            Vector2 playerPosition2;
            playerPosition2.x = playerPosition.x + playerSize.x;
            playerPosition2.y = playerPosition.y + playerSize.y;

            Vector2 wallPosition2;
            wallPosition2.x = wallPosition.x + wallSize.x;
            wallPosition2.y = wallPosition.y + wallSize.y;

            if (wallPosition.x < playerPosition2.x && playerPosition2.x < wallPosition2.x &&
                wallPosition.y < playerPosition2.y && playerPosition2.y < wallPosition2.y)
            {
                //wallPosition.y < playerPosition2.y && playerPosition2.y > wallPosition2.y )
                playerPosition.x = wallPosition.x - playerSize.x;
            }
            if (wallPosition.x + wallSize.x >= playerPosition.x)
            {
                //playerPosition.x = wallPosition.x + wallSize.x;
            }
            if (wallPosition.y <= playerPosition.y + playerSize.y)
            {
                //playerPosition.y = wallPosition.y - playerSize.y;
            }
            if (wallPosition.y + wallSize.y >= playerPosition.y)
            {
                //playerPosition.y = wallPosition.y + wallSize.y;
            }
        }
        public bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            float distanceX = Math.Abs(positionA.x - positionB.x);
            float distanceY = Math.Abs(positionA.y - positionB.y);

            float sumHalfWidths = sizeA.x / 2 + sizeB.x / 2;
            float sumHalfHeights = sizeA.y / 2 + sizeB.y / 2;       

            return distanceX <= sumHalfWidths && distanceY <= sumHalfHeights;
        }
        public void Render()
        {
            Engine.Draw("Bomberman.png", 100, 100, 2, 2, 0, 0.5f, 0.5f);
        }
    }

}
