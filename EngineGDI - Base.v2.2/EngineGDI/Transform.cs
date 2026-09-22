namespace EngineGDI
{
    public class Transform
    {
        public Vector2 Position;
        public Vector2 Scale;
        public Vector2 Offset;
        public Vector2 RealSize;
        public float Angle;
        public GameId gameId;
    }
    public enum GameId
    {
        player,
        enemy,
        wall,
        brickWall,
        bomb,
        explosion,
        speedPickUp,
        exit,
    }
}
