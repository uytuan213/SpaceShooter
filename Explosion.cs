/*
 * Program ID: Game Final Project
 * 
 * Purpose: Display explose animation
 * 
 * Revision History:
 *      Tony Trieu written Nov 28, 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace UTFinalProject
{
    /// <summary>
    /// Class to display explosion
    /// </summary>
    public class Explosion : DrawableGameComponent
    {
        private const int TEX_ROW = 5;
        private const int TEX_COL = 5;

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private Vector2 dimension;
        private SoundEffect sound;
        private int delay;
        private int delayCounter;
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Spritesheet of the explosion</param>
        /// <param name="position">position of the explosion</param>
        /// <param name="delay">Delay</param>
        public Explosion(Game game, SpriteBatch spriteBatch, Texture2D tex,
            Vector2 position, int delay, SoundEffect sound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.sound = sound;
            dimension = new Vector2(64, 64);
            createFrames();
        }
        /// <summary>
        /// Create frame 
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
        /// Drwaing explosion to the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White); 
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// update explosion animation
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {

                frameIndex++;
                if (frameIndex == 0)
                {
                    sound.Play();
                }
                if(frameIndex > (TEX_ROW * TEX_COL) - 1)
                {
                    this.Enabled = false;
                    this.Visible = false;
                    frameIndex = -1;
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }
    }
}
