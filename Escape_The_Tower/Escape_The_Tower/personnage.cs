using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class Personnage
    {
        private string pseudo;
        private Keys toucheBas;
        private bool toucheBasActive;
        private Keys toucheDroite;
        private bool toucheDroiteActive;
        private Keys toucheGauche;
        private bool toucheGaucheActive;
        private Keys toucheHaut;
        private bool toucheHautActive;
        private AnimatedSprite _perso;
        private int pasDeplacement;

        public Personnage(string pseudo, Keys toucheBas, Keys toucheDroite, Keys toucheGauche, Keys toucheHaut, AnimatedSprite perso)
        {
            this.Pseudo = pseudo;
            this.ToucheBas = toucheBas;
            this.ToucheDroite = toucheDroite;
            this.ToucheGauche = toucheGauche;
            this.ToucheHaut = toucheHaut;
            this.Perso = perso;
            this.toucheBasActive = false;
            this.toucheGaucheActive = false;
            this.toucheDroiteActive = false;
            this.toucheHautActive = false;
            this.pasDeplacement = 5;

        }

        public string Pseudo
        {
            get
            {
                return this.pseudo;
            }

            set
            {
                this.pseudo = value;
            }
        }

        public Keys ToucheBas
        {
            get
            {
                return this.toucheBas;
            }

            set
            {
                this.toucheBas = value;
            }
        }


        public Keys ToucheDroite
        {
            get
            {
                return this.toucheDroite;
            }

            set
            {
                this.toucheDroite = value;
            }
        }

        public Keys ToucheGauche
        {
            get
            {
                return this.toucheGauche;
            }

            set
            {
                this.toucheGauche = value;
            }
        }



        public Keys ToucheHaut
        {
            get
            {
                return this.toucheHaut;
            }

            set
            {
                this.toucheHaut = value;
            }
        }


        public AnimatedSprite Perso
        {
            get
            {
                return this._perso;
            }

            set
            {
                this._perso = value;
            }
        }
        public void RecupereToucheAppuyee(Keys touche)
        {
            if (touche == this.ToucheBas)
                this.toucheBasActive = true;
            else if (touche == this.ToucheHaut)
                this.toucheHautActive = true;
            else if (touche == this.ToucheGauche)
                this.toucheGaucheActive = true;
            else if (touche == this.ToucheDroite)
                this.toucheDroiteActive = true;
        }
        public void RecupereToucheRelachee(Keys touche)
        {
            if (touche == this.ToucheBas)
                this.toucheBasActive = false;
            else if (touche == this.ToucheHaut)
                this.toucheHautActive = false;
            else if (touche == this.ToucheGauche)
                this.toucheGaucheActive = false;
            else if (touche == this.ToucheDroite)
                this.toucheDroiteActive = false;
        }
        public void SeDeplace()
        {
            int x = 0;
            int y = 0;
            if (toucheDroiteActive && !toucheGaucheActive)
            {
                x = pasDeplacement;

            }
            if (toucheGaucheActive && !toucheDroiteActive)
            {
                x = -pasDeplacement;
            }
            if (toucheHautActive && !toucheBasActive)
            {
                y = -pasDeplacement;
            }
            if (!toucheHautActive && toucheBasActive)
            {
                y = pasDeplacement;
            }

        }
    }
}
