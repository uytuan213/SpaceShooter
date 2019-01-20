/*
 * Program ID: Game Final Project
 * 
 * Purpose: Display the cursor 
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
    /// Class to display the mouse cursor
    /// </summary>
    public class Cursor : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private float scale = 0.02f;
        private float rotation = 0f;
        private Rectangle rect;
        private Vector2 origin;

        /// <summary>
        /// Contructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Image of the cursor</param>
        /// <param name="position">position of the cursor</param>
        public Cursor(Game game, SpriteBatch spriteBatch,
            Texture2D tex ,Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            rect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
        }

        /// <summary>
        /// Drawing cursor to the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, rect, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update position base on mouse's position
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.Position.X >= 0 && ms.Position.X <= Shared.stage.X
                && ms.Position.Y >= 0 && ms.Position.Y <= Shared.stage.Y)
            {
                position.X = ms.X;
                position.Y = ms.Y;
            }
            base.Update(gameTime);
        }
    }
}
