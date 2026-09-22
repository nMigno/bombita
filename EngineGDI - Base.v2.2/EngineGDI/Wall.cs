namespace EngineGDI
{
    public class Wall
    {
        public Transform transform;
        string id;
        string texture;
        public Wall(float initialX, float initialY, string path)
        {
            id = "Wall";
            texture = path;
            transform = new Transform();
            //escala y angulo
            transform.Scale.x = 2.5f;
            transform.Scale.y = 2.5f;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 16 * transform.Scale.x;
            transform.RealSize.y = 16 * transform.Scale.y;
            //coordenadas
            transform.Position.x = transform.RealSize.x * initialX;
            transform.Position.y = transform.RealSize.y * initialY;
            //que somos?
            transform.gameId = GameId.wall;
        }

        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
    public class BrickWall
    {
        public Transform transform;
        string id;
        string texture;
        public BrickWall(float initialX, float initialY, string path)
        {
            id = "BrickWall";
            texture = path;
            transform = new Transform();
            //escala y angulo
            transform.Scale.x = 2.5f;
            transform.Scale.y = 2.5f;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 16 * transform.Scale.x;
            transform.RealSize.y = 16 * transform.Scale.y;
            //coordenadas
            transform.Position.x = transform.RealSize.x * initialX;
            transform.Position.y = transform.RealSize.y * initialY;
            //que somos?
            transform.gameId = GameId.brickWall;
        }

        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}