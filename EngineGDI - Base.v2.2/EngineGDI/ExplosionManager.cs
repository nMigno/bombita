using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class ExplosionManager
    {
        public List<Explosion> ActiveExplosions { get; private set; }
        public ExplosionManager()
        {
            ActiveExplosions = new List<Explosion>();
        }

        public void AddExplosion(Explosion explosion)
        {
            ActiveExplosions.Add(explosion);
        }

        public void Update(float deltaTime)
        {
            for (int i = ActiveExplosions.Count - 1; i >= 0; i--)
            {
                ActiveExplosions[i].Update(deltaTime);

                if (ActiveExplosions[i].TimeOut)
                {
                    ActiveExplosions.RemoveAt(i);
                }
            }
        }

        public void Render()
        {
            for (int i = 0; i  < ActiveExplosions.Count; i++)
            {
                ActiveExplosions[i].Render();
            }
        }

        public void Clear()
        {
            ActiveExplosions.Clear();
        }
    }
}
