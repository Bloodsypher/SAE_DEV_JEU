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
using System.Security;
using MonoGame.Extended.TextureAtlases;

namespace Escape_The_Tower
{
    internal class Map2 : GameScreen
    {

        private Game1 _myGame;
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static TiledMap _tiledMap2;
        public static TiledMapRenderer _tiledMapRenderer;
        public static TiledMapTileLayer mapLayerCollision2;

        public static int sprite_width;
        public static int sprite_height;

        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 800;

        public static Rectangle rectPerso1;
        public static Rectangle rectPerso2;

        public static Texture2D _texturePorte;
        public static Texture2D _texturePorteOuverte;
        public static Texture2D _texturePorteOuverte2;
        public static Texture2D _texturePorte2;


        public static Vector2 _positionPorte;
        public static Rectangle rectPorte;
        public static Vector2 _positionPorte2;
        public static Rectangle rectPorte2;

        public static Rectangle recttable;

        public static bool porteouverte = false;

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

        public static Texture2D _texturePlaque;
        public static Vector2 _positionPlaque1;
        public static Vector2 _positionPlaque2;
        public static Rectangle rectPlaque1;
        public static Rectangle rectPlaque2;


        public Map2(Game1 myGame) : base(myGame)
        {
            this._myGame = myGame;
        }

        public override void Initialize()
        {



        }

        public override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap2 = Content.Load<TiledMap>("Map2LF");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap2);

            mapLayerCollision2 = _tiledMap2.GetLayer<TiledMapTileLayer>("Mur2");

            _texturePlaque = Content.Load<Texture2D>("plaque_de_pression");
            _positionPlaque1 = new Vector2(1024, 224);
            _positionPlaque2 = new Vector2(1055, 545);
            rectPlaque1 = new Rectangle((int)_positionPlaque1.X, (int)_positionPlaque1.Y, 32, 32);
            rectPlaque1 = new Rectangle((int)_positionPlaque2.X, (int)_positionPlaque2.Y, 32, 32);

            //Perso
            PersoDroite._positionPerso = new Vector2(1200, 140);
            PersoGauche._positionPerso = new Vector2(200, 610);

            PersoDroite.mapJoueur2 = _tiledMap2;
            PersoDroite.mapPlayer2 = mapLayerCollision2;
            PersoGauche.mapJoueur1 = _tiledMap2;
            PersoGauche.mapPlayer1 = mapLayerCollision2;

            //obj

            _texturePorte = Content.Load<Texture2D>("porte1Vertical");
            _texturePorteOuverte = Content.Load<Texture2D>("porte4Vertical");
            _texturePorteOuverte2 = Content.Load<Texture2D>("porte4Vertical");
            _texturePorte2 = Content.Load<Texture2D>("porte1Vertical");

            _positionPorte = new Vector2(1000, 97);
            rectPorte = new Rectangle((int)_positionPorte.X, (int)_positionPorte.Y, 32, 64);
            _positionPorte2 = new Vector2(230, 352);
            rectPorte2 = new Rectangle((int)_positionPorte2.X, (int)_positionPorte2.Y, 32, 64);

            recttable = new Rectangle(1150, 190, 64, 32);

            SpriteSheet spriteSheetfeu = Content.Load<SpriteSheet>("fire.sf", new JsonContentLoader());

            _feu = new AnimatedSprite(spriteSheetfeu);
            _positionfeu = new Vector2(250, 610);
            _positionfeu2 = new Vector2(250, 640);
            _positionfeu3 = new Vector2(250, 580);
            _positionfeu4 = new Vector2(250, 550);
            _positionfeu5 = new Vector2(210, 540);
            _positionfeu6 = new Vector2(180, 540);
            _positionfeu7 = new Vector2(140, 540);

            base.LoadContent();
        }

        public override void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;

            rectfeu = new Rectangle((int)_positionfeu.X, (int)_positionfeu.Y, 48, 64);
            rectfeu2 = new Rectangle((int)_positionfeu2.X, (int)_positionfeu2.Y, 48, 64);
            rectfeu3 = new Rectangle((int)_positionfeu3.X, (int)_positionfeu3.Y, 48, 64);
            rectfeu4 = new Rectangle((int)_positionfeu4.X, (int)_positionfeu4.Y, 48, 64);
            rectfeu5 = new Rectangle((int)_positionfeu5.X, (int)_positionfeu5.Y, 48, 64);
            rectfeu6 = new Rectangle((int)_positionfeu6.X, (int)_positionfeu6.Y, 48, 64);
            rectfeu7 = new Rectangle((int)_positionfeu7.X, (int)_positionfeu7.Y, 48, 64);

            rectPerso1 = new Rectangle((int)PersoGauche._positionPerso.X, (int)PersoGauche._positionPerso.Y, sprite_width, sprite_height);
            rectPerso2 = new Rectangle((int)PersoDroite._positionPerso.X, (int)PersoDroite._positionPerso.Y, sprite_width, sprite_height);


            if (Collision(rectPorte, rectPerso2) && porteouverte == false)
            {
                PersoDroite._positionPerso.X = PersoDroite._positionPerso.X +5;
            }

            if (Collision(recttable, rectPerso2) && Keyboard.GetState().IsKeyDown(Keys.RightControl))
            {
                //porteouverte = true;
                _positionfeu = new Vector2(1000, 1000);
                _positionfeu2 = new Vector2(1000, 1000);
                _positionfeu3 = new Vector2(1000, 1000);
                _positionfeu4 = new Vector2(1000, 1000);
                _positionfeu5 = new Vector2(1000, 1000);
                _positionfeu6 = new Vector2(1000, 1000);
                _positionfeu7 = new Vector2(1000, 1000);

            }

            if (porteouverte == true)
            {
                _texturePorte = _texturePorteOuverte;
               
            }

            if (Collision(rectfeu, rectPerso1) || Collision(rectfeu2, rectPerso1) || Collision(rectfeu3, rectPerso1) || Collision(rectfeu4, rectPerso1) || Collision(rectfeu5, rectPerso1) || Collision(rectfeu6, rectPerso1) || Collision(rectfeu7, rectPerso1))
            {
                PersoGauche._positionPerso = new Vector2(200, 610);
            }



            if (Collision(rectPlaque1, rectPerso2))
            {
                porteouverte = true;
                Console.WriteLine("test");
            }




            _feu.Play("fire");
            _feu.Update(deltaTime);

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw();

            _myGame.SpriteBatch.Begin();


            _myGame.SpriteBatch.Draw(_texturePorte, _positionPorte, Color.White);
            _myGame.SpriteBatch.Draw(_texturePorte2, _positionPorte2, Color.White);

            _myGame.SpriteBatch.Draw(_feu, _positionfeu);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu2);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu3);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu4);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu5);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu6);
            _myGame.SpriteBatch.Draw(_feu, _positionfeu7);

            _myGame.SpriteBatch.Draw(_texturePlaque, _positionPlaque1, Color.White);
            _myGame.SpriteBatch.Draw(_texturePlaque, _positionPlaque2, Color.White);

            PersoGauche.Draw(_myGame.SpriteBatch);
            PersoDroite.Draw(_myGame.SpriteBatch);

            _myGame.SpriteBatch.End();


        }

        public static bool Collision(Rectangle rectPlaque1, Rectangle rectPerso1)
        {
            return rectPerso1.Intersects(rectPlaque1);

        }
    }
}
