using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class AudioManager
    {
        private const string playerDie = "PlayerHit.wav";
        // Hacemos una lista laaaaarga de todos los paths de pistas de audio que usemos en el juego
        // Usando 'private const string'

        public void PlayPlayerDie()
        {
            PlaySound(playerDie, false);
        }

        //public void PlayBombitaMusic(bombitaMusic, true);

        private void PlaySound(string sound, bool isLoop)
        {
            if (!isLoop)
            {
                Engine.PlaySound(sound);
            }
            else
            {
                Engine.PlaySoundLoop(sound);
            }
        }
    }
}
