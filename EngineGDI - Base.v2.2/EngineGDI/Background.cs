namespace EngineGDI
{
    public class Background
    {
        public Transform transform;
        private string id;
        private string texture;

        public Background(float initialx, float initialy, string path)
        {
            id = "Background";
            texture = path;
            transform = new Transform();
            transform.Position.x = initialx;
            transform.Position.y = initialy;
            transform.Scale.x = 3.24f;
            transform.Scale.y = 4.35f;
            transform.Angle = 0;
            transform.Offset.x = 0;
            transform.Offset.y = 0;
            transform.RealSize.x = 16;
            transform.RealSize.y = 16;
        }
        public void Render()
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Angle, transform.Offset.x, transform.Offset.y);
        }
    }
}
