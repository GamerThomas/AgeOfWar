using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    class Player
    {
        Texture2D spriteSheet,testArt;
        float vel, frameTime, frameRate, jumpTime, maxFallSpeed, walkSpeed;
        public Rectangle playerRect,feetRect,HeadRect;
        Rectangle cellPos;
        public bool falling, jumpDone,jumping;
        public Vector2 playerPos,playerVel;


        enum AnimState
        {
            walkingRight,walkingLeft,Jumping,Falling,idle
        }

        private AnimState m_currentState;
        

        


        public Player(Texture2D art, int x,int y,Texture2D test) 
        {
            spriteSheet = art;

            testArt = test;

            playerRect = new Rectangle(x, y, 50, 80);

            feetRect = new Rectangle(x, playerRect.Bottom - 10, 50, 10);

            cellPos = new Rectangle(0, 0, 50, 80);

            vel = 0.3f;

            frameTime = 2;
            frameRate = 24;
            jumpTime = 2;

            m_currentState = AnimState.Falling;
            falling = true;

            playerPos = new Vector2(x, y);
            playerVel = new Vector2(0, 0);

            walkSpeed = 3f;
            maxFallSpeed = 6f;
        
        }

        public void playerUpdate(KeyboardState keyB, float deltaTime)
        {
            if (keyB.IsKeyDown(Keys.D))
            {
                playerPos.X += walkSpeed;
                m_currentState = AnimState.walkingRight;
            }


            if (keyB.IsKeyDown(Keys.A))
            {
                playerPos.X -= walkSpeed;
                m_currentState = AnimState.walkingLeft;

            }

            if (falling)
            {
                playerVel.Y += vel;
                m_currentState = AnimState.Falling;
            }

            if(keyB.IsKeyDown(Keys.Space) && !jumping)
            {
                jumping = true;
                playerVel.Y -= 10;
                falling = true;
            }


            if (jumpDone)
            {
                jumping = false;
                jumpDone = false;
            }

            if(playerVel.X >= maxFallSpeed)
            {
                playerVel.Y = maxFallSpeed;
            }



            playerRect.X = (int)playerPos.X;
            playerRect.Y = (int)playerPos.Y;

            playerPos.X = playerPos.X + playerVel.X;
            playerPos.Y = playerPos.Y + playerVel.Y;


            feetRect.X = playerRect.X;
            feetRect.Y = playerRect.Bottom - 10;
        }

        public void playerDraw(SpriteBatch sb, float deltaTime)
        {
            frameTime -= deltaTime * frameRate;
            if (m_currentState == AnimState.walkingRight || m_currentState == AnimState.walkingLeft)
            {
                cellPos.Y = 0;
                if (frameTime < 0)
                {
                    cellPos.X += 50;
                    if (cellPos.X >= spriteSheet.Width)
                    {
                        cellPos.X = 0;
                    }
                    frameTime = 2;
                }
            }

            
            if(m_currentState == AnimState.walkingLeft)
            {
                sb.Draw(spriteSheet, playerRect, cellPos, Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,0);
            }
            else sb.Draw(spriteSheet, playerRect, cellPos, Color.White);


            sb.Draw(testArt, feetRect, Color.Yellow);
        }



    }
}
