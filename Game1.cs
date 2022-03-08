﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;

namespace AgeOfWar
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Cam cam;

        Player player;

        List<Wall> walls;
        List<Health> health;
        List<Platform> platforms;
        KeyboardState keyB1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 960;
        }

        protected override void Initialize()
        {
            walls = new List<Wall>();
            health = new List<Health>();
            platforms = new List<Platform>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Texture2D>("charLev1"), 100, 100, Content.Load<Texture2D>("testTxr"));

            cam = new Cam();

            playerHP();

            platformsLev1();

            wallsLev1();

        }

        protected override void Update(GameTime gameTime)
        {
            keyB1 = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.playerUpdate(keyB1, (float)gameTime.ElapsedGameTime.TotalSeconds);

            HpUpdate();

            playerCollision();

            touch();

            cam.follow(player);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: cam.transfom) ;

            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw(_spriteBatch);
            }


            player.playerDraw(_spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);

            for(int i = 0; i < health.Count;i++)
            {
                health[i].draw(_spriteBatch);
            }

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].draw(_spriteBatch);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }


        void touch()
        {
            int num = 0;
            for (int i = 0; i < platforms.Count; i++)
            {
                if(platforms[i].touch)
                {
                    num++;
                }
                    
            }
            if (num <= 0 &&  !player.jumping)
            {
                player.falling = true;
            }
        }

        void platformsLev1()
        {
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 100, 400,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 300, 400,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 400, 500,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 10, 600,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1000, 400,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 600, 400,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1000, 100,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 800, 250,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1600, 700,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1800, 400,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 0, 1000,2000));

        }


        void playerCollision()
        {

            for (int i = 0; i < platforms.Count; i++)
            {
                if (player.feetRect.Intersects(platforms[i].platRect))
                {
                    player.jumping = false;
                    player.falling = false;
                    player.playerPos.Y -= player.playerVel.Y;
                    player.playerVel.Y = 0;
                    player.playerPos.Y--;
                    platforms[i].touch = true;
                }
                else if (!player.playerRect.Intersects(platforms[i].platRect))
                {
                    platforms[i].touch = false;
                }


                if (player.HeadRect.Intersects(platforms[i].platRect))
                {
                    player.playerVel.Y = 0;
                    player.playerPos.Y++;
                }

                if (player.leftRect.Intersects(platforms[i].platRect))
                {
                    player.playerPos.X += 3;
                    player.playerPos.X++;
                }
                if (player.rightRect.Intersects(platforms[i].platRect))
                {
                    player.playerPos.X -= 3;
                    player.playerPos.X--;
                }

            }

            for(int i = 0; i < walls.Count; i++)
            {
                if(player.rightRect.Intersects(walls[i].wallRect))
                {
                    player.playerPos.X -= 3;
                }
                if (player.leftRect.Intersects(walls[i].wallRect))
                {
                    player.playerPos.X += 3;
                }
            }
        }

        void playerHP()
        {
            int pos = 0;
            for (int i = 0; i < player.health; i++)
            {
                health.Add(new Health(Content.Load<Texture2D>("Heart"), player.playerRect.X -150 + pos, player.playerRect.Y - 300));
                pos = pos + 35;
            }
        }

        void HpUpdate()
        {
            for(int i = 0; i < health.Count; i++)
            {
                health[i].update(player.playerRect, i);
            }
        }

        void wallsLev1()
        {
            walls.Add(new Wall(Content.Load<Texture2D>("testTxr"), 0, 0, 2000));
            walls.Add(new Wall(Content.Load<Texture2D>("testTxr"), 2000, 0, 2000));
        }

    }
}
