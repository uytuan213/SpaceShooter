/*
 * Program ID: Game Final Project
 * 
 * Purpose: Ship class
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UTFinalProject
{
    /// <summary>
    /// Class to display ship
    /// </summary>
    public abstract class Ship : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected Texture2D tex;
        protected Vector2 position;
        protected Rectangle srcRect;
        protected Vector2 origin;
        protected float scale = 0.04f;
        protected float rotation;
        protected Vector2 speed;
        protected int delay;
        protected int delayCounter;

        private Boolean outOfScreen = false;
        public Vector2 Position { get => position; set => position = value; }
        public float Scale { get => scale; set => scale = value; }
        public Texture2D Tex { get => tex; }
        public int Delay { get => delay; set => delay = value; }
        public int DelayCounter { get => delayCounter; set => delayCounter = value; }
        public bool OutOfScreen { get => outOfScreen; set => outOfScreen = value; }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        public Ship(Game game) : base(game)
        {
            spriteBatch = null;
            tex = null;
            position = Vector2.Zero;
            srcRect = Rectangle.Empty;
            origin = Vector2.Zero;
            scale = 0;
            rotation = 0;
            speed = Vector2.Zero;
        }
        /// <summary>
        /// Get bound  of the ship
        /// </summary>
        /// <returns>rectangle of bound of the ship</returns>
        public Rectangle getBound()
        {
            return new Rectangle((int)(position.X - tex.Width * scale),
                (int)(position.Y - tex.Height * scale),
                (int)(tex.Width * scale),
                (int)(tex.Height * scale));
        }
        
    }
}
