/*
 * Program ID: Game Final Project
 * 
 * Purpose: Display explose animation
 * 
 * Revision History:
 *      Tony Trieu written Nov 28, 2018
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace UTFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StartScene startScene;
        ActionScene actionScene;
        HelpScene helpScene;
        AboutScene aboutScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            Song songBackground = this.Content.Load<Song>("Musics/songBackground");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(songBackground);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //add background
            Texture2D texStartBackground = this.Content.Load<Texture2D>("Images/spaceBackground");
            Vector2 speedBackground = new Vector2(3, 0);
            StartBackground sbg = new StartBackground(this, spriteBatch, texStartBackground, speedBackground);
            this.Components.Add(sbg);

            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);
            startScene.show();

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            KeyboardState ks = Keyboard.GetState();
            int selectedIndex = 0;
            if (startScene.Enabled)
            {
                selectedIndex = startScene.menu.selectedIndex;
                if (ks.IsKeyDown(Keys.Enter))
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            startScene.hide();
                            actionScene = new ActionScene(this, spriteBatch);
                            this.Components.Add(actionScene);
                            actionScene.show();
                            break;
                        case 1:
                            startScene.hide();
                            helpScene.show();
                            break;
                        case 2:
                            startScene.hide();
                            HighScoreScene highScoreScene = new HighScoreScene(this, spriteBatch);
                            this.Components.Add(highScoreScene);
                            highScoreScene.show();
                            break;
                        case 3:
                            startScene.hide();
                            aboutScene.show();
                            break;
                        case 4:
                            Exit();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    DisableAllScenes();
                    startScene.show();
                }
            }
            base.Update(gameTime);
        }

        private void DisableAllScenes()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if(Components[i] is GameScene)
                {
                    GameScene scene = (GameScene)Components[i];
                    scene.Enabled = false;
                    scene.Visible = false;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
