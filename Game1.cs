using Microsoft.Xna.Framework;
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

        Player player;
        List<Platform> platforms;

        KeyboardState keyB1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            platforms = new List<Platform>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Texture2D>("CharSheet1"), 100, 100, Content.Load<Texture2D>("testTxr"));

            platforms.Add(new Platform(Content.Load<Texture2D>("testTxr"), 100, 400));
        }

        protected override void Update(GameTime gameTime)
        {
            keyB1 = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.playerUpdate(keyB1, (float)gameTime.ElapsedGameTime.TotalSeconds);


            if(player.feetRect.Intersects(platforms[0].platRect))
            {
                player.falling = false;
                player.jumping = false;
                player.playerPos.Y -= player.playerVel.Y;
                player.playerVel.Y = 0;
                player.playerPos.Y--;
            }
            else if(player.feetRect.X > platforms[0].platRect.Right || player.feetRect.Right < platforms[0].platRect.X)
            {
                player.falling = true;
            }







            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            player.playerDraw(_spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
            platforms[0].draw(_spriteBatch);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
