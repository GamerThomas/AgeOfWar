using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class Document
    {
        public Rectangle docRect;
        Rectangle hudRect;
        Texture2D docTxr;
        public bool found;

        int docNum;

        public Document(Texture2D art, int x ,int y, int num)
        {
            docTxr = art;

            docRect = new Rectangle(x, y, docTxr.Width, docTxr.Height);

            docNum = num;

            hudRect = new Rectangle(0, 0, 50, 50);
        }

        public void Update(Rectangle playerRect)
        {
            if(docNum ==1)
            {
                hudRect.X = playerRect.X + 1700;
                hudRect.Y = playerRect.Y - 300;
            }

            if(docNum == 2)
            {
                hudRect.X = playerRect.X + 1640;
                hudRect.Y = playerRect.Y - 300;
            }

            if(docNum == 3)
            {
                hudRect.X = playerRect.X + 1580;
                hudRect.Y = playerRect.Y - 300;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            if (!found)
            {
                sb.Draw(docTxr, docRect, Color.White);
                sb.Draw(docTxr, hudRect,new Color(10,10,10,100)) ;
            }
            if(found)
            {
                sb.Draw(docTxr, hudRect, Color.White);
            }

        }

    }
}
