/*
 * Program ID: Game Final Project
 * 
 * Purpose: Display Bullet 
 * 
 * Revision History:
 *      Tony Trieu written Nov 26, 2018
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
    /// Class to display bullet
    /// </summary>
    public class Bullet : DrawableGameComponent
    {
        private const int BULLET_DISTANCE = 8;

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Rectangle srcRect;
        private Vector2 origin;
        private float scale = 0.02f;
        private float rotation = 0;
        private Vector2 speed;
        private Point destination;
        private Ship ship;
        //Attribute to check if the bullet out of screen
        public Boolean outOfScreen = false;
        
        public Vector2 Position { get => position; set => position = value; }
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Images of the bullet</param>
        /// <param name="position">Position of the bullet</param>
        /// <param name="ship">the ship that own the buller</param>
        /// <param name="destination">destination of the bullet</param>
        public Bullet(Game game, SpriteBatch spriteBatch,
            Texture2D tex, Vector2 position, Ship ship,
            Point destination) : base(game)
        {
            float deviation = 0;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(0, tex.Height / 2);
            this.destination = destination;
            this.ship = ship;
            float xDiff = destination.X - ship.Position.X;
            float yDiff = destination.Y - ship.Position.Y;
            calculateSpeed(xDiff, yDiff);
            if(xDiff < 0)
            {
                deviation = (float)Math.PI;
            }
            rotation = (float)Math.Atan(yDiff / xDiff) + deviation;
        }
        /// <summary>
        /// Calculate the speed base on 
        /// ship position and destination position
        /// </summary>
        /// <param name="xDiff">destination.X - ship.Position.X</param>
        /// <param name="yDiff">destination.Y - ship.Position.Y</param>
        private void calculateSpeed(float xDiff, float yDiff)
        {
            //linear equation y = ax + b
            //slope is a
            float slope;
            //intercept is b
            float intercept;
            //temporary vector to get speed
            Vector2 temp;
            //slope = (y1 - y2) / (x1 - x2)
            slope = yDiff / xDiff;
            //intercept = y - ax
            intercept = ship.Position.Y - (ship.Position.X * slope);

            temp.X = ship.Position.X + BULLET_DISTANCE;
            temp.Y = slope * temp.X + intercept;
            //get speed by minus spaceship position to temp
            speed = temp - ship.Position;

            if (xDiff < 0)
            {
                speed = -speed;
            } 
        }

        /// <summary>
        /// Drawing bullet to the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {   
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation,
                origin, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update position of the bullet
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            if (position.X > Shared.stage.X
                || position.Y > Shared.stage.Y)
            {
                this.Enabled = false;
                this.Visible = false;
                this.position = new Vector2(-100, -100);
                outOfScreen = true;
            }
            else if (position.X > 0 && position.Y > 0)
            {
                position.X += speed.X;
                position.Y += speed.Y; 
            }
            else
            {
                this.Enabled = false;
                this.Visible = false;

                position = new Vector2(-100, -100);
                outOfScreen = true;
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Get bullet's bound
        /// </summary>
        /// <returns>Bound of the bullet</returns>
        public Rectangle getBound()
        {
            return new Rectangle((int)(position.X - tex.Width * scale),
                (int)(position.Y - tex.Height * scale),
                (int)(tex.Width * scale),
                (int)(tex.Height * scale));
        }
    }
}
