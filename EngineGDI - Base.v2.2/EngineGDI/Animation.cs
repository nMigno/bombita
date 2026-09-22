using System.Collections.Generic;

namespace EngineGDI
{
    public class Animation
    {
        private string id;
        private bool isLoopEnabled;
        public List<string> Frames;
        private float speed = 0;
        private float currentAnimationTime = 0;
        private int currentFrameIndex = 0;
        private float width = 0f;
        private float height = 0f;
        public string Id => id;
        public string CurrentFrame => Frames[currentFrameIndex];
        public float Width => width;
        public float Height => height;
        public bool IsFinished { get; private set; } = false;
        public Animation(string id, List<string> frames, float speed, bool isLoopEnabled, float w, float h)
        {
            this.id = id;
            this.Frames = frames;
            this.speed = speed;
            this.isLoopEnabled = isLoopEnabled;
            this.width = w;
            this.height = h;
        }
        public void Reset()
        {
            this.currentFrameIndex = 0;
            this.currentAnimationTime = 0;
            this.IsFinished = false;
        }
        public void Update()
        {
            currentAnimationTime += Program.DeltaTime;
            if (currentAnimationTime >= speed)
            {
                currentFrameIndex++;
                currentAnimationTime = 0;
                if (currentFrameIndex >= Frames.Count)
                {
                    if (isLoopEnabled)
                    {
                        currentFrameIndex = 0;
                    }
                    else
                    {
                        currentFrameIndex = Frames.Count - 1;
                        IsFinished = true;
                    }
                }
            }
        }
    }
}
