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
        private Cam cam;

        Player player;

        Boss1 bossLevel1;

        Background background; 

        List<movingPlatform> movingPlatforms;
        List<BadGuyType1> badGuys;
        List<Wall> walls;
        List<Health> health;
        List<Platform> platforms;
        List<Document> documents;
        List<Door> doors;
        List<HealthPack> healthPacks;


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
            healthPacks = new List<HealthPack>();
            movingPlatforms = new List<movingPlatform>();
            documents = new List<Document>();
            badGuys = new List<BadGuyType1>();
            walls = new List<Wall>();
            health = new List<Health>();
            platforms = new List<Platform>();
            doors = new List<Door>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Texture2D>("charLev1"), 100, 100, Content.Load<Texture2D>("testTxr"));

            cam = new Cam();

            background = new Background(Content.Load<Texture2D>("background1"), -728, -400);

            badGuys.Add(new BadGuyType1(Content.Load<Texture2D>("frenchBadGuy"),100, 510, Content.Load<Texture2D>("testTxr"),10,500, Content.Load<Texture2D>("dead")));

            bossLevel1 = new Boss1(Content.Load<Texture2D>("jacobiteBoss-1.png"), 1250, 508, 1.5f, Content.Load<Texture2D>("testTxr"));

            movingPlatforms.Add(new movingPlatform(Content.Load<Texture2D>("logPlat"), 1000, 100, 1600, 800));

            doors.Add(new Door(Content.Load<Texture2D>("testTxr"), 2000, 500));

            healthPacksLev1();

            playerHP();

            platformsLev1();

            wallsLev1();

            docLev1();

        }

        protected override void Update(GameTime gameTime)
        {
            keyB1 = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.playerUpdate(keyB1, (float)gameTime.ElapsedGameTime.TotalSeconds);

            bossLevel1.update();
            bossLevel1.updateBossPos();

            if (bossLevel1.detRect.Intersects(player.playerRect) && player.playerPos.X >= bossLevel1.trigger1.X && player.rightRect.X <= bossLevel1.trigger2.X)
            {
                bossLevel1.bossDet(player);
            }

            for (int i =0; i < badGuys.Count;i++)
            {
                badGuys[i].update();
            }

            for (int i = 0; i < documents.Count; i++)
            {
                documents[i].Update(player.playerRect);
            }

            for (int i = 0; i < movingPlatforms.Count; i++)
            {
                movingPlatforms[i].update();
            }

            background.update(player.playerRect);

            healthPickUp();

            hitPlayer();

            HpUpdate();

            playerCollision();

            touch();

            docCollision();

            openDoor();

            cam.follow(player);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: cam.transfom) ;

            background.draw(_spriteBatch);

            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw(_spriteBatch);
            }


            player.playerDraw(_spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);

            bossLevel1.Draw(_spriteBatch);

            for (int i = 0; i < healthPacks.Count; i++)
            {
                healthPacks[i].draw(_spriteBatch);
            }

            for (int i = 0; i < health.Count;i++)
            {
                health[i].draw(_spriteBatch);
            }

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].draw(_spriteBatch);
            }

            for (int i = 0; i < badGuys.Count; i++)
            {
                badGuys[i].Draw(_spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            for (int i = 0; i < documents.Count; i++)
            {
                documents[i].Draw(_spriteBatch);
            }

            for (int i = 0; i < movingPlatforms.Count; i++)
            {
                movingPlatforms[i].draw(_spriteBatch);
            }

            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].draw(_spriteBatch);
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

            for (int i = 0; i < movingPlatforms.Count; i++)
            {
                if (movingPlatforms[i].touch)
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
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 100, 0,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 300, 0,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 400, 100,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 10, 200,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1000, 0,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 600, 0,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1000, -200,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 800, -150,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1600, 300,150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 1800, 0,150));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 0, 600,2000));

            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 200, 450, 150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 300, 350, 150));
            platforms.Add(new Platform(Content.Load<Texture2D>("logPlat"), 400, 250, 150));


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



            for (int i = 0; i < movingPlatforms.Count; i++)
            {
                if (player.feetRect.Intersects(movingPlatforms[i].platRect))
                {
                    player.jumping = false;
                    player.falling = false;
                    player.playerPos.Y -= player.playerVel.Y;
                    player.playerVel.Y = 0;
                    player.playerPos.Y--;

                    movingPlatforms[i].touch = true;
                }
                else if (!player.playerRect.Intersects(movingPlatforms[i].platRect))
                {
                    movingPlatforms[i].touch = false;
                }


                if (player.playerRect.Intersects(movingPlatforms[i].platRect))
                {
                    if (movingPlatforms[i].right) player.playerVel.X = 2;
                    else player.playerVel.X = -2;
                }




                if (player.HeadRect.Intersects(movingPlatforms[i].platRect))
                {
                    player.playerVel.Y = 0;
                    player.playerPos.Y++;
                }

                if (player.leftRect.Intersects(movingPlatforms[i].platRect))
                {
                    player.playerPos.X += 3;
                    player.playerPos.X++;
                }
                if (player.rightRect.Intersects(movingPlatforms[i].platRect))
                {
                    player.playerPos.X -= 3;
                    player.playerPos.X--;
                }

            }

            for (int i = 0; i < walls.Count; i++)
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

            for (int i = 0; i < doors.Count; i++)
            {
                if (player.rightRect.Intersects(doors[i].doorRect))
                {
                    player.playerPos.X -= 3;
                }
                if (player.leftRect.Intersects(doors[i].doorRect))
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
            walls.Add(new Wall(Content.Load<Texture2D>("testTxr"), 0, -600, 2000));
            walls.Add(new Wall(Content.Load<Texture2D>("testTxr"), 2000, -500, 1000));
        }

        void hitPlayer()
        {
            for(int i = 0; i < badGuys.Count; i++)
            {
                if(badGuys[i].badRect.Intersects(player.rightRect) && !player.hit && player.health >0 && !badGuys[i].dead)
                {
                    player.hit = true;
                    health.RemoveAt(player.health-1);
                    player.health--;
                    player.playerVel.X -= 10;
                }
                if (badGuys[i].badRect.Intersects(player.leftRect) && !player.hit && player.health > 0 && !badGuys[i].dead)
                {
                    player.hit = true;
                    health.RemoveAt(player.health- 1);
                    player.health--;
                    player.playerVel.X += 10;
                }
                if (badGuys[i].badRect.Intersects(player.feetRect) && player.health > 0 && !badGuys[i].dead)
                {
                    player.playerVel.Y -= 5;
                }
                if(badGuys[i].badRect.Intersects(player.attackRect) && player.playerHit)
                {
                    badGuys[i].dead = true;
                }
            }
        }

        void docLev1()
        {
            documents.Add(new Document(Content.Load<Texture2D>("file"), 350, -60,1));
            documents.Add(new Document(Content.Load<Texture2D>("file"), 1050, -260,2));
            documents.Add(new Document(Content.Load<Texture2D>("file"), 1650, 240,3));
        }

        void docCollision()
        {
            for (int i = 0; i < documents.Count; i++)
            {
                if (player.playerRect.Intersects(documents[i].docRect))
                {
                    documents[i].found = true;
                }
            }
            
        }


        void openDoor()
        {
            int num = 0;
            for(int i = 0; i < documents.Count; i++)
            {
                if(documents[i].found == true)
                {
                    num++;
                }
            }

            if(num == documents.Count)
            {
                doors.Clear();
            }
        }

        void healthPacksLev1()
        {
            healthPacks.Add(new HealthPack(Content.Load<Texture2D>("healthPack"), 1500, 550));
            healthPacks.Add(new HealthPack(Content.Load<Texture2D>("healthPack"), 100, -50));
        }
        void healthPickUp()
        {
            for(int i = 0; i < healthPacks.Count; i++)
            {
                if(player.playerRect.Intersects(healthPacks[i].healthRect)&& player.health < 3 && !healthPacks[i].pickUp)
                {
                    player.health++;
                    health.Add(new Health(Content.Load<Texture2D>("Heart"), 0, player.playerRect.Y - 300));
                    healthPacks[i].pickUp = true;
                }
            }
        }
    }
}
