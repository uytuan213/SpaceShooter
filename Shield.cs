/*
 * Program ID: Game Final Project
 * 
 * Purpose: Display shield
 * 
 * Revision History:
 *      Tony Trieu written Dec 4, 2018
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
    /// class to display shield
    /// </summary>
    public class Shield : DrawableGameComponent
    {
        private const int TEX_ROW = 2;
        private const int TEX_COL = 4;
        private const int DIMENSION_X = 80;
        private const int DIMENSION_Y = 80;

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        Vector2 position;
        private List<Rectangle> frames;
        private int frameIndex = 0;
        private Vector2 dimension;
        private Vector2 speed;

        private Vector2 origin;
        private float scale = 1f;
        private float rotation = 0f;
        //private Rectangle src;

        private Boolean isOn;
        private int delay;
        private int delayCounter;

        public Vector2 Position { get => position; set => position = value; }
        public bool IsOn { get => isOn; set => isOn = value; }

        /// <summary>
        /// contructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">spritesheet of the shield</param>
        /// <param name="position">position of the shield</param>
        /// <param name="delay">delay of the shield</param>
        /// <param name="speed">speed of the shield</param>
        public Shield(Game game, SpriteBatch spriteBatch, Texture2D tex,
            Vector2 position, int delay, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.speed = speed;
            dimension = new Vector2(DIMENSION_X, DIMENSION_Y);
            origin = new Vector2(dimension.X / 2, dimension.Y / 2);
            createFrames();
            isOn = true;
        }
        /// <summary>
        /// Create frames
        /// </summary>
        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < TEX_ROW; i++)
            {
                for (int j = 0; j < TEX_COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }
        /// <summary>
        /// Drawing shield animation to screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End(); 
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update animation 
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            if (!isOn)
            {
                this.Visible = false;
                this.Enabled = false;
            }
            else
            {
                this.Visible = true;
                this.Enabled = true;
            }
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > (TEX_ROW * TEX_COL) - 1)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
                position -= speed;
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Get bound of shield
        /// </summary>
        /// <returns>rectangle of bound of shield</returns>
        public Rectangle getBound()
        {
            return new Rectangle((int)position.X - tex.Width / 2,
                (int)position.Y - tex.Height / 2,
                tex.Width,
                tex.Height);
        }
    }
}
