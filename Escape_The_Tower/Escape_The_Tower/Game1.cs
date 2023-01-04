using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Escape_The_Tower
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private KeyboardState _keyboardState;
        private int _vitessePerso;
        private float deltaSeconds;
        public const int LONGUEUR_ECRAN = 1400;
        public const int LARGEUR_ECRAN = 800;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = LONGUEUR_ECRAN;
            _graphics.PreferredBackBufferHeight = LARGEUR_ECRAN;
            _graphics.ApplyChanges();

            _vitessePerso = 2;

            _positionPerso = new Vector2(20, 240);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tiledMap = Content.Load<TiledMap>("labo");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
    //        SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
          //  _perso = new AnimatedSprite(spriteSheet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
          //  _perso.Play("idle"); // une des animations définies dans « persoAnimation.sf »
         //   _perso.Update(deltaSeconds); // time écoulé



            //DEPLACEMENT PERSO1
            _keyboardState = Keyboard.GetState();

            // si fleche droite
            if (_keyboardState.IsKeyDown(Keys.D) && !_keyboardState.IsKeyDown(Keys.Q))
            {
             //   _perso.Play("walkEast");
               // _perso.Update(deltaSeconds); // time écoulé
                _positionPerso.X = _positionPerso.X + _vitessePerso;
            }

            // si fleche gauchee
            if (_keyboardState.IsKeyDown(Keys.Q) && !_keyboardState.IsKeyDown(Keys.D))
            {
              //  _perso.Play("walkWest");
              //  _perso.Update(deltaSeconds);
                _positionPerso.X = _positionPerso.X - _vitessePerso;
            }

            //si fleche bas
            if (_keyboardState.IsKeyDown(Keys.S) && !_keyboardState.IsKeyDown(Keys.Z))
            {
               // _perso.Play("walkSouth");
              //  _perso.Update(deltaSeconds);
                _positionPerso.Y = _positionPerso.Y + _vitessePerso;
            }

            //si fleche haut
            if (_keyboardState.IsKeyDown(Keys.Z) && !_keyboardState.IsKeyDown(Keys.S))
            {
                //_perso.Play("walkNorth");
               // _perso.Update(deltaSeconds);
                _positionPerso.Y = _positionPerso.Y - _vitessePerso;
            }

            //combinaison de touche pour pas bouger
            if (_keyboardState.IsKeyDown(Keys.Z) && _keyboardState.IsKeyDown(Keys.D))
            {
                _positionPerso.Y = _positionPerso.Y + _vitessePerso;
                _positionPerso.X = _positionPerso.X - _vitessePerso;
            }

            if (_keyboardState.IsKeyDown(Keys.Z) && _keyboardState.IsKeyDown(Keys.Q))
            {
                _positionPerso.Y = _positionPerso.Y + _vitessePerso;
                _positionPerso.X = _positionPerso.X + _vitessePerso;
            }

            if (_keyboardState.IsKeyDown(Keys.S) && _keyboardState.IsKeyDown(Keys.Q))
            {
                _positionPerso.Y = _positionPerso.Y - _vitessePerso;
                _positionPerso.X = _positionPerso.X + _vitessePerso;
            }

            if (_keyboardState.IsKeyDown(Keys.S) && _keyboardState.IsKeyDown(Keys.D))
            {
                _positionPerso.Y = _positionPerso.Y - _vitessePerso;
                _positionPerso.X = _positionPerso.X - _vitessePerso;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
          //  _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }

}