#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;


#endregion

namespace SET_Breakout
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;
        const int PADDLE_OFFSET = 30;
        const float BALL_START_SPEED = 8f;
        const float KEYBOARD_PADDLE_SPEED = 10f;

        Player player1;
        //Player player2;
        Ball ball;
        Brick[] brick;
        FrickinLaser laser1;
        FrickinLaser laser2;
        SpriteFont Font1;
        Vector2 FontPos;

        public Game1()
            : base()
        {
           _graphics = new GraphicsDeviceManager(this);
           _graphics.IsFullScreen = false;
           _graphics.PreferredBackBufferHeight = 800;
           _graphics.PreferredBackBufferWidth = 1170;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
           

            player1 = new Player();
            player1.score = 0;
            brick = new Brick[66];
            
            int count = 0;

            foreach (Brick b in brick)
            {
                brick[count] = new Brick();
                count++;
            }

            ball = new Ball();
            laser1 = new FrickinLaser();
            laser2 = new FrickinLaser();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
                // Create a new SpriteBatch, which can be used to draw textures.
                _spriteBatch = new SpriteBatch(GraphicsDevice);

           Font1 = Content.Load<SpriteFont>("Arial");
        FontPos = new Vector2(10,10);
        // TODO: use this.Content to load your game content here
       player1.Texture = Content.Load<Texture2D>("Paddle");

       foreach (Brick b in brick)
       {
           b.Texture = Content.Load<Texture2D>("Brick");
       }
     //  player2.Texture = Content.Load<Texture2D>("Paddle");
 
        player1.Position = new Vector2(PADDLE_OFFSET, ScreenHeight / 2 + 370);

        int brickposX = 10;
        int brickposY = 10;
        int row = 0;

        foreach (Brick b in brick)
        {    
            if (brickposX > Game1.ScreenWidth)
            {
                brickposX = 10;
                brickposY += (10 + b.Texture.Height);
                row++;

            }
          
             b.Position = new Vector2(brickposX, brickposY);
             b.row = row;
             brickposX += (10 + b.Texture.Width);
            
        }

    
      //  player2.Position = new Vector2(ScreenWidth - player2.Texture.Width - PADDLE_OFFSET, ScreenHeight / 2 - player2.Texture.Height / 2);

 
        ball.Texture = Content.Load<Texture2D>("Ball");
        laser1.Texture = Content.Load<Texture2D>("laser");
        laser2.Texture = Content.Load<Texture2D>("laser");

        ball.Position = new Vector2(Game1.ScreenWidth / 2 - ball.Texture.Width / 2, Game1.ScreenHeight / 2 - ball.Texture.Height / 2);
        if (laser1.alive == false && laser2.alive == false)
        {
            laser1.Position = new Vector2(player1.Position.X, player1.Position.Y - laser1.Texture.Height);
            laser2.Position = new Vector2(player1.Position.X + (int)(player1.Texture.Height * player1.SizeMultiplier) - laser2.Texture.Width, player1.Position.Y - laser2.Texture.Height);
        }
}
         

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))// GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Exit();

            // TODO: Add your update logic here
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ball.Move(ball.Velocity);
            laser1.Move(laser1.Velocity);
            laser2.Move(laser1.Velocity);

            if (ball.HitTop)
            {
                if (!ball.Triggered)
                {
                    player1.SizeMultiplier = 0.5;
                    ball.Triggered = true;
                }
            }

            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 player1Velocity = Input.GetKeyboardInputDirection(PlayerIndex.One) * KEYBOARD_PADDLE_SPEED;
          //  Vector2 player2Velocity = Input.GetKeyboardInputDirection(PlayerIndex.Two) * KEYBOARD_PADDLE_SPEED;

            player1.Move(player1Velocity);
            if (laser1.alive == false && laser2.alive == false)
            {
                laser1.Position = new Vector2(player1.Position.X, player1.Position.Y - laser1.Texture.Height);
                laser2.Position = new Vector2(player1.Position.X + (int)(player1.Texture.Height * player1.SizeMultiplier) - laser2.Texture.Width, player1.Position.Y - laser2.Texture.Height);
            }
            
          //  player2.Move(player2Velocity);
            
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                ball.Launch(BALL_START_SPEED);
            }

            if (keyboardState.IsKeyDown(Keys.F))
            {
                if (laser1.alive == false && laser2.alive == false)
                {
                    laser1.alive = true;
                    laser2.alive = true;
                    laser1.Launch(BALL_START_SPEED);
                    laser2.Launch(BALL_START_SPEED);
                }
            }

            if (GameObject.CheckPaddleBallCollision(player1, ball))
            {
                ball.Velocity.Y = -Math.Abs(ball.Velocity.Y);        
            }
            foreach (Brick b in brick)
            {
                if (b.alive == true)
                {
                    if (GameObject.CheckPaddleBallCollision(b, ball))
                    {
                        if (get_side(b) == 0)
                        {
                            ball.Velocity.X *= -1;//Math.Abs(ball.Velocity.Y);
                        }
                        else
                        {
                            ball.Velocity.Y *= -1;
                        }

                        b.alive = false;

                        ball.SpeedIncrease(b.row);

                        ball.Collided();

                        player1.score += get_score(b);
                    }

                    if (laser1.alive == true)
                    {
                        if (GameObject.CheckPaddleLaserCollision(b, laser1))
                        {
                            b.alive = false;
                            laser1.alive = false;
                            player1.score += get_score(b);
                        }
                    }
                    if (laser2.alive == true)
                    {
                        if (GameObject.CheckPaddleLaserCollision(b, laser2))
                        {
                            b.alive = false;
                            laser2.alive = false;
                            player1.score += get_score(b);
                        }
                    }
                }
            }
 
          /*  if (GameObject.CheckPaddleBallCollision(player2, ball))
            {
                ball.Velocity.X = -Math.Abs(ball.Velocity.X);
            }*/
 
            if (ball.Position.X + ball.Texture.Width < 0)
            {
                ball.Launch(BALL_START_SPEED);
            }

            if (ball.Position.X + ball.Texture.Width < 0)
                {
                    ball.Launch(BALL_START_SPEED);
                }
 
                if (ball.Position.X > ScreenWidth)
                {
                    ball.Launch(BALL_START_SPEED);
                }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            
            player1.Draw(_spriteBatch);
            foreach (Brick b in brick)
            {
                if (b.alive == true)
                {
                    b.Draw(_spriteBatch);
                }
            }
          //  player2.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);
            if(laser1.alive == true)
            {
                laser1.Draw(_spriteBatch);
            }

            if (laser2.alive == true)
            {
                laser2.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(
            Font1,                          // SpriteFont
            "Score: " + player1.score.ToString(),  // Text
            FontPos,                      // Position
            Color.White); 

            _spriteBatch.End();
            base.Draw(gameTime);
        }


        public int get_side(Brick b)
        {
            float x_col = 0;
            float y_col = 0;

            if (ball.Position.X < b.Position.X + b.Texture.Width)
            {
                x_col = (b.Position.X + b.Texture.Width) - ball.Position.X;
            }
            else if (ball.Position.X > b.Position.X)
            {
                x_col = ball.Position.X - b.Position.X;
            }
            else
            {
                x_col = 0;
            }

            if (ball.Position.Y < b.Position.Y + b.Texture.Height)
            {
                y_col = (b.Position.Y + b.Texture.Height) - ball.Position.Y;
            }
            else if (ball.Position.Y > b.Position.Y)
            {
                y_col = ball.Position.Y - b.Position.Y;
            }
            else
            {
                y_col = 0;
            }

            if (y_col > x_col)
            {
                return 0;
            }
            else
            {
                return 1;
            }
     
        }

        public int get_score(Brick b)
        {
            switch (b.row)
            {
                case 0:
                    return 5;
                case 1:
                    return 5;
                case 2:
                    return 3;
                case 3:
                    return 3;
                case 4:
                    return 1;
                case 5:
                    return 1;
            }
            return 0;
        }


    }
}
