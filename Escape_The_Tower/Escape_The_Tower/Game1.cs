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
        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 900;
        private TiledMapTileLayer mapLayerCollision;
        private TiledMapTileLayer mapLayerEscalier;
        private TiledMapTileLayer mapLayerButton;
        private TiledMapTileLayer mapLayerPlaques;
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

            _positionPerso = new Vector2(LONGUEUR_ECRAN / 2 - 50, LARGEUR_ECRAN/2);

            _graphics.IsFullScreen = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tiledMap = Content.Load<TiledMap>("maptuto1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            //Définition des layers

            mapLayerCollision = _tiledMap.GetLayer<TiledMapTileLayer>("collision");
            mapLayerEscalier = _tiledMap.GetLayer<TiledMapTileLayer>("escalier");
            mapLayerButton = _tiledMap.GetLayer<TiledMapTileLayer>("button");
            mapLayerPlaques = _tiledMap.GetLayer<TiledMapTileLayer>("plaques");

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
          _perso = new AnimatedSprite(spriteSheet);
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);


            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            //DEPLACEMENT PERSO1
           
            _keyboardState = Keyboard.GetState();
            _sensPersoX = 0;
            _sensPersoY = 0;

            // si fleche D enfoncé
            if (_keyboardState.IsKeyDown(Keys.D) && !(_keyboardState.IsKeyDown(Keys.Q)))
            {
                
                ushort txMillieu = (ushort)(_positionPerso.X / _tiledMap.TileWidth +0.5);
                ushort tyMillieu = (ushort)(_positionPerso.Y / _tiledMap.TileHeight +0.5);

                ushort txHaut = (ushort)(_positionPerso.X / _tiledMap.TileWidth +0.5);
                ushort tyHaut = (ushort)(_positionPerso.Y / _tiledMap.TileHeight +0.5);

                ushort txBas = (ushort)(_positionPerso.X / _tiledMap.TileWidth +0.5);
                ushort tyBas = (ushort)(_positionPerso.Y / _tiledMap.TileHeight +0.5);


                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = 1;
            }
            // si fleche Q enfoncé
            if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)))
            {
                ushort txMillieu = (ushort)(_positionPerso.X / _tiledMap.TileWidth -0.4);
                ushort tyMillieu = (ushort)(_positionPerso.Y / _tiledMap.TileHeight -0.4);

                ushort txHaut = (ushort)(_positionPerso.X / _tiledMap.TileWidth -0.4);
                ushort tyHaut = (ushort)(_positionPerso.Y / _tiledMap.TileHeight -0.4);

                ushort txBas = (ushort)(_positionPerso.X / _tiledMap.TileWidth -0.4);
                ushort tyBas = (ushort)(_positionPerso.Y / _tiledMap.TileHeight -0.4);

                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = -1;
            }

            // si fleche Z enfoncé
            if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)))
            {
                ushort txGauche = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort tyGauche = (ushort)((_positionPerso.Y ) / _tiledMap.TileHeight - 0.5);



                ushort txDroite = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort tyDroite = (ushort)((_positionPerso.Y ) / _tiledMap.TileHeight - 0.5);

                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = -1;
            }

            // si fleche S enfoncé
            if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)))
            {
                ushort txGauche = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.8);
                ushort tyGauche = (ushort)((_positionPerso.Y ) / _tiledMap.TileHeight + 0.8);

                ushort txDroite = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.8);
                ushort tyDroite = (ushort)((_positionPerso.Y ) / _tiledMap.TileHeight + 0.8);

                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = 1;
            }

            // deplace le personnage
            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

            if (_sensPersoX == 0 && _sensPersoY == 0) _perso.Play("idle"); // une des animations définies dans « persoAnimation.sf »

            // si on bouge alors on play anim
           else if (_sensPersoX == 1 && _sensPersoY == 1 || _sensPersoX == -1 && _sensPersoY == 1 || _sensPersoX == 0 && _sensPersoY == 1) _perso.Play("walkSouth");
           else if (_sensPersoX == 1 && _sensPersoY == -1 || _sensPersoX == -1 && _sensPersoY == -1 || _sensPersoX == 0 && _sensPersoY == -1) _perso.Play("walkNorth");
           else if (_sensPersoX == -1 && _sensPersoY == 0) _perso.Play("walkWest");
           else if (_sensPersoX == 1 && _sensPersoY == 0) _perso.Play("walkEast");

            _perso.Update(deltaTime); // time écoulé
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
            Console.WriteLine(mapLayerCollision.GetTile(x, y).GlobalIdentifier);
            Console.WriteLine(mapLayerEscalier.GetTile(x, y).GlobalIdentifier);
            Console.WriteLine(mapLayerButton.GetTile(x, y).GlobalIdentifier);
            Console.WriteLine(mapLayerPlaques.GetTile(x, y).GlobalIdentifier);

            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayerCollision.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }

}