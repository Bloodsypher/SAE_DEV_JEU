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
    internal class Map1 : GameScreen
    {
        private Game1 _myGame;

        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static TiledMap _tiledMap1;
        public static TiledMapRenderer _tiledMapRenderer;
        public static TiledMapTileLayer mapLayerCollision1;

        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 800;

        public static Texture2D _textutePorte;
        public static Texture2D _textutePorteOuverte;
        public static Vector2 _positionPorte;
        public static Rectangle rectPorte;

        public static Rectangle recttable;


        public static Rectangle rectPerso1;
        public static Rectangle rectPerso2;
        public static int sprite_width;
        public static int sprite_height;


        public static bool porteouverte = false;

        public static Texture2D _texturePlaque;
        public static Vector2 _positionPlaque1;
        public static Vector2 _positionPlaque2;

        public static Rectangle recescalier1;
        public static Rectangle recescalier2;

        public static Rectangle mort1;
        public static Rectangle mort2;
        public static Rectangle mort3;
        public static Rectangle mort4;
        public static Rectangle mort5;
        public static Rectangle mort6;
        public static Rectangle mort7;
        public static Rectangle mort8;
        public static Rectangle mort9;

        public Map1(Game1 myGame) : base(myGame)
        {
            this._myGame = myGame;
        }

        public override void Initialize()
        {



        }

        public override void LoadContent()
        {
            _texturePlaque = Content.Load<Texture2D>("plaque_de_pression");
            _positionPlaque1 = new Vector2(1120, 290);
            _positionPlaque2 = new Vector2(1055, 545);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap1 = Content.Load<TiledMap>("map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap1);

            //définition des layers 
            mapLayerCollision1 = _tiledMap1.GetLayer<TiledMapTileLayer>("collision1");

            //définition des obj
            _textutePorte = Content.Load<Texture2D>("porte1");
            _textutePorteOuverte = Content.Load<Texture2D>("porte4");
            _positionPorte = new Vector2(223, 190);
            rectPorte = new Rectangle((int)_positionPorte.X, (int)_positionPorte.Y, 64, 32);

            recttable = new Rectangle(730, 590, 70, 45);


            PersoDroite._positionPerso = new Vector2(942, 705);
            PersoGauche._positionPerso = new Vector2(428, 700);

            PersoDroite.mapJoueur2   = _tiledMap1;
            PersoDroite.mapPlayer2 = mapLayerCollision1;
            PersoGauche.mapJoueur1 = _tiledMap1;
            PersoGauche.mapPlayer1 = mapLayerCollision1;

            mort1 = new Rectangle(832, 704, 32, 32);
            mort2 = new Rectangle(1055, 704, 32 ,32);
            mort3 = new Rectangle(1055, 545, 32, 32);
            mort4 = new Rectangle(1025, 416, 32, 32);
            mort5 = new Rectangle(735, 510, 32, 32);
            mort6 = new Rectangle(895, 255, 32, 32);
            mort7 = new Rectangle(958, 255, 32, 32);
            mort8 = new Rectangle(1023, 290, 32, 32);
            mort9 = new Rectangle(1120, 290, 32, 32);

            recescalier1 = new Rectangle(200, 110, 64, 64);
            recescalier2 = new Rectangle(1100, 110, 64, 64); 

            base.LoadContent();

        }

        public override void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;


            rectPerso1 = new Rectangle((int)PersoGauche._positionPerso.X, (int)PersoGauche._positionPerso.Y, sprite_width, sprite_height);
            rectPerso2 = new Rectangle((int)PersoDroite._positionPerso.X, (int)PersoDroite._positionPerso.Y, sprite_width, sprite_height);

            if (Collision(mort1, rectPerso2) || Collision(mort2, rectPerso2) || Collision(mort3, rectPerso2) || Collision(mort4, rectPerso2) || Collision(mort5, rectPerso2) || Collision(mort6,rectPerso2) || Collision(mort7,rectPerso2) || Collision(mort8, rectPerso2) || Collision(mort9,rectPerso2))
                PersoDroite._positionPerso = new Vector2(942, 705);

            if (Collision(rectPorte, rectPerso1) && !porteouverte)
            {
                PersoGauche._positionPerso.Y = PersoGauche._positionPerso.Y + 5;
            }


            if (Collision(recttable, rectPerso2) && Keyboard.GetState().IsKeyDown(Keys.RightControl))
            {
                porteouverte = true;

            }

            if (porteouverte)
            {
                _textutePorte = _textutePorteOuverte;
                if (Collision(rectPorte, rectPerso2))
                {
                    PersoDroite._positionPerso.Y = PersoDroite._positionPerso.Y - 5;
                }
            }

            if (Collision(recescalier1, rectPerso1) && Collision(recescalier2, rectPerso2))
            {
               
                _myGame.Etat = Game1.Etats.Map2;
            }


            //Console.WriteLine(PersoDroite._positionPerso.X + " " + PersoDroite._positionPerso.Y);
            //PersoDroite._perso2.Update(deltaTime);



        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw();

            _myGame.SpriteBatch.Begin();

            _myGame.SpriteBatch.Draw(_texturePlaque, _positionPlaque1, Color.White);
            _myGame.SpriteBatch.Draw(_texturePlaque, _positionPlaque2, Color.White);

            _myGame.SpriteBatch.Draw(_textutePorte, _positionPorte, Color.White);


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
