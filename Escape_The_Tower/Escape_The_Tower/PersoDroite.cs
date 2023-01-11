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


        public static Vector2 _positionPerso2;
        public static AnimatedSprite _perso2;
        public static KeyboardState _keyboardState;
        public static int _vitessePerso2;
        public static double _sensPersoX2;
        public static double _sensPersoY2;

        public static int sprite_width2;
        public static int sprite_height2;

        public static TiledMapTileLayer mapJoueur;
        public static TiledMap mapPlayer;


        public static void Initialize()
        {
            //---------------------------
            //Initialise perso
            //---------------------------

            _positionPerso2 = new Vector2(830, 600);

            _vitessePerso2 = 100;


        }

        public static void LoadContent(Game game)
        {
            //définition des animation

            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("perso2.sf", new JsonContentLoader());
            _perso2 = new AnimatedSprite(spriteSheet);

        }
        public static void Update(GameTime gametime)
        {
            float deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            _perso2.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            _sensPersoX2 = 0;
            _sensPersoY2 = 0;

            //-------Deplacement--------

            // si fleche fleche droite enfoncé
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                ushort tx = (ushort)(_positionPerso2.X / mapJoueur.TileWidth +0.7 );
                ushort ty = (ushort)(_positionPerso2.Y  / mapJoueur.TileHeight );


                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                {
                    _sensPersoX2 = 1;
                   
                }
            }
            // si fleche fleche gauche enfoncé
            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                ushort tx = (ushort)(_positionPerso2.X / mapJoueur.TileWidth - 0.6);
                ushort ty = (ushort)(_positionPerso2.Y / mapJoueur.TileHeight );



                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))// && !IsCollision(txHaut, tyHaut)
                {
                    _sensPersoX2 = -1;
                    
                }
                
            }

            // si fleche fleche haut enfoncé
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                ushort tx = (ushort)(_positionPerso2.X / mapJoueur.TileWidth );
                ushort ty = (ushort)((_positionPerso2.Y) / mapJoueur.TileHeight - 0.7);



                if (!IsCollision(tx, ty) && !IsCollision(tx, ty))
                    _sensPersoY2 = -1;
                
            }

            // si fleche bas enfoncé
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                ushort tx = (ushort)(_positionPerso2.X / mapJoueur.TileWidth );
                ushort ty = (ushort)((_positionPerso2.Y) / mapJoueur.TileHeight + 0.5);

                

                if (!IsCollision(tx, ty))
                    _sensPersoY2 = 1;
                

            }



            // deplace le personnage
            _positionPerso2.X += (float)_sensPersoX2 * _vitessePerso2 * deltaTime;
            _positionPerso2.Y += (float)_sensPersoY2 * _vitessePerso2 * deltaTime;

            if (_sensPersoX2 == 0 && _sensPersoY2 == 0) _perso2.Play("pause"); // une des animations définies dans « persoAnimation.sf »

            // si on bouge alors on play anim
            else if (_sensPersoX2 == 1 && _sensPersoY2 == 1 || _sensPersoX2 == -1 && _sensPersoY2 == 1 || _sensPersoX2 == 0 && _sensPersoY2 == 1) _perso2.Play("marcheB");
            else if (_sensPersoX2 == 1 && _sensPersoY2 == -1 || _sensPersoX2 == -1 && _sensPersoY2 == -1 || _sensPersoX2 == 0 && _sensPersoY2 == -1) _perso2.Play("marcheH");
            else if (_sensPersoX2 == -1 && _sensPersoY2 == 0) _perso2.Play("marcheG");
            else if (_sensPersoX2 == 1 && _sensPersoY2 == 0) _perso2.Play("marcheD");

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
            if (mapJoueur.TryGetTile(x, y, out tile) == false)
            
            return false;
            if (!tile.Value.IsBlank)

            return true;
            return false;
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(_perso2, _positionPerso2);


        }

    }
}
