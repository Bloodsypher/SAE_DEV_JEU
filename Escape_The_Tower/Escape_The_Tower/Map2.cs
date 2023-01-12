﻿using Microsoft.Xna.Framework;
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

            //Perso
            PersoDroite._positionPerso = new Vector2(942, 705);
            PersoGauche._positionPerso = new Vector2(428, 700);

            PersoDroite.mapJoueur2 = _tiledMap2;
            PersoDroite.mapPlayer2 = mapLayerCollision2;
            PersoGauche.mapJoueur1 = _tiledMap2;
            PersoGauche.mapPlayer1 = mapLayerCollision2;


            base.LoadContent();
        }

        public override void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;

            rectPerso1 = new Rectangle((int)PersoGauche._positionPerso.X, (int)PersoGauche._positionPerso.Y, sprite_width, sprite_height);
            rectPerso2 = new Rectangle((int)PersoDroite._positionPerso.X, (int)PersoDroite._positionPerso.Y, sprite_width, sprite_height);



        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw();

            _myGame.SpriteBatch.Begin();

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