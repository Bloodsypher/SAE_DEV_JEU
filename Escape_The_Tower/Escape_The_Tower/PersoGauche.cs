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
    internal class PersoGauche
    {


        public static Vector2 _positionPerso;
        public static AnimatedSprite _perso1;
        public static KeyboardState _keyboardState;
        public static int _vitessePerso;
        public static int _sensPersoX;
        public static int _sensPersoY;

        public static int sprite_width;
        public static int sprite_height;

        public static TiledMapTileLayer mapPlayer1;
        public static TiledMap mapJoueur1;


        public static void Initialize()
        {
            //---------------------------
            //Initialise perso
            //---------------------------

            _positionPerso = new Vector2(600, 600);

            _vitessePerso = 100;


        }

        public static void LoadContent(Game game)
        {
            //définition des animation

            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            _perso1 = new AnimatedSprite(spriteSheet);

        }
        public static void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            _perso1.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            _sensPersoX = 0;
            _sensPersoY = 0;

            // si fleche D enfoncé
            if (_keyboardState.IsKeyDown(Keys.D))
            {

                ushort tx = (ushort)(_positionPerso.X / mapJoueur1.TileWidth + 0.7);
                ushort ty = (ushort)(_positionPerso.Y / mapJoueur1.TileHeight);


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = 1;
            }
            // si fleche Q enfoncé
            if (_keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / mapJoueur1.TileWidth - 0.6);
                ushort ty = (ushort)(_positionPerso.Y / mapJoueur1.TileHeight);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = -1;
            }

            // si fleche Z enfoncé
            if (_keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso.X / mapJoueur1.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y) / mapJoueur1.TileHeight - 0.7);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY = -1;
            }

            // si fleche S enfoncé
            if (_keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso.X / mapJoueur1.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y) / mapJoueur1.TileHeight + 0.5);


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY = 1;

            }

            // deplace le personnage
            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

            if (_sensPersoX == 0 && _sensPersoY == 0) _perso1.Play("idle"); // une des animations définies dans « persoAnimation.sf »

            // si on bouge alors on play anim
            else if (_sensPersoX == 1 && _sensPersoY == 1 || _sensPersoX == -1 && _sensPersoY == 1 || _sensPersoX == 0 && _sensPersoY == 1) _perso1.Play("walkSouth");
            else if (_sensPersoX == 1 && _sensPersoY == -1 || _sensPersoX == -1 && _sensPersoY == -1 || _sensPersoX == 0 && _sensPersoY == -1) _perso1.Play("walkNorth");
            else if (_sensPersoX == -1 && _sensPersoY == 0) _perso1.Play("walkWest");
            else if (_sensPersoX == 1 && _sensPersoY == 0) _perso1.Play("walkEast");

            _perso1.Update(deltaTime); // time écoulé


        }
        private static bool IsCollision(ushort x, ushort y)
        {


            //Console.WriteLine(mapLayerCollision.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerEscalier.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerButton.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerPlaques.GetTile(x, y).GlobalIdentifier);

            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapPlayer1.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(_perso1, _positionPerso);


        }

    }
}
