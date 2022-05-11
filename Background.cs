using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class Background
    {
        Texture2D backgroundTxr;
        Rectangle backgroundRect;

        public Background(Texture2D art, int x, int y)
        {
            backgroundTxr = art;
            backgroundRect = new Rectangle(x, y, 1920, 1080);
        }

        public void update(Rectangle playerRect)
        {
            backgroundRect.X = playerRect.X - 828;
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(backgroundTxr, backgroundRect, Color.White);
        }



    }
}
