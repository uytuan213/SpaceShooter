/*
 * Program ID: Game Final Project
 * 
 * Purpose: Display background
 * 
 * Revision History:
 *      Tony Trieu written Nov 20, 2018
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
    /// Class to dislay background
    /// </summary>
    public class StartBackground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position1;
        private Vector2 position2;
        private Vector2 speed;
        /// <summary>
        /// Constructor of the background
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Image of the background</param>
        /// <param name="speed">Speed of the background</param>
        public StartBackground(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position1 = Vector2.Zero;
            position2 = new Vector2(position1.X + tex.Width, position1.Y);
            this.speed = speed;
        }
        /// <summary>
        /// Drawing background
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position1, Color.White);
            spriteBatch.Draw(tex, position2, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Moving background
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;
            if (position1.X < -tex.Width)
            {
                position1.X = position2.X + tex.Width;
                position1.Y = position2.Y;
            }
            if (position2.X < -tex.Width)
            {
                position2.X = position1.X + tex.Width;
                position2.Y = position1.Y;
            }
            base.Update(gameTime);
        }
    }
}
