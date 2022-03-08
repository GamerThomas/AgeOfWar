using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class Wall
    {
        Texture2D wallArt;

        public Rectangle wallRect;


        public Wall(Texture2D art, int x, int y, int height)
        {
            wallArt = art;


            wallRect = new Rectangle(x, y, 10, height);


        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(wallArt, wallRect, Color.Brown);
        }

    }
}
