using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class Health
    {
        Texture2D healthArt;
        Rectangle hpRect;

        public Health(Texture2D art, int x, int y)
        {
            healthArt = art;
      
            hpRect = new Rectangle(x, -340, 32, 32);
        }

        public void update(Rectangle playerRect , int num)
        {
            if(num == 0)
            {
                hpRect.X = playerRect.X - 800;
            }

            if (num == 1)
            {
                hpRect.X = playerRect.X - 770;
            }


            if (num == 2)
            {
                hpRect.X = playerRect.X -740;
            }



        }


        public void draw(SpriteBatch sb)
        {
            sb.Draw(healthArt, hpRect, Color.White);
        }





    }
}
