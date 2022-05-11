using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class HealthPack
    {
        Texture2D healthPackTxr;
        public Rectangle healthRect;
        public bool pickUp;

        public HealthPack(Texture2D art, int x, int y)
        {
            healthPackTxr = art;
            healthRect = new Rectangle(x, y, healthPackTxr.Width, healthPackTxr.Height);
        }

        public void update()
        {

        }


        public void draw(SpriteBatch sb)
        {
            if(!pickUp)
            {
                sb.Draw(healthPackTxr, healthRect, Color.White);
            }

        }

    }
}
