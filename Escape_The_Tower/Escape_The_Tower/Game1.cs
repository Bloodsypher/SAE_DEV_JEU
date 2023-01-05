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
        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 900;
        private TiledMapTileLayer mapLayer;
        private int _sensPersoX;
        private int _sensPersoY;

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

            _vitessePerso = 100;

            _positionPerso = new Vector2(20, 240);

            _graphics.IsFullScreen = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tiledMap = Content.Load<TiledMap>("azer");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("collision");

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
          _perso = new AnimatedSprite(spriteSheet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
            _perso.Play("idle"); // une des animations définies dans « persoAnimation.sf »
            _perso.Update(deltaSeconds); // time écoulé

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            /*
            //DEPLACEMENT PERSO1
            _keyboardState = Keyboard.GetState();

            // si fleche droite
            if (_keyboardState.IsKeyDown(Keys.D) && !_keyboardState.IsKeyDown(Keys.Q))
            {
              _perso.Play("walkEast");
              
                _positionPerso.X = _positionPerso.X + _vitessePerso;
            }

            // si fleche gauchee
            if (_keyboardState.IsKeyDown(Keys.Q) && !_keyboardState.IsKeyDown(Keys.D))
            {
              _perso.Play("walkWest");
              
                _positionPerso.X = _positionPerso.X - _vitessePerso;
            }

            //si fleche bas
            if (_keyboardState.IsKeyDown(Keys.S) && !_keyboardState.IsKeyDown(Keys.Z))
            {
               _perso.Play("walkSouth");
               _perso.Update(deltaSeconds);
                _positionPerso.Y = _positionPerso.Y + _vitessePerso;
            }

            //si fleche haut
            if (_keyboardState.IsKeyDown(Keys.Z) && !_keyboardState.IsKeyDown(Keys.S))
            {
                _perso.Play("walkNorth");
               
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
            */
            _keyboardState = Keyboard.GetState();
            _sensPersoX = 0;
            _sensPersoY = 0;

            // si fleche droite enfoncé
            if (_keyboardState.IsKeyDown(Keys.D))
            {
                Console.WriteLine("dzd");
                ushort txMillieu = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort tyMillieu = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);

                ushort txHaut = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort tyHaut = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1);

                ushort txBas = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort tyBas = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 2);


                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = 1;
            }
            // si fleche gauche enfoncé
            if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)))
            {
                ushort txMillieu = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort tyMillieu = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);

                ushort txHaut = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort tyHaut = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1);

                ushort txBas = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort tyBas = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 2);

                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = -1;
            }

            // si fleche haut enfoncé
            if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)))
            {
                ushort txGauche = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort tyGauche = (ushort)((_positionPerso.Y + 10) / _tiledMap.TileHeight - 1);



                ushort txDroite = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort tyDroite = (ushort)((_positionPerso.Y + 10) / _tiledMap.TileHeight - 1);
                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = -1;
            }

            // si fleche bas enfoncé
            if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)))
            {
                ushort txGauche = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort tyGauche = (ushort)((_positionPerso.Y + 2) / _tiledMap.TileHeight + 2);

                ushort txDroite = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort tyDroite = (ushort)((_positionPerso.Y + 2) / _tiledMap.TileHeight + 2);

                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = 1;
            }

            // deplace le personnage
            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
          _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }

}