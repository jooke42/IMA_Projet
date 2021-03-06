﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Rectangle : GameObject
    {
        V3 direct_width, direct_heigth,normal;
        int width, heigth;

        public Rectangle(V3 _position,V3 _direct_width, V3 _direct_heigth, int _width, int _height, Couleur _couleur = new Couleur(), Texture _texture = null, BumpMapping _bumpMap = null)
            : base(_position, _couleur, _texture, _bumpMap)
        {
            this.direct_width = _direct_width;
            this.direct_heigth = _direct_heigth;
            width = _width;
            heigth = _height;
            direct_width.Normalize();
            direct_heigth.Normalize();
            this.normal = direct_width ^ direct_heigth;
            this.normal.Normalize();
        }
        

        public override void derivative(float u, float v, out V3 dmdu, out V3 dmdv)
        {

            dmdu = new V3(this.direct_width);
            dmdu.Normalize();
            dmdv = new V3(this.direct_heigth);
            dmdv.Normalize();
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
            V3 AI = p - this.position;
            V3 ab, ac;
            ab = direct_width * width;
            ac = direct_heigth * heigth;
            float alpha = V3.prod_scal(ref AI, ref ab) / V3.prod_scal(ref ab, ref ab);
            float theta = V3.prod_scal(ref AI, ref ac) / V3.prod_scal(ref ac, ref ac);
            u = alpha;
            v = theta;
        }

        public override Point generatePoint(float u, float v)
        {
            Point p = new Point();
            V3 ab, ac;
            ab = direct_width * width;
            ac = direct_heigth * heigth;
            p.couleur = new Couleur(0f, 0f, 0f);
            p.normal = normal;
            p.gameObject = this;
            p.position = this.position + u * ab + v * ac;
            p.u = u;
            p.v = v;
            
            return p;
        }

        public override void getCoordBumpMap(Point p, out float u_bump, out float v_bump)
        {
            u_bump = p.u ;
            v_bump = p.v;
        }

        public override bool getIntersectPoint(V3 camPos, V3 dirRayon, out float newt)
        {
            newt = -1;
            V3 I, AI;
            V3 ab, ac;
            float alpha, theta;
            float t;

            ab = direct_width * width;
            ac = direct_heigth * heigth;
            

            V3 CamPosA = this.position - camPos;
            t = V3.prod_scal(ref CamPosA, ref this.normal) / V3.prod_scal(ref dirRayon, ref this.normal);
            I = camPos + t * dirRayon;
            AI = I - this.position;

            alpha = V3.prod_scal(ref AI,ref ab) / V3.prod_scal(ref ab, ref ab);
            theta = V3.prod_scal(ref AI, ref ac) / V3.prod_scal(ref ac, ref ac);
            
            if (alpha >= 0 && alpha <= 1 && theta >= 0 && theta <= 1)
            {
                newt = t;
                return true;
            }
                
            return false;
        }


        public override Couleur getPointBasicColor(Point p)
        {
            if (texture == null)
                return couleur;

            float u_texture, v_texture;


            u_texture = p.u ;
            v_texture = p.v;

            return texture.LireCouleur(u_texture,1- v_texture);
        }
    }
}
