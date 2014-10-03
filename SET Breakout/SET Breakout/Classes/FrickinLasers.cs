using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SET_Breakout
{
    public class FrickinLaser : GameObject
    {
        public Vector2 Velocity;
        public Random random;
        public bool alive = false;

        public void Launch(float speed)
        {
            Velocity.X = 0.0f;
            Velocity.Y = -3.0f;

            Velocity *= speed;
        }

        public void CheckWallCollision()
        {
            if (Position.Y < 0)
            {
                alive = false;
            }

        }

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            CheckWallCollision();
        }
    }


}