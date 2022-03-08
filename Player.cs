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
        Texture2D spriteSheet, testArt;
        float vel, frameTime, frameRate, maxFallSpeed, walkSpeed;
        public Rectangle playerRect, feetRect, HeadRect, attackRect,leftRect,rightRect;
        Rectangle cellPos, attackCellPos;
        public bool falling, jumping;
        bool flipped, attackDone;
        KeyboardState oldKeyB;

        public int health;

        public Vector2 playerPos, playerVel;


        enum AnimState
        {
            walkingRight, walkingLeft, Jumping, Falling, idle, attack
        }

        private AnimState m_currentState;





        public Player(Texture2D art, int x, int y, Texture2D test)
        {
            spriteSheet = art;

            testArt = test;

            playerRect = new Rectangle(x, y, 65, 80);

            feetRect = new Rectangle(x + 20, playerRect.Bottom - 10, 40, 9);

            HeadRect = new Rectangle(x + 20, playerRect.Top + 10, 40, 9);

            leftRect = new Rectangle(x, y +10, 10, playerRect.Height-30);
            rightRect = new Rectangle(playerRect.Right-10, y +10, 10, playerRect.Height-30);

            cellPos = new Rectangle(0, 0, 65, 80);

            attackCellPos = new Rectangle(0, 81, 65, 80);

            vel = 0.3f;

            frameTime = 4;
            frameRate = 24;

            m_currentState = AnimState.Falling;
            falling = true;

            playerPos = new Vector2(x, y);
            playerVel = new Vector2(0, 0);


            walkSpeed = 3f;
            maxFallSpeed = 6f;

            attackDone = true;
            flipped = false;

            health = 3;

        }

        public void playerUpdate(KeyboardState keyB, float deltaTime)
        {
            if (keyB.IsKeyDown(Keys.D) && attackDone)
            {
                playerPos.X += walkSpeed;
                m_currentState = AnimState.walkingRight;
                flipped = false;
            }


            if (keyB.IsKeyDown(Keys.A) && attackDone)
            {
                playerPos.X -= walkSpeed;
                m_currentState = AnimState.walkingLeft;
                flipped = true;
            }

            if (falling)
            {
                playerVel.Y += vel;
                m_currentState = AnimState.Falling;
            }

            if (keyB.IsKeyDown(Keys.Space) && !jumping)
            {
                jumping = true;
                playerVel.Y -= 10;
                falling = true;
            }

            if (!falling && keyB.IsKeyDown(Keys.F) && attackDone && !oldKeyB.IsKeyDown(Keys.F))
            {
                m_currentState = AnimState.attack;
                attackDone = false;
                attack();
            }


            if (playerVel.X >= maxFallSpeed)
            {
                playerVel.Y = maxFallSpeed;
            }

            updatePos();

            oldKeyB = keyB;
        }

        public void playerDraw(SpriteBatch sb, float deltaTime)
        {
            frameTime -= deltaTime * frameRate;
            if (m_currentState == AnimState.walkingRight || m_currentState == AnimState.walkingLeft)
            {
                if (frameTime < 0)
                {
                    cellPos.X += 65;
                    if (cellPos.X >= spriteSheet.Width)
                    {
                        cellPos.X = 0;
                    }
                    frameTime = 4;
                }
            }

            if (m_currentState == AnimState.attack)
            {
                if (frameTime < 0)
                {
                    attackCellPos.X += 65;
                    if (attackCellPos.X >= spriteSheet.Width - 65)
                    {
                        attackDone = true;
                        attackCellPos.X = 0;
                        m_currentState = AnimState.walkingRight;
                    }
                    frameTime = 4;
                }
            }


            if (m_currentState == AnimState.walkingLeft)
            {
                sb.Draw(spriteSheet, playerRect, cellPos, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }



            if (m_currentState == AnimState.walkingRight)
            {
                sb.Draw(spriteSheet, playerRect, cellPos, Color.White);
            }

            if (m_currentState == AnimState.attack && !flipped)
            {
                sb.Draw(spriteSheet, playerRect, attackCellPos, Color.White);
            }

            if (m_currentState == AnimState.attack && flipped)
            {
                sb.Draw(spriteSheet, playerRect, attackCellPos, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                
            }


            sb.Draw(testArt, attackRect, Color.Red);
            sb.Draw(testArt, feetRect, Color.Yellow);
            sb.Draw(testArt, HeadRect, Color.Blue);
            sb.Draw(testArt, leftRect, Color.Orange);
            sb.Draw(testArt, rightRect, Color.Green);
        }


        void attack()
        {
           if(flipped)
            {
                attackRect = new Rectangle(playerRect.Left-25, playerRect.Y, 50, 80);
            }
           else
            {
                attackRect = new Rectangle(playerRect.Right-25, playerRect.Y, 50, 80);
            }
        }

        void updatePos()
        {

            playerRect.X = (int)playerPos.X;
            playerRect.Y = (int)playerPos.Y;

            playerPos.X = playerPos.X + playerVel.X;
            playerPos.Y = playerPos.Y + playerVel.Y;


            feetRect.X = playerRect.X + 10;
            feetRect.Y = playerRect.Bottom - 10;

            HeadRect.X = playerRect.X + 10;
            HeadRect.Y = playerRect.Top + 10;

            leftRect.X = playerRect.X;
            leftRect.Y = playerRect.Y +20;

            rightRect.X = playerRect.Right -10;
            rightRect.Y = playerRect.Y +20;
        }

    }


    
}
