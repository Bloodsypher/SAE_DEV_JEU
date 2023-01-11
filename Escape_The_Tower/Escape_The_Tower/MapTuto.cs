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
    internal class MapTuto : GameScreen
    {
        private Game1 _myGame;
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static TiledMap _tiledMap;
        public static TiledMapRenderer _tiledMapRenderer;

        public static TiledMapTileLayer mapLayerCollision;
        public static TiledMapTileLayer mapLayerEscalier;
        public static TiledMapTileLayer mapLayerButton;
        public static TiledMapTileLayer mapLayerPlaques;
        public static Texture2D _textutePorte;
        public static Texture2D _textutePorteOuverte;
        public static Vector2 _positionPorte;
        public static Texture2D _texturePlaque;
        public static Vector2 _positionPlaque1;
        public static Vector2 _positionPlaque2;
        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 800;

        public static Rectangle rectPlaque1;
        public static Rectangle rectPorte;
        public static Rectangle rectPlaque2;
        public static Rectangle rectPerso1;
        public static Rectangle rectPerso2;

        public static int sprite_width;
        public static int sprite_height;
        private GameTime gameTime;

        public static Rectangle recttable;
        public static Rectangle recescalier1;
        public static Rectangle recescalier2;


        public static AnimatedSprite _feu;
        public static Vector2 _positionfeu;
        public static Vector2 _positionfeu2;
        public static Vector2 _positionfeu3;
        public static Vector2 _positionfeu4;
        public static Vector2 _positionfeu5;
        public static Vector2 _positionfeu6;
        public static Vector2 _positionfeu7;


        public static Rectangle rectfeu;
        public static Rectangle rectfeu2;
        public static Rectangle rectfeu3;
        public static Rectangle rectfeu4;
        public static Rectangle rectfeu5;
        public static Rectangle rectfeu6;
        public static Rectangle rectfeu7;

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

        //-------------------------------

        public static bool porteouverte = false;
        

        public MapTuto(Game1 myGame) : base(myGame)
        {
            this._myGame = myGame;
        }

        public override void Initialize()
        {
            _positionPerso1 = new Vector2(600, 600);

            _vitessePerso1 = 100;

            _positionPerso2 = new Vector2(830, 600);

            _vitessePerso2 = 100;



        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //définition des animation perso

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            _perso1 = new AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheet2 = Content.Load<SpriteSheet>("perso2.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheet2);

            // TODO: use this.Content to load your game content here
            _tiledMap = Content.Load<TiledMap>("maptuto1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            //Définition des layers

            mapLayerCollision = _tiledMap.GetLayer<TiledMapTileLayer>("collision");
            mapLayerEscalier = _tiledMap.GetLayer<TiledMapTileLayer>("escalier");
            mapLayerButton = _tiledMap.GetLayer<TiledMapTileLayer>("button");
            mapLayerPlaques = _tiledMap.GetLayer<TiledMapTileLayer>("plaques");

            //Initialise sprite obj
            _textutePorte = Content.Load<Texture2D>("porte1");
            _texturePlaque = Content.Load<Texture2D>("plaque_de_pression");
            _textutePorteOuverte = Content.Load<Texture2D>("porte4");
            SpriteSheet spriteSheetfeu = Content.Load<SpriteSheet>("fire.sf", new JsonContentLoader());


            //obj
            _positionPorte = new Vector2(800, 487);
            rectPorte = new Rectangle((int)_positionPorte.X, (int)_positionPorte.Y, 100, 32);

            _positionPlaque1 = new Vector2(512, 544);
            _positionPlaque2 = new Vector2(895, 544);

            rectPlaque1 = new Rectangle((int)_positionPlaque1.X, (int)_positionPlaque1.Y, 32, 32);
            rectPlaque2 = new Rectangle((int)_positionPlaque2.X, (int)_positionPlaque2.Y, 32, 32);

            //feu
            _feu = new AnimatedSprite(spriteSheetfeu);
           
            _positionfeu = new Vector2(595, 193);
            _positionfeu2 = new Vector2(595, 205);
            _positionfeu3 = new Vector2(595, 230);
            _positionfeu4 = new Vector2(600, 250);
            _positionfeu5 = new Vector2(625, 250);
            _positionfeu6 = new Vector2(655, 250);
            _positionfeu7 = new Vector2(685, 250);


            recescalier1 = new Rectangle(610, 193, 96, 64);
            recescalier2 = new Rectangle(730,193,96,64);

            recttable = new Rectangle(855, 305, 32, 64);
            
           


            base.LoadContent();

        }


        public override void Update(GameTime gametime)
        {

            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            //==================================================perso1=============================================
            _perso1.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            _sensPersoX1 = 0;
            _sensPersoY1 = 0;


            // si fleche D enfoncé
            if (_keyboardState.IsKeyDown(Keys.D))
            {

                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth + 0.7);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap.TileHeight);


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX1 = 1;
            }
            // si fleche Q enfoncé
            if (_keyboardState.IsKeyDown(Keys.Q))
            {

                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth - 0.6);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap.TileHeight);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX1 = -1;
            }

            // si fleche Z enfoncé
            if (_keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso1.Y) / _tiledMap.TileHeight - 0.7);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY1 = -1;
            }

            // si fleche S enfoncé
            if (_keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso1.Y) / _tiledMap.TileHeight + 0.5);

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
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap.TileWidth + 0.7);
                ushort ty = (ushort)(_positionPerso2.Y / _tiledMap.TileHeight);


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                {
                    _sensPersoX2 = 1;

                }
            }
            // si fleche fleche gauche enfoncé
            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap.TileWidth - 0.6);
                ushort ty = (ushort)(_positionPerso2.Y / _tiledMap.TileHeight);



                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                {
                    _sensPersoX2 = -1;

                }

            }

            // si fleche fleche haut enfoncé
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso2.Y) / _tiledMap.TileHeight - 0.7);



                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY2 = -1;

            }

            // si fleche bas enfoncé
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                ushort tx = (ushort)(_positionPerso2.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso2.Y) / _tiledMap.TileHeight + 0.5);



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



            rectfeu = new Rectangle((int)_positionfeu.X, (int)_positionfeu.Y, 48, 64);
            rectfeu2 = new Rectangle((int)_positionfeu2.X, (int)_positionfeu2.Y, 48, 64);
            rectfeu3 = new Rectangle((int)_positionfeu3.X, (int)_positionfeu3.Y, 48, 64);
            rectfeu4 = new Rectangle((int)_positionfeu4.X, (int)_positionfeu4.Y, 48, 64);
            rectfeu5 = new Rectangle((int)_positionfeu5.X, (int)_positionfeu5.Y, 48, 64);
            rectfeu6 = new Rectangle((int)_positionfeu6.X, (int)_positionfeu6.Y, 48, 64);
            rectfeu7 = new Rectangle((int)_positionfeu7.X, (int)_positionfeu7.Y, 48, 64);





            if (Collision(rectPorte, rectPerso2))
            {
                _positionPerso2.Y = _positionPerso2.Y + 5;
            }

            if (Collision(rectPlaque1, rectPerso1) && Collision(rectPlaque2, rectPerso2))
            {
                porteouverte = true;

                
            }
            if (porteouverte == true)
            {
                _textutePorte = _textutePorteOuverte;
                if (Collision(rectPorte, rectPerso2))
                {
                    _positionPerso2.Y = _positionPerso2.Y - 5;
                }
            }


            if (Collision(rectfeu, rectPerso1) || Collision(rectfeu2, rectPerso1) || Collision(rectfeu3, rectPerso1) || Collision(rectfeu4, rectPerso1) || Collision(rectfeu5, rectPerso1) || Collision(rectfeu6, rectPerso1) || Collision(rectfeu7, rectPerso1))
            {
                _positionPerso1 = new Vector2(600, 600);
            }


            if (Collision(recttable, rectPerso2) && Keyboard.GetState().IsKeyDown(Keys.RightControl))
            {
                _positionfeu = new Vector2(1000,1000);
                _positionfeu2 = new Vector2(1000, 1000);
                _positionfeu3 = new Vector2(1000, 1000);
                _positionfeu4 = new Vector2(1000, 1000);
                _positionfeu5 = new Vector2(1000, 1000);
                _positionfeu6 = new Vector2(1000, 1000);
                _positionfeu7 = new Vector2(1000, 1000);
               
            }

            if (Collision(recescalier1, rectPerso1) && Collision(recescalier2, rectPerso2))
            {
                //Console.WriteLine("test");
                _myGame.Etat = Game1.Etats.Map1;
            }

            _feu.Play("fire");
            _feu.Update(deltaTime);


            //if (_myGame.Etat == Game1.Etats.Map1)
            //{
            //    _tiledMap = Content.Load<TiledMap>("map1");

            //}
        }
        public override void Draw(GameTime gameTime)
        {
         // TODO: Add your drawing code here

            GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw();
            _myGame.SpriteBatch.Begin();

            _myGame.SpriteBatch.Draw(_textutePorte, _positionPorte, Color.White);
            _myGame.SpriteBatch.Draw(_texturePlaque, _positionPlaque1, Color.White);
            _myGame.SpriteBatch.Draw(_texturePlaque, _positionPlaque2, Color.White);

            _myGame.SpriteBatch.Draw(_feu, _positionfeu);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu2);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu3);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu4);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu5);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu6);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu7);


            _myGame.SpriteBatch.Draw(_perso1, _positionPerso1);
            _myGame.SpriteBatch.Draw(_perso2, _positionPerso2);


            _myGame.SpriteBatch.End();

        }
        public static bool Collision(Rectangle rectPlaque1, Rectangle rectPerso1)
        {
            return rectPerso1.Intersects(rectPlaque1);
            
        }

        private static bool IsCollision(ushort x, ushort y)
        {


            //Console.WriteLine(mapLayerCollision.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerEscalier.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerButton.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerPlaques.GetTile(x, y).GlobalIdentifier);

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
