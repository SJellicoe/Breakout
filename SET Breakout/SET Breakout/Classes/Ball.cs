using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SET_Breakout
{
    public class Ball : GameObject
    {
        public Vector2 Velocity;
        public Random random;
        public bool HitTop { get; set; }
        private bool hitMiddleRow = false;
        private bool hitTopRow = false;
        public bool Triggered { get; set; }
        const float BALL_START_SPEED = 8f;
        private float speedMultiplier = BALL_START_SPEED;

        public bool alive = true;
        private int collisions = 0;

        public Ball()
        {
            random = new Random();
        }
        public void Launch(float speed)
        {
            Position = new Vector2(Game1.ScreenWidth / 2 - Texture.Width / 2, Game1.ScreenHeight / 2 - Texture.Height / 2);
            // get a random + or - 60 degrees angle to the right
            float rotation = (float)(Math.PI / 2 + (random.NextDouble() * (Math.PI / 1.3f) - Math.PI / 3));

            Velocity.X = (float)Math.Sin(rotation);
            Velocity.Y = (float)Math.Cos(rotation);

            // 50% chance whether it launches left or right
            if (random.Next(2) == 1)
            {
                Velocity.X *= -1; //launch to the left
            }

            if (Velocity.Y < 0)
            {
                Velocity.Y *= -1;
            }

            Velocity *= speed;
        }
        public void CheckWallCollision()
        {
            if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y *= -1;
                HitTop = true;
            }
            if (Position.Y + Texture.Height > Game1.ScreenHeight)
            {
                Position = new Vector2(Game1.ScreenWidth / 2 - Texture.Width / 2, Game1.ScreenHeight / 2 - Texture.Height / 2);
                Velocity.Y = 0;
                Velocity.X = 0;
                alive = false;
                speedMultiplier = BALL_START_SPEED;
                HitTop = false;
                hitTopRow = false;
                hitMiddleRow = false;
                collisions = 0;

            }
            if (Position.X < 0)
            {
                Position.X = 0;
                Velocity.X *= -1;
            }
            if (Position.X + Texture.Width > Game1.ScreenWidth)
            {
                Position.X = Game1.ScreenWidth - Texture.Width;
                Velocity.X *= -1;
            }
        }

        public void SpeedIncrease(int row)
        {
            if (row == 2 || row == 3)
            {
                if (!hitMiddleRow)
                {
                    Velocity *= (float)1.10;
                    hitMiddleRow = true;
                }
            }
            else if (row == 0 || row == 1)
            {
                if (!hitTopRow)
                {
                    Velocity *= (float)1.10;
                    hitTopRow = true;
                }
            }
        }

        public void Collided()
        {
            collisions++;
            if (collisions == 4 || collisions == 12)
            {
                Velocity *= (float)1.10;
            }
        }

         public override void Move(Vector2 amount)
         {
             base.Move(amount);
             CheckWallCollision();
         }

         public void ReflectAngle(int paddleMiddle, int paddleWidth)
         {
             int ballMiddle = (int)(Position.X + Texture.Width / 2);
             int offset = ballMiddle - paddleMiddle;
             double percentage = Math.Abs((double)offset / ((double)paddleWidth / 2));
             Velocity /= speedMultiplier;
             if (Velocity.X < 0 && offset < 0)
             {
                 Velocity.X += Velocity.X * (float)percentage;
             }
             else if (Velocity.X < 0 && offset < 0)
             {
                 Velocity.X -= Velocity.X * (float)percentage;
             }
             else if (Velocity.X > 0 && offset < 0)
             {
                 Velocity.X -= Velocity.X * (float)percentage;
             }
             else if (Velocity.X > 0 && offset > 0)
             {
                 Velocity.X += Velocity.X * (float)percentage;
             }

             Velocity *= speedMultiplier;
         }
    }

}