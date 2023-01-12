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

namespace Escape_The_Tower
{
    internal class Map3 : GameScreen
    {

        private Game1 _myGame;
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static TiledMap _tiledMap3;
        public static TiledMapRenderer _tiledMapRenderer;
        public static TiledMapTileLayer mapLayerCollision3;

        

        public static int sprite_width;
        public static int sprite_height;
        public static Rectangle rectPerso1;
        public static Rectangle rectPerso2;

        //porte
        public static Texture2D _texturePorte;
        public static Texture2D _texturePorteOuverte;
        public static Texture2D _texturePorteOuverte2;
        public static Texture2D _texturePorteOuverte3;

        public static Texture2D _texturePorte2;
        public static Texture2D _texturePorte3;
        public static Vector2 _positionPorte3;
        public static Rectangle rectPorte3;


        public static Vector2 _positionPorte;
        public static Rectangle rectPorte;
        public static Vector2 _positionPorte2;
        public static Rectangle rectPorte2;

        //plaque
        public static Texture2D _texturePlaque;
        public static Vector2 _positionPlaque1;
        public static Vector2 _positionPlaque2;
        public static Rectangle rectPlaque1;
        public static Rectangle rectPlaque2;



        public static bool porteouverte = false;
        public static bool porteouverte2 = false;
        public static bool porteouverte3 = false;

        public static bool plaque = false;

        public Map3(Game1 myGame) : base(myGame)
        {
            this._myGame = myGame;
        }

        public override void Initialize()
        {



        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap3 = Content.Load<TiledMap>("MapFinal");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap3);

            mapLayerCollision3 = _tiledMap3.GetLayer<TiledMapTileLayer>("MurFinal");

            //obj
            _texturePorte = Content.Load<Texture2D>("porte1");
            _texturePorteOuverte = Content.Load<Texture2D>("porte4");
            _texturePorteOuverte2 = Content.Load<Texture2D>("porte4");
            _texturePorte2 = Content.Load<Texture2D>("porte1");
            _texturePorte3 = Content.Load<Texture2D>("porte1");
            _texturePorteOuverte3 = Content.Load<Texture2D>("porte4");


            _positionPorte = new Vector2(192, 425);
            rectPorte = new Rectangle((int)_positionPorte.X, (int)_positionPorte.Y, 64, 32);
            _positionPorte2 = new Vector2(1184, 425);
            rectPorte2 = new Rectangle((int)_positionPorte2.X, (int)_positionPorte2.Y, 64, 32);
            _positionPorte3 = new Vector2(705, 200);
            rectPorte3 = new Rectangle((int)_positionPorte3.X, (int)_positionPorte3.Y, 64, 32);

            _texturePlaque = Content.Load<Texture2D>("plaque_de_pression");
            _positionPlaque2 = new Vector2(1248, 320);
            _positionPlaque1 = new Vector2(128, 608);
            rectPlaque1 = new Rectangle((int)_positionPlaque1.X, (int)_positionPlaque1.Y, 32, 32);
            rectPlaque2 = new Rectangle((int)_positionPlaque2.X, (int)_positionPlaque2.Y, 32, 32);

            //perso
            PersoDroite._positionPerso = new Vector2(1200, 640);
            PersoGauche._positionPerso = new Vector2(235, 640);

            PersoDroite.mapJoueur2 = _tiledMap3;
            PersoDroite.mapPlayer2 = mapLayerCollision3;
            PersoGauche.mapJoueur1 = _tiledMap3;
            PersoGauche.mapPlayer1 = mapLayerCollision3;


            base.LoadContent();
        }

        public override void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;


            rectPerso1 = new Rectangle((int)PersoGauche._positionPerso.X, (int)PersoGauche._positionPerso.Y, sprite_width, sprite_height);
            rectPerso2 = new Rectangle((int)PersoDroite._positionPerso.X, (int)PersoDroite._positionPerso.Y, sprite_width, sprite_height);

            //porte

            if (Collision(rectPorte2, rectPerso2) && porteouverte == false)
            {
                PersoDroite._positionPerso.Y = PersoDroite._positionPerso.Y + 5;
            }

            if (Collision(rectPorte, rectPerso1) && porteouverte2 == false)
            {
                PersoGauche._positionPerso.Y = PersoGauche._positionPerso.Y + 5;
            }

            if (Collision(rectPorte3, rectPerso1) && porteouverte3 == false )
            {
                PersoGauche._positionPerso.Y = PersoGauche._positionPerso.Y + 5;
            }

            if (Collision(rectPorte3, rectPerso2) && porteouverte3 == false)
            {
                PersoDroite._positionPerso.Y = PersoDroite._positionPerso.Y + 5;
            }

            if (Collision(rectPlaque1, rectPerso1 ))
            {
                porteouverte2 = true;
            }

            if (porteouverte2 == true)
            {
                _texturePorte2 = _texturePorteOuverte2;
                if (Collision(rectPorte2, rectPerso2))
                {
                    PersoDroite._positionPerso.Y = PersoDroite._positionPerso.Y - 5;
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw();

            _myGame.SpriteBatch.Begin();

            _myGame.SpriteBatch.Draw(_texturePorte, _positionPorte, Color.White);
            _myGame.SpriteBatch.Draw(_texturePorte2, _positionPorte2, Color.White);
            _myGame.SpriteBatch.Draw(_texturePorte3, _positionPorte3, Color.White);

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
