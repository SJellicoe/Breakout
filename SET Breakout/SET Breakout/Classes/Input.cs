using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
 
namespace SET_Breakout
{
    public static class Input
    {
        static Input()
        {

        }
        public static Vector2 GetKeyboardInputDirection(PlayerIndex playerIndex)
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState(playerIndex);

            if (playerIndex == PlayerIndex.One)
            {
                if (keyboardState.IsKeyDown(Keys.A))
                    direction.X += -1;
                if (keyboardState.IsKeyDown(Keys.D))
                    direction.X += 1;
            }

            if (playerIndex == PlayerIndex.Two)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                    direction.X += -1;
                if (keyboardState.IsKeyDown(Keys.Down))
                    direction.X += 1;
            }
            return direction;
        }
    }
}