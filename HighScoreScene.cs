/*
 * ProgramID: Game Final Project
 * 
 * Purpose: Display high scores screen
 * 
 * Revision history: 
 *      Tony Trieu written Dec 6, 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace UTFinalProject
{
    /// <summary>
    /// Class to display high scores
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private const int MARGIN_TOP = 50;

        private SpriteBatch spriteBatch;
        private string title;
        
        /// <summary>
        /// constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public HighScoreScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            title = "Top 10 Scores";
            List<int> lstHighScores = Shared.getHighScoresList();
            

            //add high scores to screen
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/regularFont");
            Vector2 posTitle = new Vector2
                (Shared.stage.X / 2 - font.MeasureString(title).X / 2, MARGIN_TOP);
            GameString stringTitle = new GameString
                (game, spriteBatch, font, title, posTitle, Color.Red);
            this.Components.Add(stringTitle);

            if (lstHighScores != null)
            {
                for (int i = 0; i < lstHighScores.Count; i++)
                {
                    //string score = $"{i + 1}.{lstHighScores[i].ToString("D6")}";
                    string score = lstHighScores[i].ToString("D6");
                    Vector2 size = font.MeasureString(score);
                    Vector2 pos = new Vector2
                        (Shared.stage.X / 2 - size.X / 2,
                        font.LineSpacing * i + font.LineSpacing + MARGIN_TOP);
                    GameString stringScore = new GameString
                        (game, spriteBatch, font, score, pos, Color.White);
                    this.Components.Add(stringScore);
                } 
            }
        }
    }
}
