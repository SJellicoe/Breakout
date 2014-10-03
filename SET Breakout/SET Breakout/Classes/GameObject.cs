using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SET_Breakout
{
    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
            //spriteBatch.Draw(Texture, Position, null, Color.White, (float)(Math.PI/2), Position, 1, SpriteEffects.None, 1);
        }
        public virtual void Move(Vector2 amount)
        {
            Position += amount;
        }
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public static bool CheckPaddleBallCollision(Player player, Ball ball)
        {
            if (player.PaddleBounds.Intersects(ball.Bounds))
                return true;
            return false;
        }

        public static bool CheckPaddleBallCollision(Brick brick, Ball ball)
        {
            if (brick.Bounds.Intersects(ball.Bounds))
                return true;
            return false;
        }

        public static bool CheckPaddleLaserCollision(Brick brick, FrickinLaser laser)
        {
            if (brick.Bounds.Intersects(laser.Bounds))
                return true;
            return false;
        }

        public static bool CheckPaddlePowerUpCollision(Player player, powerup power)
        {
            if (player.PaddleBounds.Intersects(power.Bounds))
                return true;
            return false;
        }


    }
    
}
