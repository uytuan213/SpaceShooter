/*
 * Program ID: Game Final Project
 * 
 * Purpose: Menu components 
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
    /// Class to contains menu components
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private const int MARGIN_TOP = 100;

        private SpriteBatch spriteBatch;
        private SpriteFont regularFont;
        List<string> menuItems;
         public int selectedIndex { get; set; } = 0;
        Color hilightColor = Color.Red;
        Color regularColor = Color.White;

        KeyboardState oldState;
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="regularFont">Font</param>
        /// <param name="menuItems">Menu items</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch,
            SpriteFont regularFont,
            string[] menuItems) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.menuItems = menuItems.ToList();
            //position = new Vector2(Shared.stage.X/2, Shared.stage.Y/2);
        }
        /// <summary>
        /// Draw menu components to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos;
            tempPos.Y = Shared.stage.Y / 3;
            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
             
                if (i == selectedIndex)
                {
                    Vector2 size = regularFont.MeasureString(menuItems[i]);
                    tempPos.X = Shared.stage.X / 2 - size.X / 2;
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, hilightColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
                else
                {
                    Vector2 size = regularFont.MeasureString(menuItems[i]);
                    tempPos.X = Shared.stage.X / 2 - size.X / 2;
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update menu components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if(selectedIndex >= menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}
