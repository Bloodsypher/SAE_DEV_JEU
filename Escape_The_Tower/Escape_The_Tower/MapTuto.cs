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
        public static Rectangle rectfeu;
        public static int sprite_width;
        public static int sprite_height;
        private GameTime gameTime;
        public static AnimatedSprite _feu;
        public static Vector2 _positionfeu;
        public static bool porteouverte = false;
        

        public MapTuto(Game1 myGame) : base(myGame)
        {
            this._myGame = myGame;
        }

        public override void Initialize()
        {
           


        }
        public override void LoadContent()
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

            //Initialise sprite obj
            _textutePorte = Content.Load<Texture2D>("porte1");
            _texturePlaque = Content.Load<Texture2D>("plaque_de_pression");
            _textutePorteOuverte = Content.Load<Texture2D>("porte4");
            SpriteSheet spriteSheetfeu = Content.Load<SpriteSheet>("fire.sf", new JsonContentLoader());

            //feu
            _feu = new AnimatedSprite(spriteSheetfeu);
            _positionfeu = new Vector2(595, 193);
            base.LoadContent();

        }
        public override void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            //posiiton obj
            _positionPorte = new Vector2(800, 487);
            _positionPlaque1 = new Vector2(512, 544);
            _positionPlaque2 = new Vector2(895, 544);
            rectPerso1 = new Rectangle((int)PersoGauche._positionPerso.X, (int)PersoGauche._positionPerso.Y, sprite_width, sprite_height);
            rectPlaque1 = new Rectangle((int)_positionPlaque1.X, (int)_positionPlaque1.Y, 32, 32);
            rectPerso2 = new Rectangle((int)PersoDroite._positionPerso.X, (int)PersoDroite._positionPerso.Y, sprite_width, sprite_height);
            rectPlaque2 = new Rectangle((int)_positionPlaque2.X, (int)_positionPlaque2.Y, 32, 32);
            rectfeu = new Rectangle((int)_positionfeu.X, (int)_positionfeu.Y, 48, 64);
            rectPorte = new Rectangle((int)_positionPorte.X, (int)_positionPorte.Y, 64, 32);



            if (Collision(rectPorte, rectPerso2))
            {
                PersoDroite._positionPerso.Y = PersoDroite._positionPerso.Y + 5;
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
                    PersoDroite._positionPerso.Y = PersoDroite._positionPerso.Y - 5;
                }
            }


            if (Collision(rectfeu, rectPerso1))
            {
                PersoGauche._positionPerso = new Vector2(600, 600);
            }

            _feu.Play("fire");
            _feu.Update(deltaTime);

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
