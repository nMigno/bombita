using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class Animation
    {
        private string id;
        private bool isLoopEnabled;
        public List<string> frames;
        private float speed = 0;
        private float currentAnimationTime = 0;
        private int currentFrameIndex = 0;
        private float width = 0f;
        private float height = 0f;
        public string Id => id;
        public string CurrentFrame => frames[currentFrameIndex];
        public float Width => width;
        public float Height => height;
        public Animation(string id, List<string> frames, float speed, bool isLoopEnabled, float w, float h)
        {
            this.id = id;
            this.frames = frames;
            this.speed = speed;
            this.isLoopEnabled = isLoopEnabled;
            this.width = w;
            this.height = h;
        }
        public void Reset()
        {
            this.currentFrameIndex = 0;
            this.currentAnimationTime = 0;
        }
        public void Update()
        {
            currentAnimationTime += Program.deltaTime;
            if (currentAnimationTime >= speed)
            {
                currentFrameIndex++;
                currentAnimationTime = 0;
                if (currentFrameIndex >= frames.Count)
                {
                    if (isLoopEnabled)
                    {
                        currentFrameIndex = 0;
                    }
                    else
                    {
                        currentFrameIndex = frames.Count - 1;
                    }
                }
            }
        }
    }
}
