namespace EngineGDI
{
    public class Background
    {
        public Transform Transform;
        private string texture;

        public Background(float initialx, float initialy, string path)
        {
            texture = path;
            Transform = new Transform();
            Transform.Position.X = initialx;
            Transform.Position.Y = initialy;
            Transform.Scale.X = 3.24f;
            Transform.Scale.Y = 4.35f;
            Transform.Angle = 0;
            Transform.Offset.X = 0;
            Transform.Offset.Y = 0;
            Transform.RealSize.X = 16;
            Transform.RealSize.Y = 16;
        }
        public void Render()
        {
            Engine.Draw(texture, Transform.Position.X, Transform.Position.Y, Transform.Scale.X, Transform.Scale.Y, Transform.Angle, Transform.Offset.X, Transform.Offset.Y);
        }
    }
}
