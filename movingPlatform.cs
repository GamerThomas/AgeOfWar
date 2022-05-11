using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class movingPlatform
    {
        Texture2D platTxr;
        public Rectangle platRect;
        public bool touch, right;
        int max, min;

        public movingPlatform(Texture2D art, int x, int y, int maxX, int minX)
        {
            platTxr = art;

            platRect = new Rectangle(x, y, platTxr.Width, platTxr.Height);

            max = maxX;

            min = minX;
            
        }

        public void update()
        {
            if(right)
            {
                platRect.X += 2;
            }
            if(!right)
            {
                platRect.X -= 2;
            }

            if(platRect.X < min)
            {
                right = true;
            }

            if (platRect.X > max)
            {
                right = false;
            }
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(platTxr, platRect, Color.White);
        }

    }
}
