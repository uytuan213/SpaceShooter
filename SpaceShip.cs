/*
 * Program ID: Game Final Project
 * 
 * Purpose: Spaceship class
 * 
 * Revision History:
 *      Tony Trieu written Nov 20, 2018
 *      Tony Trieu modified Nov 28, 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UTFinalProject
{
    /// <summary>
    /// Class to display spaceship which is inherited from ship
    /// </summary>
    public class SpaceShip : Ship
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Image of the ship</param>
        /// <param name="position">Position of the ship</param>
        public SpaceShip(Game game, SpriteBatch spriteBatch,
            Texture2D tex ,Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(5, 5);
            scale = .25f;
            rotation = MathHelper.PiOver2;
            delay = 5;
            delayCounter = 6;
        }

        /// <summary>
        /// Drawing ship to screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        
        /// <summary>
        /// Update ship 
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();
            //Moving
            if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                position.Y -= speed.Y;
            }
            if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                position.Y += speed.Y;
            }
            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                position.X += speed.X;
            }
            if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {
                position.X -= speed.X;
            }

            //Check if the spaceShip is out of screen
            if (position.X - tex.Width /2 * scale < 0)
            {
                position.X = tex.Width / 2 * scale;
            }
            if (position.X + tex.Width / 2 * scale > Shared.stage.X)
            {
                position.X = Shared.stage.X - tex.Width / 2 * scale;
            }
            if (position.Y - tex.Height / 2 * scale < 0)
            {
                position.Y = tex.Height / 2 * scale;
            }
            if (position.Y + tex.Height / 2 * scale > Shared.stage.Y)
            {
                position.Y = Shared.stage.Y - tex.Height / 2 * scale;
            }
            base.Update(gameTime);
        }
    }
}
