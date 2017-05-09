﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Rectangle : GameObject
    {
        V3 ab, ac, normal;

        public Rectangle(V3 _position,V3 _ab,V3 _ac, Couleur _couleur = new Couleur(), Texture _texture = null, BumpMapping _bumpMap = null)
            : base(_position, _couleur, _texture, _bumpMap)
        {
            this.ab = _ab;
            this.ac = _ac;
            this.normal = ab ^ac;
            this.normal.Normalize();
        }

        public override void derivative(float u, float v, out V3 dmdu, out V3 dmdv)
        {

            dmdu = new V3();
            dmdv = new V3();

            dmdu.x = 1;
            dmdv.y = 1;
        }

        public override void draw(float pas, Scene scene)
        {
            pas = 0.0005f;
            for (float u = 0; u <= 1; u += pas)
            {
                for (float v = 0; v <= 1; v += pas)
                {
                    Point p = this.generatePoint(u, v);
                    scene.drawPoint(ref p);
                }

            }
        }

        public override void finduv(V3 p, out float u, out float v)
        {
            throw new NotImplementedException();
        }

        public override Point generatePoint(float u, float v)
        {
            Point p = new Point();
            p.couleur = new Couleur(0f, 0f, 0f);
            p.normal = normal;
            p.gameObject = this;
            p.position = this.position + u * ab + v * ac;
            p.u = u;
            p.v = v;

         

            return p;
        }

        public override bool getIntersectPoint(V3 camPos, V3 dirRayon, out float newt)
        {
            throw new NotImplementedException();
        }

        public override Couleur getPointBasicColor(Point p)
        {
            if (texture == null)
                return couleur;

            float u_texture, v_texture;


            u_texture = p.u ;
            v_texture = p.v;

            return texture.LireCouleur(u_texture, v_texture);
        }
    }
}
