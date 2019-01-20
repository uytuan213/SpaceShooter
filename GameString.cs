/*
 * ProgramID: Game Final Project
 * 
 * Purpose: Display string on screen
 * 
 * Revision history: 
 *      Tony Trieu written Dec 3, 2018
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
    /// Class to display string
    /// </summary>
    public class GameString : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private string message;
        private Vector2 position;
        private Color color;
        public string Message { get => message; set => message = value; }
        /// <summary>
        /// constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="font">SPriteFont</param>
        /// <param name="message">Message</param>
        /// <param name="position">Position of the string</param>
        /// <param name="color">Color of the string</param>
        public GameString(Game game, SpriteBatch spriteBatch,
            SpriteFont font, string message, 
            Vector2 position, Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.message = message;
            this.position = position;
            this.color = color;
        }

        /// <summary>
        /// Drawing string to the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update string
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
