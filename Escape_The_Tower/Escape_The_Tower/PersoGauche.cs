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
        public static AnimatedSprite _perso;
        public static KeyboardState _keyboardState;
        public static int _vitessePerso;
        public static int _sensPersoX;
        public static int _sensPersoY;
        
        public static int sprite_width;
        public static int sprite_height;
        public static TiledMap _tiledMap;
        public static SpriteBatch _spriteBatch;


        public static void Initialize()
        {
            //---------------------------
            //Initialise perso
            //---------------------------

            _positionPerso = new Vector2(Game1.LONGUEUR_ECRAN / 2 - 50, Game1.LARGEUR_ECRAN / 2);

            _vitessePerso = 100;


        }

        public static void LoadContent(Game game)
        {
            //définition des animation

            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

        }
        public static void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            _perso.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            _sensPersoX = 0;
            _sensPersoY = 0;

            // si fleche D enfoncé
            if (_keyboardState.IsKeyDown(Keys.D))
            {

                ushort txMillieu = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort tyMillieu = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1.5);

                ushort txHaut = (ushort)(_positionPerso.X + sprite_width / 2 / _tiledMap.TileWidth + 0.5);
                ushort tyHaut = (ushort)(_positionPerso.Y + sprite_height / 2 / _tiledMap.TileHeight + 1.5);

                ushort txBas = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort tyBas = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1.5);


                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = 1;
            }
            // si fleche Q enfoncé
            if (_keyboardState.IsKeyDown(Keys.Q))
            {
                ushort txMillieu = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.4);
                ushort tyMillieu = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 0.4);

                ushort txHaut = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.4);
                ushort tyHaut = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 0.4);

                ushort txBas = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.4);
                ushort tyBas = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 0.4);

                if (!IsCollision(txMillieu, tyMillieu) && !IsCollision(txBas, tyBas))// && !IsCollision(txHaut, tyHaut)
                    _sensPersoX = -1;
            }

            // si fleche Z enfoncé
            if (_keyboardState.IsKeyDown(Keys.Z))
            {
                ushort txGauche = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort tyGauche = (ushort)((_positionPerso.Y) / _tiledMap.TileHeight - 0.5);



                ushort txDroite = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort tyDroite = (ushort)((_positionPerso.Y) / _tiledMap.TileHeight - 0.5);

                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = -1;
            }

            // si fleche S enfoncé
            if (_keyboardState.IsKeyDown(Keys.S))
            {
                ushort txGauche = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.8);
                ushort tyGauche = (ushort)((_positionPerso.Y) / _tiledMap.TileHeight + 0.8);

                ushort txDroite = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.8);
                ushort tyDroite = (ushort)((_positionPerso.Y) / _tiledMap.TileHeight + 0.8);

                if (!IsCollision(txGauche, tyGauche) && !IsCollision(txDroite, tyDroite))
                    _sensPersoY = 1;

            }

            // deplace le personnage
            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

            if (_sensPersoX == 0 && _sensPersoY == 0) _perso.Play("idle"); // une des animations définies dans « persoAnimation.sf »

            // si on bouge alors on play anim
            else if (_sensPersoX == 1 && _sensPersoY == 1 || _sensPersoX == -1 && _sensPersoY == 1 || _sensPersoX == 0 && _sensPersoY == 1) _perso.Play("walkSouth");
            else if (_sensPersoX == 1 && _sensPersoY == -1 || _sensPersoX == -1 && _sensPersoY == -1 || _sensPersoX == 0 && _sensPersoY == -1) _perso.Play("walkNorth");
            else if (_sensPersoX == -1 && _sensPersoY == 0) _perso.Play("walkWest");
            else if (_sensPersoX == 1 && _sensPersoY == 0) _perso.Play("walkEast");

            _perso.Update(deltaTime); // time écoulé
            

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
        public static void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso/*, null, Color.White, 0, _origin, _rotation ,SpriteEffects.None, 0*/);
            _spriteBatch.End();
            
        }

    }
}
