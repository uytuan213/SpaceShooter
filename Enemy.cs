/*
 * Program ID: Game Final Project
 * 
 * Purpose: Display enemy
 * 
 * Revision History:
 *      Tony Trieu written Nov 26, 2018
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
    /// Class to display Enemy which is inherit from ship
    /// </summary>
    public class Enemy : Ship
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Image of the enemy</param>
        /// <param name="position">position of the enemy</param>
        /// <param name="speed">speed of the enemy</param>
        public Enemy(Game game, SpriteBatch spriteBatch,
             Texture2D tex, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            this.speed = speed;
            scale = 0.04f;
            rotation = -MathHelper.PiOver2;
            Delay = 50;
            DelayCounter = 0;
        }


        /// <summary>
        /// Drawing enemy to the screen
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
        /// Update enemy position
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            position += speed;
            if (position.X < 0)
            {
                OutOfScreen = true;
                this.Enabled = false;
                this.Visible = false;
                position.X = -100;
                position.Y = -100;
            }
            base.Update(gameTime);
        }
    }
}
