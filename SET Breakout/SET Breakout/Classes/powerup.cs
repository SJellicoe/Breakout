using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SET_Breakout
{
    public class powerup : GameObject
    {
        public bool alive = false;
        public Vector2 Velocity;
        public String power;


        public void Launch(float speed)
        {
            Velocity.Y += +4;
            Velocity *= speed;
        }


        public void CheckWallCollision()
        {
            if (Position.Y + Texture.Height > Game1.ScreenHeight)
            {
                alive = false;
            }
        }
    }


}