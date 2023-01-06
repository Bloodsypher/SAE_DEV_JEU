using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Timers;

namespace Escape_The_Tower
{
    internal class MapTuto
    {

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
        public static Vector2 _positionPlaque;
        public const int LONGUEUR_ECRAN = 1440;
        public const int LARGEUR_ECRAN = 900;
        public static Rectangle rectPlaque1;
        public static Rectangle rectPerso1;
        public static Vector2 _positionPerso;
        public static int sprite_width;
        public static int sprite_height;

        public static void Initialize()
        {
            _positionPerso = new Vector2(LONGUEUR_ECRAN / 2 - 50, LARGEUR_ECRAN / 2);
            //posiiton obj
            _positionPorte = new Vector2(LONGUEUR_ECRAN / 2 + 80, LARGEUR_ECRAN / 2 + 40);
            _positionPlaque = new Vector2(512, LARGEUR_ECRAN / 2 + 94);
            rectPerso1 = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, sprite_width, sprite_height);
            rectPlaque1= new Rectangle((int)_positionPlaque.X, (int)_positionPlaque.Y, 32, 32);

        }
        public static void LoadContent(Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tiledMap = game.Content.Load<TiledMap>("maptuto1");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            //Définition des layers

            mapLayerCollision = _tiledMap.GetLayer<TiledMapTileLayer>("collision");
            mapLayerEscalier = _tiledMap.GetLayer<TiledMapTileLayer>("escalier");
            mapLayerButton = _tiledMap.GetLayer<TiledMapTileLayer>("button");
            mapLayerPlaques = _tiledMap.GetLayer<TiledMapTileLayer>("plaques");
            //Initialise sprite obj
            _textutePorte = game.Content.Load<Texture2D>("porte1");
            _texturePlaque = game.Content.Load<Texture2D>("plaque_de_pression");
            _textutePorteOuverte = game.Content.Load<Texture2D>("porte4");

        }
        public static void Update(GameTime gametime)
        {
            

            if (Collision(rectPlaque1, rectPerso1))
            {
                _textutePorte = _textutePorteOuverte;

            }
         
        }
        public static void Draw(GameTime gameTime)
        {
         // TODO: Add your drawing code here

            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_textutePorte, _positionPorte, Color.White);
            _spriteBatch.Draw(_texturePlaque, _positionPlaque, Color.White);
            _spriteBatch.End();

        }
        public static bool Collision(Rectangle rectPlaque1, Rectangle rrectPerso1)
        {
            return rrectPerso1.Intersects(rectPlaque1);
        }
    }
}
