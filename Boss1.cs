using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfWar
{
    class Boss1
    {

        Texture2D charTxr, testTxr;
       
        Rectangle cellPos;
        public Rectangle bossRect, feetRect, headRect, leftRect, rightRect, detRect , trigger1, trigger2;
                
        private float bossSpeed;
        
        bool flip;        

        public Vector2 bossPos;
        private Vector2 bossVel;
        

        public Boss1(Texture2D art, int x, int y, float baseSpeed, Texture2D art2)
        {
            charTxr = art;
            testTxr = art2;
            bossRect = new Rectangle(x, y, 65, 90);
            cellPos = new Rectangle(0, 0, 65, 90);

            feetRect = new Rectangle(x, bossRect.Bottom - 10, 55, 10);
            headRect = new Rectangle(x, bossRect.Top - 10, 55, 10);

            leftRect = new Rectangle(x - 6, y, 10, bossRect.Height);
            rightRect = new Rectangle(bossRect.Right - 8, y, 10, bossRect.Height);

            trigger1 = new Rectangle(500, 550, 20, 20);
            trigger2 = new Rectangle(1500, 550, 20, 20);

            detRect = new Rectangle(x, y - 150, 765, bossRect.Height + 150);

            bossSpeed = baseSpeed;

            bossPos = new Vector2(x, y);
            bossVel = new Vector2(0, 0);

        }

        public void update()
        {
            if(flip)
            {
                bossRect.X -= 1;
            }
            if(!flip)
            {
                bossRect.X += 1;
            }

            if (bossRect.Intersects(trigger1) || bossRect.Intersects(trigger2))
            {
                if (flip)
                {
                    bossRect.X += 1;
                    flip = false;
                }
                else
                {
                    bossRect.X -= 1;
                    flip = true;
                }
            }
        }

        public void updateBossPos()
        {
            bossRect.X = (int)bossPos.X;
            bossRect.Y = (int)bossPos.Y;

            bossPos.X = bossRect.X;
            bossPos.Y = bossRect.Y;

            feetRect.X = bossRect.X + 5;
            feetRect.Y = bossRect.Bottom - 10;

            headRect.X = bossRect.X + 5;
            headRect.Y = bossRect.Top - 10;

            leftRect.X = bossRect.X;
            leftRect.Y = bossRect.Y;

            rightRect.X = bossRect.Right -8;
            rightRect.Y = bossRect.Y;

            detRect.X = bossRect.X - 350;
            detRect.Y = bossRect.Y - 150;
        }

        public void bossDet(Player target)
        {
            bossVel.X = target.playerRect.Center.X - this.detRect.Center.X;            
            
            bossVel.Normalize();

            bossVel *= bossSpeed;
            bossPos += bossVel;
        }

        public void Draw(SpriteBatch sb)
        {

            if(flip)
            {
                sb.Draw(charTxr, bossRect, cellPos, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                sb.Draw(charTxr, bossRect, cellPos, Color.White);
            }         

            //sb.Draw(testTxr, trigger1, Color.Red);
            //sb.Draw(testTxr, trigger2, Color.Red);  
            //sb.Draw(testTxr, leftRect, Color.Orange);
            //sb.Draw(testTxr, rightRect, Color.Green);
            //sb.Draw(testTxr, headRect, Color.Blue);
            //sb.Draw(testTxr, feetRect, Color.Yellow);
            //sb.Draw(testTxr, detRect, Color.White * 0.1f);
        }
    }
}
