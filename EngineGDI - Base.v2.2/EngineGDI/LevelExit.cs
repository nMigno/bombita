namespace EngineGDI
{
    public class LevelExit
    {
        private string texture;
        private Transform transform;
        private bool opened = false;
        private int pixelSize = 16;
        public bool Opened => opened; // Si bien no se usa ahora, lo dejamos seteado para cuando hagamos la lógica 
                                      // de matar a todos los enemigos para abrir la puerta de salida
        public Transform Transform => transform;

      
        public LevelExit(float posX, float posY)
        {
            texture = "Assets/Sprites/Icons/Exit.png";
            transform = new Transform();
            Transform.Position.X = posX;
            Transform.Position.Y = posY;
            Transform.Scale.X = 2.5f;
            Transform.Scale.Y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;
            Transform.RealSize.X = pixelSize * Transform.Scale.X;
            Transform.RealSize.Y = pixelSize * Transform.Scale.Y;
            Transform.gameId = GameId.exit;
        }

        public void Render()
        {
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Angle, transform.Offset.X, transform.Offset.Y);
        }

        public void OpenExit()
        {
            opened = true;
        }
    }
}
