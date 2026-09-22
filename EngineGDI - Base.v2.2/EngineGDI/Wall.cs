namespace EngineGDI
{
    public class Wall
    {
        public Transform Transform;
        private string texture;
        public Wall(float initialX, float initialY, string path)
        {
            texture = path;
            Transform = new Transform();
            //escala y angulo
            Transform.Scale.X = 2.5f;
            Transform.Scale.Y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;
            Transform.RealSize.X = 16 * Transform.Scale.X;
            Transform.RealSize.Y = 16 * Transform.Scale.Y;
            //coordenadas
            Transform.Position.X = Transform.RealSize.X * initialX;
            Transform.Position.Y = Transform.RealSize.Y * initialY;
            //que somos?
            Transform.gameId = GameId.wall;
        }

        public void Render()
        {
            Engine.Draw(texture, Transform.Position.X, Transform.Position.Y, Transform.Scale.X, Transform.Scale.Y, Transform.Angle, Transform.Offset.X, Transform.Offset.Y);
        }
    }
    public class BrickWall
    {
        public Transform Transform;
        private string texture;
        public BrickWall(float initialX, float initialY, string path)
        {
            texture = path;
            Transform = new Transform();
            //escala y angulo
            Transform.Scale.X = 2.5f;
            Transform.Scale.Y = 2.5f;
            Transform.Angle = 0;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;
            Transform.RealSize.X = 16 * Transform.Scale.X;
            Transform.RealSize.Y = 16 * Transform.Scale.Y;
            //coordenadas
            Transform.Position.X = Transform.RealSize.X * initialX;
            Transform.Position.Y = Transform.RealSize.Y * initialY;
            //que somos?
            Transform.gameId = GameId.brickWall;
        }

        public void Render()
        {
            Engine.Draw(texture, Transform.Position.X, Transform.Position.Y, Transform.Scale.X, Transform.Scale.Y, Transform.Angle, Transform.Offset.X, Transform.Offset.Y);
        }
    }
}