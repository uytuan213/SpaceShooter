/*
 * Program ID: Game Final Project
 * 
 * Purpose: Action Scene for Game
 * 
 * Revision History:
 *      Tony Trieu written Nov 19, 2018
 *      Tony Trieu modify Nov 29, 2018
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
using Microsoft.Xna.Framework.Media;

namespace UTFinalProject
{
    /// <summary>
    /// Class to create and display 
    /// play screen including ships, enemy, and shield
    /// </summary>
    public class ActionScene : GameScene
    {
        private const int MARGIN_RIGHT = 300;
        private const int MARGIN_LEFT = 100;
        private const int MARGIN_TOP = 100;
        private const int ENEMY_SPEED_X = -2;
        private const int MIN_APPEAR_TIME = 50;

        private SpriteBatch spriteBatch;
        private SpaceShip spaceShip;
        //Store all ships including enemy and player ship
        private List<Ship> lstShips = new List<Ship>();
        //Variable to check if the ship is shielding
        private Boolean shieldOn = false;

        //Check if game over
        private Boolean isOver = false;
        //enemy appear delay
        private int appearTime = 200;
        //timer
        private double timer = 0;
        //When get 10 more scores --> decrease appearTime by 1 --> level up
        private int nextLevel = 10;
        //variable to avoid saving many times to file
        private Boolean isSave = false;
        private List<Bullet> lstBullet = new List<Bullet>();
        //List to store all enemy's images
        private List<Texture2D> lstTexEnemies;

        //Soundtracks
        private SoundEffect soundShooting;
        private SoundEffect soundExplosion;
        private SoundEffect soundGameover;

        private Shield shield;

        private Game game;
        private GameString strScore;

        private Texture2D texBullet;


        int score = 0;
        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            texBullet = game.Content.Load<Texture2D>("Images/bullet");

            //Add enemies texture to lstEnemies
            lstTexEnemies = new List<Texture2D>()
            {
                game.Content.Load<Texture2D>("Images/Enemies/enemy1"),
                game.Content.Load<Texture2D>("Images/Enemies/enemy2"),
                game.Content.Load<Texture2D>("Images/Enemies/enemy3")
            };
            
            // Add Spaceship
            Texture2D texShip = game.Content.Load<Texture2D>("Images/redShip");
            Vector2 pos = new Vector2(Shared.stage.X / 4, Shared.stage.Y / 2);
            spaceShip = new SpaceShip(game, spriteBatch, texShip, pos);
            //player's ship always at index 0 of the list
            lstShips.Add(spaceShip);
            this.Components.Add(spaceShip);

            //Add Cursor to the screen
            Texture2D texCursor = game.Content.Load<Texture2D>("Images/aim");
            Vector2 posCursor = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            Cursor c = new Cursor(game, spriteBatch, texCursor, posCursor);
            this.Components.Add(c);

            //Add shield
            Texture2D texShield = game.Content.Load<Texture2D>("Images/shield");
            Vector2 posShield = new Vector2(-100, -100);

            //Vector2 posShield = spaceShip.Position;
            int delayShield = 4;
            Vector2 speedShield = new Vector2(4, 0);
            shield = new Shield(game, spriteBatch, texShield, posShield, delayShield, speedShield);
            shield.IsOn = false;
            //this.Components.Add(shield);

            //Soundtracks
            soundShooting = Game.Content.Load<SoundEffect>("Musics/shoot");
            soundExplosion = Game.Content.Load<SoundEffect>("Musics/explosion");
            soundGameover = Game.Content.Load<SoundEffect>("Musics/gameover");

            //Add score to screen
            SpriteFont fontScore = game.Content.Load<SpriteFont>("Fonts/regularFont");
            Vector2 posScore = new Vector2(MARGIN_LEFT, 0);
            string messageScore = "Score: " + score.ToString();
            strScore = new GameString(game, spriteBatch, fontScore, messageScore, posScore, Color.White);
            this.Components.Add(strScore);

            //Add highest score to screen
            Vector2 posHighestScore = new Vector2(Shared.stage.X - MARGIN_RIGHT, 0);
            GameString strHighestScore = new GameString(game, spriteBatch, fontScore, $"Highest Score: {Shared.getHighestScore()}", posHighestScore, Color.White);
            this.Components.Add(strHighestScore);
        }
        /// <summary>
        /// Drawing method
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        /// <summary>
        /// Update method:
        /// Handle collision, shooting, etc
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            //Random variable to generate enemies randomly
            Random r = new Random();

            //if there is no player ship in list --> game over
            if (!lstShips.Contains(spaceShip))
            {
                if (!isOver)
                {
                    soundGameover.Play();
                    isOver = true;
                }
                //Display Game over string
                SpriteFont fontGameOver = game.Content.Load<SpriteFont>("Fonts/GameOverFont");
                Vector2 sizeGameOver = fontGameOver.MeasureString("Game over");
                Vector2 posGameOver = new Vector2(Shared.stage.X / 2 - sizeGameOver.X / 2, MARGIN_TOP);
                GameString gameOverString = new GameString(game, spriteBatch, fontGameOver, "Game over", posGameOver, Color.White);
                this.Components.Add(gameOverString);

                SpriteFont RegularFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");
                
                //Display message asking if user want to save score
                string stringInfo = "Press enter to save your score";
                Vector2 sizeInfo = RegularFont.MeasureString(stringInfo);
                Vector2 posInfo = new Vector2(Shared.stage.X / 2 - sizeInfo.X / 2, posGameOver.Y + fontGameOver.LineSpacing);

                GameString info = new GameString(game, spriteBatch, RegularFont, stringInfo, posInfo, Color.White);
                this.Components.Add(info);
                
                KeyboardState ks = Keyboard.GetState();
                //if user press enter and the score haven't saved to file
                if (!isSave && ks.IsKeyDown(Keys.Enter))
                {
                    isSave = true;
                    Shared.recordNewScore(score);
                    //Add messege after saving
                    string stringSuccess = "Saved successfully!\nPress Esc to go back";
                    Vector2 sizeSuccess = RegularFont.MeasureString(stringSuccess);
                    Vector2 posSuccess = new Vector2(Shared.stage.X / 2 - sizeSuccess.X / 2,
                        posInfo.Y + RegularFont.LineSpacing);
                    GameString success = new GameString(game, spriteBatch, RegularFont, stringSuccess, posSuccess, Color.White);
                    this.Components.Add(success);
                }
            }
            if (!isOver)
            {
                timer++;
                //Add enemies every appearTime
                if (timer > appearTime)
                {
                    //if there is ship in lst
                    if (lstShips.Count != 0)
                    {
                        Enemy e;
                        //there is 3 images of enemies, random to pick one
                        int enemyIndex = r.Next(0, lstTexEnemies.Count);
                        Vector2 speedEnemy;
                        speedEnemy = new Vector2(ENEMY_SPEED_X, 0);
                        //if enemies overlap each other --> generate new position
                        do
                        {
                            Vector2 posEnemy = new Vector2(r.Next((int)Shared.stage.X, (int)Shared.stage.X + 500),
                                r.Next((int)(lstTexEnemies[enemyIndex].Height / 2 * lstShips.Last().Scale),
                                (int)(Shared.stage.Y - lstTexEnemies[enemyIndex].Height / 2 * lstShips.Last().Scale)));
                            e = new Enemy(game, spriteBatch, lstTexEnemies[enemyIndex], posEnemy, speedEnemy);
                        } while (lstShips.Last().getBound().Intersects(e.getBound())); 
                    
                        //increase enemies shooting speed 
                        if(score % nextLevel == 0 && score > 0)
                        {
                            e.Delay -= 20;
                        }
                        lstShips.Add(e);
                        this.Components.Add(e);
                        timer = 0;
                    }
                }


                //Spaceship cannot shoot at the back
                if (ms.Position.X - spaceShip.Position.X > 0)
                {
                    if (ms.LeftButton == ButtonState.Pressed)
                    {
                        if (spaceShip.DelayCounter > spaceShip.Delay)
                        {
                            //Texture2D texBullet = game.Content.Load<Texture2D>("Images/bullet");
                            Vector2 posBullet = new Vector2(spaceShip.Position.X
                                + spaceShip.Scale * (spaceShip.Tex.Width / 2),
                                spaceShip.Position.Y);
                            Bullet b = new Bullet(game, spriteBatch, texBullet, posBullet, spaceShip, ms.Position);
                            lstBullet.Add(b);
                            this.Components.Add(b);
                            soundShooting.Play();
                            spaceShip.DelayCounter = 0;
                        }
                        else
                        {
                            spaceShip.DelayCounter++;
                        }
                    }
                    if (ms.LeftButton == ButtonState.Released)
                    {
                        spaceShip.DelayCounter = spaceShip.Delay + 1;
                    }
                }
                foreach (Ship item in lstShips)
                {
                    //If item is  enemy --> auto shoot
                    if (item is Enemy)
                    {

                        //Check if enemy is on the screen
                        Enemy e = (Enemy)item;
                        if (!e.OutOfScreen && e.Position.X < Shared.stage.X
                            && e.Position.X > spaceShip.Position.X)
                        {
                            //Initalize Bullet 
                            if (item.DelayCounter > item.Delay)
                            {
                                //Texture2D texBullet = 
                                //    game.Content.Load<Texture2D>("Images/bullet");

                                Vector2 posBullet = new Vector2(item.Position.X
                                    - (item.Tex.Width * item.Scale),
                                    item.Position.Y);
                                Point destination = 
                                    new Point((int)spaceShip.Position.X,
                                    (int)spaceShip.Position.Y);
                                Bullet b = new Bullet(game, spriteBatch, texBullet, posBullet, item, destination);
                                lstBullet.Add(b);
                                this.Components.Add(b);
                                soundShooting.Play();
                                item.DelayCounter = 0;
                            }
                            item.DelayCounter++;
                        }
                    }
                }

                //Check collision
                //if there is bullet on screen
                if (lstBullet.Count > 0)
                {
                    for (int i = lstBullet.Count - 1; i >= 0; i--)
                    {
                        for (int j = lstShips.Count - 1; j >= 0; j--)
                        {
                            Rectangle boundBullet = lstBullet[i].getBound();
                            Rectangle boundShip = lstShips[j].getBound();
                            if (boundBullet.Intersects(boundShip))
                            {
                                Texture2D texExplosion =
                                    game.Content.Load<Texture2D>("Images/explosion");
                                Vector2 posExplosion = new Vector2(lstBullet[i].Position.X,
                                    lstBullet[i].Position.Y);
                                int delayExplosion = 5;
                                Explosion ex =
                                    new Explosion(game,
                                    spriteBatch, texExplosion,
                                    posExplosion, delayExplosion, soundExplosion);
                                this.Components.Add(ex);

                                lstBullet[i].Visible = false;
                                lstBullet[i].Enabled = false;
                                lstBullet[i].Position = new Vector2(-100, -100);
                                Console.WriteLine("ShieldOn = " + shieldOn);
                                if (shieldOn && lstShips[j] == spaceShip)
                                {
                                    shieldOn = false;
                                    shield.IsOn = false;
                                    this.Components.Remove(shield);
                                }
                                else
                                {
                                    lstShips[j].Visible = false;
                                    lstShips[j].Enabled = false;
                                    lstShips[j].Position = new Vector2(-100, -100);
                                    lstShips.RemoveAt(j);

                                    if (lstShips.Contains(spaceShip))
                                    {
                                        strScore.Message = $"Score: {++score}"; 
                                    }
                                    if(score % nextLevel == nextLevel - 1)
                                    {
                                        appearTime -= 20;
                                    }
                                }
                                
                                lstBullet.RemoveAt(i);
                                break;
                            }
                            //if (!lstShips.Contains(spaceShip))
                            //{
                            //    isOver = true;

                            //    SpriteFont fontGameOver = game.Content.Load<SpriteFont>("Fonts/GameOverFont");
                            //    Vector2 sizeGameOver = fontGameOver.MeasureString("Game over");
                            //    Vector2 posGameOver = new Vector2(Shared.stage.X / 2 - sizeGameOver.X / 2, MARGIN_TOP);
                            //    GameString gameOverString = new GameString(game, spriteBatch, fontGameOver, "Game over", posGameOver, Color.White);
                            //    SpriteFont fontInfo = game.Content.Load<SpriteFont>("Fonts/RegularFont");

                            //    string stringInfo = "Press enter to save your score";
                            //    Vector2 sizeInfo = fontGameOver.MeasureString(stringInfo);
                            //    Vector2 posInfo = new Vector2(Shared.stage.X / 2 - sizeInfo.X / 2, posGameOver.Y + fontGameOver.LineSpacing);

                            //    GameString info = new GameString(game, spriteBatch, fontInfo, "Press enter to save your scores", posGameOver, Color.White);
                            //    this.Components.Add(info);
                            //    soundGameover.Play();
                            //    KeyboardState ks = Keyboard.GetState();
                            //    if (ks.IsKeyDown(Keys.Enter))
                            //    {
                            //        Shared.recordNewScore(score);
                            //        //Show high score screen
                            //        HighScoreScene highScoreScene = new HighScoreScene(game, spriteBatch);
                            //        this.hide();
                            //        highScoreScene.show();
                            //    }
                            //}
                        }
                    }
                }

                //if enemy out of screen --> remove from lstShips
                lstShips.RemoveAll(s => s.OutOfScreen);
                //If bullet is out of screen --> delete
                for (int i = lstBullet.Count - 1; i >= 0; i--)
                {
                    Bullet b = lstBullet[i];
                    if (b.outOfScreen)
                    {
                        lstBullet.Remove(b);
                    }
                }
                //if there is no sheild --> add one
                if (!shield.IsOn)
                {
                    shield.IsOn = true;
                    shield.Position = new Vector2(r.Next((int)Shared.stage.X * 2, (int)Shared.stage.X * 4),
                        r.Next(0, (int)Shared.stage.Y));
                    this.Components.Add(shield);
                }
                else
                {
                    if (spaceShip.getBound().Intersects(shield.getBound()))
                    {
                        shieldOn = true;
                        shield.Position = spaceShip.Position;
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
