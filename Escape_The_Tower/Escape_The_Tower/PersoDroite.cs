using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Screens;

namespace Escape_The_Tower
{
    internal class PersoDroite
    {


        public static Vector2 _positionPerso;
        public static AnimatedSprite _perso2;
        public static KeyboardState _keyboardState;
        public static int _vitessePerso;
        public static int _sensPersoX;
        public static int _sensPersoY;

        public static int sprite_width;
        public static int sprite_height;



        public static void Initialize()
        {
            //---------------------------
            //Initialise perso
            //---------------------------

            _positionPerso = new Vector2(830, 600);

            _vitessePerso = 100;


        }

        public static void LoadContent(Game game)
        {
            //définition des animation

            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheet);

        }
        public static void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            _perso2.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            _sensPersoX = 0;
            _sensPersoY = 0;

            // si fleche D enfoncé
            if (_keyboardState.IsKeyDown(Keys.Right))
            {

                ushort txMillieu = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth + 0.5);
                ushort tyMillieu = (ushort)(_positionPerso.Y / MapTuto._tiledMap.TileHeight + 1.5);

                ushort txHaut = (ushort)(_positionPerso.X + sprite_width / 2 / MapTuto._tiledMap.TileWidth + 0.5);
                ushort tyHaut = (ushort)(_positionPerso.Y + sprite_height / 2 / MapTuto._tiledMap.TileHeight + 1.5);

                ushort txBas = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth + 0.5);
                ushort tyBas = (ushort)(_positionPerso.Y / MapTuto._tiledMap.TileHeight + 1.5);


                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = 1;
            }
            // si fleche Q enfoncé
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                ushort txMillieu = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth - 0.4);
                ushort tyMillieu = (ushort)(_positionPerso.Y / MapTuto._tiledMap.TileHeight - 0.4);

                ushort txHaut = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth - 0.4);
                ushort tyHaut = (ushort)(_positionPerso.Y / MapTuto._tiledMap.TileHeight - 0.4);

                ushort txBas = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth - 0.4);
                ushort tyBas = (ushort)(_positionPerso.Y / MapTuto._tiledMap.TileHeight - 0.4);

                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = -1;
            }

            // si fleche Z enfoncé
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                ushort txGauche = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth - 0.5);
                ushort tyGauche = (ushort)((_positionPerso.Y) / MapTuto._tiledMap.TileHeight - 0.5);



                ushort txDroite = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth - 0.5);
                ushort tyDroite = (ushort)((_positionPerso.Y) / MapTuto._tiledMap.TileHeight - 0.5);

                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = -1;
            }

            // si fleche S enfoncé
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                ushort txGauche = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth + 0.8);
                ushort tyGauche = (ushort)((_positionPerso.Y) / MapTuto._tiledMap.TileHeight + 0.8);

                ushort txDroite = (ushort)(_positionPerso.X / MapTuto._tiledMap.TileWidth + 0.8);
                ushort tyDroite = (ushort)((_positionPerso.Y) / MapTuto._tiledMap.TileHeight + 0.8);

                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = 1;

            }

            // deplace le personnage
            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

            if (_sensPersoX == 0 && _sensPersoY == 0) _perso2.Play("idle"); // une des animations définies dans « persoAnimation.sf »

            // si on bouge alors on play anim
            else if (_sensPersoX == 1 && _sensPersoY == 1 || _sensPersoX == -1 && _sensPersoY == 1 || _sensPersoX == 0 && _sensPersoY == 1) _perso2.Play("walkSouth");
            else if (_sensPersoX == 1 && _sensPersoY == -1 || _sensPersoX == -1 && _sensPersoY == -1 || _sensPersoX == 0 && _sensPersoY == -1) _perso2.Play("walkNorth");
            else if (_sensPersoX == -1 && _sensPersoY == 0) _perso2.Play("walkWest");
            else if (_sensPersoX == 1 && _sensPersoY == 0) _perso2.Play("walkEast");

            _perso2.Update(deltaTime); // time écoulé


        }
        private static bool IsCollision(ushort x, ushort y)
        {


            //Console.WriteLine(mapLayerCollision.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerEscalier.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerButton.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerPlaques.GetTile(x, y).GlobalIdentifier);

            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (MapTuto.mapLayerCollision.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(_perso2, _positionPerso);


        }

    }
}
