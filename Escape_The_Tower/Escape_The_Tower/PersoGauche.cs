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
        

        public static Vector2 _positionPerso1;
        public static AnimatedSprite _perso1;
        public static KeyboardState _keyboardState;
        public static int _vitessePerso1;
        public static int _sensPersoX1;
        public static int _sensPersoY1;
        
        public static int sprite_width1;
        public static int sprite_height1;
        public static TiledMapTileLayer mapJoueur;
        public static TiledMap mapPlayer;


        public static void Initialize()
        {
            //---------------------------
            //Initialise perso
            //---------------------------

            _positionPerso1 = new Vector2(600, 600);

            _vitessePerso1 = 100;

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
            _sensPersoX1 = 0;
            _sensPersoY1 = 0;

            // si fleche D enfoncé
            if (_keyboardState.IsKeyDown(Keys.D))
            {

                ushort tx = (ushort)(_positionPerso1.X / mapPlayer.TileWidth + 0.7);
                ushort ty = (ushort)(_positionPerso1.Y / mapPlayer.TileHeight);


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                  _sensPersoX1 = 1;
            }
            // si fleche Q enfoncé
            if (_keyboardState.IsKeyDown(Keys.Q))
            {

                ushort tx = (ushort)(_positionPerso1.X / mapPlayer.TileWidth - 0.6);
                ushort ty = (ushort)(_positionPerso1.Y / mapPlayer.TileHeight);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                   _sensPersoX1 = -1;
            }

            // si fleche Z enfoncé
            if (_keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso1.X / mapPlayer.TileWidth);
                ushort ty = (ushort)((_positionPerso1.Y) / mapPlayer.TileHeight - 0.7);

                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                   _sensPersoY1 = -1;
            }

            // si fleche S enfoncé
            if (_keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso1.X / mapPlayer.TileWidth);
                ushort ty = (ushort)((_positionPerso1.Y) / mapPlayer.TileHeight + 0.5);

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
            

        }
        private static bool IsCollision(ushort x, ushort y)
        {


            //Console.WriteLine(mapLayerCollision.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerEscalier.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerButton.GetTile(x, y).GlobalIdentifier);
            //Console.WriteLine(mapLayerPlaques.GetTile(x, y).GlobalIdentifier);

            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapJoueur.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_perso1, _positionPerso1);
            
            
        }

    }
}
