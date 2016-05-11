using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PortalToMyWorld
{
    class Portal
    {
        Texture2D PortalTex;
        public Rectangle PortalRect;
        public Portal()
        {
            PortalRect = new Rectangle(0, 0, 0, 0);
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D PortalTex)
        {
            this.PortalTex = PortalTex;
            spriteBatch.Draw(PortalTex, PortalRect, Color.White);
        }
    }
}
