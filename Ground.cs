using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PortalToMyWorld
{
    class Ground
    {

        Texture2D GroundTex;
        public Rectangle GroundRect;

        public Ground(Texture2D GroundTex)
        {
            this.GroundTex = GroundTex;
            GroundRect = new Rectangle(0, 0, 0, 0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GroundTex, GroundRect, Color.White);
        }
    }
}
