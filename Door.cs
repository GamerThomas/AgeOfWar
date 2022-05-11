using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfWar
{
    class Door
    {
        Texture2D doorTxr;
        public Rectangle doorRect;

        public Door(Texture2D art, int x, int y)
        {
            doorTxr = art;
            doorRect = new Rectangle(x, y, 50, 100);
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(doorTxr, doorRect, Color.Cyan);
        }


    }
}
