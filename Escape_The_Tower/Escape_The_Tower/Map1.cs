using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Timers;

namespace Escape_The_Tower
{
    internal class Map1 : GameScreen
    {
        private Game1 _myGame;

        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static TiledMap _tiledMap2;
        public static TiledMapRenderer _tiledMapRenderer;
        public static TiledMapTileLayer mapLayerCollision2;

        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 800;

        public static Texture2D _textutePorte;
        public static Texture2D _textutePorteOuverte;

        public static Rectangle rectPerso1;
        public static Rectangle rectPerso2;
        public static int sprite_width;
        public static int sprite_height;

        //-----------Perso1-------------

        public static Vector2 _positionPerso1;
        public static AnimatedSprite _perso1;
        public static KeyboardState _keyboardState;
        public static int _vitessePerso1;
        public static int _sensPersoX1;
        public static int _sensPersoY1;

        public static int sprite_width1;
        public static int sprite_height1;

        //-----------Perso2-------------

        public static Vector2 _positionPerso2;
        public static AnimatedSprite _perso2;
        public static int _vitessePerso2;
        public static double _sensPersoX2;
        public static double _sensPersoY2;

        public static int sprite_width2;
        public static int sprite_height2;



        public Map1(Game1 myGame) : base(myGame)
        {
            this._myGame = myGame;
        }

        public override void Initialize()
        {
            _positionPerso1 = new Vector2(428, 700);

            _vitessePerso1 = 100;

            _positionPerso2 = new Vector2(950, 720);

            _vitessePerso2 = 100;


        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap2 = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap2);

            //définition des animation perso

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            _perso1 = new AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheet2 = Content.Load<SpriteSheet>("perso2.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheet2);

            //définition des layers 
            mapLayerCollision2 = _tiledMap2.GetLayer<TiledMapTileLayer>("collision1");

            //définition des textures obj
            _textutePorte = Content.Load<Texture2D>("porte1");
            _textutePorteOuverte = Content.Load<Texture2D>("porte4");

            

            

            base.LoadContent();

        }

        public override void Update(GameTime gametime)
        {



            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;

            //================================================== perso1 =============================================
            _perso1.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            _sensPersoX1 = 0;
            _sensPersoY1 = 0;


            // si fleche D enfoncé
            if (_keyboardState.IsKeyDown(Keys.D))
            {

                ushort tx = (ushort)(_positionPerso1.X / _tiledMap2.TileWidth + 0.7);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap2.TileHeight);


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX1 = 1;
            }
            // si fleche Q enfoncé
            if (_keyboardState.IsKeyDown(Keys.Q))
            {

                ushort tx = (ushort)(_positionPerso1.X / _tiledMap2.TileWidth - 0.6);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap2.TileHeight);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX1 = -1;
            }

            // si fleche Z enfoncé
            if (_keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap2.TileWidth);
                ushort ty = (ushort)((_positionPerso1.Y) / _tiledMap2.TileHeight - 0.7);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY1 = -1;
            }

            // si fleche S enfoncé
            if (_keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap2.TileWidth);
                ushort ty = (ushort)((_positionPerso1.Y) / _tiledMap2.TileHeight + 0.5);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY1 = 1;

            }

            // deplace le personnage
            _positionPerso1.X += _sensPersoX1 * _vitessePerso1 * deltaTime;
            _positionPerso1.Y += _sensPersoY1 * _vitessePerso1 * deltaTime;

            if (_sensPersoX1 == 0 && _sensPersoY1 == 0) _perso1.Play("idle"); // une des animations définies dans « persoAnimation.sf »

            // si on bouge alors on play anim
            else if (_sensPersoX1 == 1 && _sensPersoY1 == 1 || _sensPersoX1 == -1 && _sensPersoY1 == 1 || _sensPersoX1 == 0 && _sensPersoY1 == 1) _perso1.Play("walkSouth");
            else if (_sensPersoX1 == 1 && _sensPersoY1 == -1 || _sensPersoX1 == -1 && _sensPersoY1 == -1 || _sensPersoX1 == 0 && _sensPersoY1 == -1) _perso1.Play("walkNorth");
            else if (_sensPersoX1 == -1 && _sensPersoY1 == 0) _perso1.Play("walkWest");
            else if (_sensPersoX1 == 1 && _sensPersoY1 == 0) _perso1.Play("walkEast");

            _perso1.Update(deltaTime); // time écoulé


            //=========================================================perso2================================================================
            _perso2.Update(deltaTime);
            _sensPersoX2 = 0;
            _sensPersoY2 = 0;

            //-------Deplacement--------

            // si fleche fleche droite enfoncé
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap2.TileWidth + 0.7);
                ushort ty = (ushort)(_positionPerso2.Y / _tiledMap2.TileHeight);


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                {
                    _sensPersoX2 = 1;

                }
            }
            // si fleche fleche gauche enfoncé
            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap2.TileWidth - 0.6);
                ushort ty = (ushort)(_positionPerso2.Y / _tiledMap2.TileHeight);



                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                {
                    _sensPersoX2 = -1;

                }

            }

            // si fleche fleche haut enfoncé
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap2.TileWidth);
                ushort ty = (ushort)((_positionPerso2.Y) / _tiledMap2.TileHeight - 0.7);



                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY2 = -1;

            }

            // si fleche bas enfoncé
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap2.TileWidth);
                ushort ty = (ushort)((_positionPerso2.Y) / _tiledMap2.TileHeight + 0.5);



                if (!IsCollision(tx, ty))
                    _sensPersoY2 = 1;


            }



            // deplace le personnage
            _positionPerso2.X += (float)_sensPersoX2 * _vitessePerso2 * deltaTime;
            _positionPerso2.Y += (float)_sensPersoY2 * _vitessePerso2 * deltaTime;

            if (_sensPersoX2 == 0 && _sensPersoY2 == 0) _perso2.Play("pause"); // une des animations définies dans « persoAnimation.sf »

            // si on bouge alors on play anim
            else if (_sensPersoX2 == 1 && _sensPersoY2 == 1 || _sensPersoX2 == -1 && _sensPersoY2 == 1 || _sensPersoX2 == 0 && _sensPersoY2 == 1) _perso2.Play("marcheB");
            else if (_sensPersoX2 == 1 && _sensPersoY2 == -1 || _sensPersoX2 == -1 && _sensPersoY2 == -1 || _sensPersoX2 == 0 && _sensPersoY2 == -1) _perso2.Play("marcheH");
            else if (_sensPersoX2 == -1 && _sensPersoY2 == 0) _perso2.Play("marcheG");
            else if (_sensPersoX2 == 1 && _sensPersoY2 == 0) _perso2.Play("marcheD");

            _perso2.Update(deltaTime); // time écoulé



            //rectangle perso
            rectPerso1 = new Rectangle((int)_positionPerso1.X, (int)_positionPerso1.Y, sprite_width, sprite_height);
            rectPerso2 = new Rectangle((int)_positionPerso2.X, (int)_positionPerso2.Y, sprite_width, sprite_height);

            //=====================================================================================================

         

           


        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw();

            _myGame.SpriteBatch.Begin();

            _myGame.SpriteBatch.Draw(_perso1, _positionPerso1);
            _myGame.SpriteBatch.Draw(_perso2, _positionPerso2);


            _myGame.SpriteBatch.End();


        }

        private static bool IsCollision(ushort x, ushort y)
        {


            //Console.WriteLine(mapLayerCollision.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerEscalier.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerButton.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerPlaques.GetTile(x, y).GlobalIdentifier);

            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayerCollision2.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }

}
