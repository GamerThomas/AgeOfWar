using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class BadGuyType1
    {
        Texture2D charTxr,testTxr;
        Rectangle cellPos,trigger1,trigger2;
        public Rectangle badRect;
        float frameRate, frameTime;
        bool flip;

        public BadGuyType1(Texture2D art, int x, int y,Texture2D art2)
        {
            charTxr = art;
            testTxr = art2;
            badRect = new Rectangle(x, y, 50, 50);
            cellPos = new Rectangle(0, 0, 50, 50);

            trigger1 = new Rectangle(10, 950, 20, 20);
            trigger2 = new Rectangle(500, 950, 20, 20);

            frameRate = 24;
            frameTime = 4;

        }


        public void update()
        {
            if(flip)
            {
                badRect.X -= 1;
            }
            if(!flip)
            {
                badRect.X += 1;
            }

            if(badRect.Intersects(trigger1)|| badRect.Intersects(trigger2))
            {
                if (flip)
                {
                    badRect.X += 1;
                    flip = false;
                }
                else
                {
                    badRect.X -= 1;
                    flip = true;
                }
            }




        }

        public void Draw(SpriteBatch sb,float deltaTime)
        {
            frameTime -= deltaTime * frameRate;
            if (frameTime < 0)
            {
                cellPos.X += 50;
                if (cellPos.X >= charTxr.Width)
                {
                    cellPos.X = 0;
                }
                frameTime = 4;
            }


            if(flip)
            {
                sb.Draw(charTxr, badRect, cellPos, Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,0);
            }
            else
            {
                sb.Draw(charTxr, badRect, cellPos, Color.White);
            }

            sb.Draw(testTxr, trigger1, Color.Red);
            sb.Draw(testTxr, trigger2, Color.Red);
        }






    }
}
