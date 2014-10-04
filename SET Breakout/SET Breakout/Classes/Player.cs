using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SET_Breakout
{
    public class Player : GameObject
    {
        public double SizeMultiplier { get; set; }
        public int score;
        public int lives = 3;
        public bool lasers = false;
        public bool mutli = false;
        public int wins = 0;
        public Rectangle PaddleBounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Height * SizeMultiplier), Texture.Width); }
        }

        public Player()
        {
            SizeMultiplier = 1.0;
        }

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            if (Position.X <= 0)
                Position.X = 0;
            if (Position.X + (int)(Texture.Height * SizeMultiplier) >= Game1.ScreenWidth)
                Position.X = Game1.ScreenWidth - (int)(Texture.Height * SizeMultiplier);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Height * SizeMultiplier), Texture.Width), Color.White);
        }
    }


}