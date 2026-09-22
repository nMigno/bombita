namespace EngineGDI
{
    public class AudioManager
    {
        private const string playerDie = "SFX/PlayerHit.wav";
        // Hacemos una lista laaaaarga de todos los paths de pistas de audio que usemos en el juego
        // Usando 'private const string'
        // O bien usar un JSON 

        //metodo suscrito en program CLASEDELEGADO
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
