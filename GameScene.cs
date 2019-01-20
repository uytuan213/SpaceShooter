/*
 * Program ID: Game Final Project
 * 
 * Purpose: Game Scene for Game
 * 
 * Revision History:
 *      Tony Trieu written Nov 19, 2018
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
    /// Class of GameScene
    /// </summary>
    public class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }
        /// <summary>
        /// Show scene
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Hide scene
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Contructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide();
        }

        
        /// <summary>
        /// Drawing scene to the screen
        /// </summary>
        /// <param name="gameTime">GameTIme</param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if(item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime); 
                    }
                }
            }
            base.Draw(gameTime);
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
    }
}
