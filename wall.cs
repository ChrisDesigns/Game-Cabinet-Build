using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PortalToMyWorld
{
    class wall
    {
        Texture2D wallTex;
        public Rectangle wallRect;

        public wall(Texture2D wallTex)
        {
            this.wallTex = wallTex;
            wallRect = new Rectangle(0, 0, 0, 0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(wallTex, wallRect, Color.White);
        }
    }
}
