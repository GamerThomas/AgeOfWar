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
        Texture2D charTxr,testTxr,deadTxr;
        Rectangle cellPos,trigger1,trigger2, deadRect,deadRectCell;
        public Rectangle badRect;
        float frameRate, frameTime;
        bool flip;
        public bool dead;
        

        public BadGuyType1(Texture2D art, int x, int y,Texture2D art2,int trigx1,int trigx2,Texture2D art3)
        {
            charTxr = art;
            testTxr = art2;
            deadTxr = art3;
            badRect = new Rectangle(x, y, 75, 90);
            cellPos = new Rectangle(0, 0, 75, 90);

            trigger1 = new Rectangle(trigx1, 550, 20, 20);
            trigger2 = new Rectangle(trigx2, 550, 20, 20);

            deadRect = new Rectangle(0, 0, 95, 60);
            deadRectCell = new Rectangle(0, 0, 95, 60);

            frameRate = 24;
            frameTime = 4;

        }


        public void update()
        {
            if(flip && !dead)
            {
                badRect.X -= 1;
            }
            if(!flip && !dead)
            {
                badRect.X += 1;
            }

            if(badRect.Intersects(trigger1)|| badRect.Intersects(trigger2) && !dead)
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




            deadRect.X = badRect.X;
            deadRect.Y = badRect.Y +33;

        }

        public void Draw(SpriteBatch sb,float deltaTime)
        {
            frameTime -= deltaTime * frameRate;
            if (frameTime < 0)
            {
                cellPos.X += 75;
                deadRectCell.X  += 95;
                if (cellPos.X >= charTxr.Width -75)
                {
                    cellPos.X = 0;
                }

                if(deadRectCell.X >= deadTxr.Width)
                {
                    deadRectCell.X = 0;
                }
                frameTime = 4;
            }


            if(flip && !dead)
            {
                sb.Draw(charTxr, badRect, cellPos, Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,0);
            }
            else if(!dead)
            {
                sb.Draw(charTxr, badRect, cellPos, Color.White);
            }

            if (dead && flip)
            {
                sb.Draw(deadTxr, deadRect, deadRectCell, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }

            if(dead && !flip)
            {
                sb.Draw(deadTxr, deadRect, deadRectCell, Color.White);
            }

            sb.Draw(testTxr, trigger1, Color.Red);
            sb.Draw(testTxr, trigger2, Color.Red);
        }






    }
}
