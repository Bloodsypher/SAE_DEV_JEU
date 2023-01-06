using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Escape_The_Tower
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager screenManager;
        private GraphicsDeviceManager _graphics;
        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 900;
        public enum Etats { Menu, Controls, Play, Quit };

        // on définit un champ pour stocker l'état en cours du jeu
        private Etats etat;
        private MapTuto _ScreenMapTuto;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _ScreenMapTuto = new MapTuto(this);
            screenManager = new ScreenManager();
            Components.Add(screenManager);
        }
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }
        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }



        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            //---------------------------
            //MODE PLEINE ECRAN
            //---------------------------

            //int w = graphics.DisplayMode.Width;
            //int h = graphics.DisplayMode.Height;

            _graphics.PreferredBackBufferWidth = LONGUEUR_ECRAN;
            _graphics.PreferredBackBufferHeight = LARGEUR_ECRAN;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager.LoadScreen(_ScreenMapTuto, new FadeTransition(GraphicsDevice, Color.Black));
          
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
            // TODO: Add your update logic here
           
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }

}