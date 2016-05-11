using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PortalToMyWorld
{
    class Spike
    {

        Texture2D SpikeTex;
        public Rectangle SpikeRect;

        public Spike(Texture2D SpikeTex)
        {
            this.SpikeTex = SpikeTex;
            SpikeRect = new Rectangle(0, 0, 0, 0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpikeTex, SpikeRect, Color.White);
        }
    }
}
