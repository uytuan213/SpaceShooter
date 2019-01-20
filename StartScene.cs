/*
 * Program ID: Game Final Project
 * 
 * Purpose: start Scene for Game
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
    /// Class to display start scene
    /// </summary>
    public class StartScene : GameScene
    {
        public MenuComponent menu { get; set; }
        private SpriteBatch spriteBatch;
        string[] menuItems =
        {
            "Start Game",
            "Help",
            "High Score",
            "About",
            "Quit"
        };
        /// <summary>
        /// Contructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public StartScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            //Menu Items
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            
            menu = new MenuComponent(game, spriteBatch, regularFont, menuItems);
            this.Components.Add(menu);
        }
        /// <summary>
        /// Drawing scene to screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update scene 
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
