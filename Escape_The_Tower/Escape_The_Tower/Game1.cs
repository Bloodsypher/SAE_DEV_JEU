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

        //load des différents screen
        private readonly ScreenManager _screenManager;
        private readonly MenuDemarage _fondMenu;
        private Map1 _ScreenMap1;
        private Map2 _ScreenMap2;
        private Map3 _ScreenMap3;
        private MenuControle _ScreenControle;
        private readonly ecranFin _fondFin;

        private GraphicsDeviceManager _graphics;
        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 800;
        public enum Etats { Menu, Jouer, Controle, Quit, Map1, Map11, Map2, Map22, Map3, Map33, Fin, Fin2};

        // on définit un champ pour stocker l'état en cours du jeu
        private Etats etat;
        private MapTuto _ScreenMapTuto;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            _screenManager = new ScreenManager();
            _ScreenMapTuto = new MapTuto(this);
            _fondMenu = new MenuDemarage(this);
            _ScreenMap1 = new Map1(this);
            _ScreenControle = new MenuControle(this);
            _ScreenMap2 = new Map2(this);
            _ScreenMap3 = new Map3(this);
            _fondFin = new ecranFin(this);

            Components.Add(_screenManager);
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

            
            _graphics.PreferredBackBufferWidth = LONGUEUR_ECRAN;
            _graphics.PreferredBackBufferHeight = LARGEUR_ECRAN;


            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            PersoGauche.Initialize();
            PersoDroite.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            PersoGauche.LoadContent(this);
            PersoDroite.LoadContent(this);
            _screenManager.LoadScreen(_fondMenu, new FadeTransition(GraphicsDevice, Color.Black));

        }

        protected override void Update(GameTime gameTime)
        {
            PersoGauche.Update(gameTime);
            PersoDroite.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // On teste le clic de souris et l'état pour savoir quelle action faire 
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question


                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Jouer)
                    _screenManager.LoadScreen(_ScreenMapTuto, new FadeTransition(GraphicsDevice, Color.Black));

                else if (this.Etat == Etats.Controle)
                    _screenManager.LoadScreen(_ScreenControle, new FadeTransition(GraphicsDevice, Color.Black));

                else if (this.Etat == Etats.Menu)
                    _screenManager.LoadScreen(_fondMenu, new FadeTransition(GraphicsDevice, Color.Black));

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Delete))
            {
                if (this.Etat == Etats.Menu)
                    _screenManager.LoadScreen(_fondMenu, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                _screenManager.LoadScreen(_ScreenMapTuto, new FadeTransition(GraphicsDevice, Color.Black));
               
            }

            if (this.Etat == Etats.Map1 || Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                _screenManager.LoadScreen(_ScreenMap1, new FadeTransition(GraphicsDevice, Color.Black));
                 this.Etat = Etats.Map11;

            }

            if (this.Etat == Etats.Map2 || Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                _screenManager.LoadScreen(_ScreenMap2, new FadeTransition(GraphicsDevice, Color.Black));
                this.Etat = Etats.Map22;
            }

            if (this.Etat == Etats.Map3 || Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                _screenManager.LoadScreen(_ScreenMap3, new FadeTransition(GraphicsDevice, Color.Black));
                this.Etat = Etats.Map33;
            }

            if (this.Etat == Etats.Fin || Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                _screenManager.LoadScreen(_fondFin, new FadeTransition(GraphicsDevice, Color.Black));
                this.Etat = Etats.Fin2;
            }

            base.Update(gameTime);
            // TODO: Add your update logic here


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }

}