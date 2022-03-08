using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class Platform
    {
        Texture2D platArt;
        public Rectangle platRect;
        public bool touch;

        public Platform(Texture2D art,int x,int y,int width)
        {
            platArt = art;
            platRect = new Rectangle(x, y, width, 10);
        }


        public void draw(SpriteBatch sb)
        {
            sb.Draw(platArt, platRect, Color.White);
        }
    
    }
}
