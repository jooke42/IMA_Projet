using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    struct Point
    {
        public V3 position;
        public Couleur couleur;
        public GameObject gameObject;
        public float u, v;
        public V3 normal;
    }
    class Sphere : GameObject
    {
        public float rayon;

        public Sphere(V3 _position,float _rayon,Couleur _couleur = new Couleur(),Texture _texture = null, BumpMapping _bumpMap = null):
            base( _position, _couleur, _texture, _bumpMap)
        {
            this.rayon = _rayon;
        }

        public override Couleur getPointBasicColor(Point p)
        {
            if(texture == null)
                return couleur;

            float  u_texture, v_texture;
            

            u_texture = p.u / (2 * IMA.PI);
            v_texture = (p.v + IMA.PI / 2) * (1 / IMA.PI);

            return texture.LireCouleur(u_texture,1-v_texture);

        }

        public override Point generatePoint(float u, float v)
        {
            Point p = new Point();
            p.couleur = new Couleur(0f, 0f, 0f);
            p.normal = S(u, v);
            p.gameObject = this;
            p.position = this.rayon * p.normal + this.position;
            p.u = u;
            p.v = v;

            if (this.bumpMap != null)
                this.bumpMap.generateNewNormal(ref p);

            return p;
        }

        private  V3 S(float u, float v)
        {
            V3 result = new V3();
            result.x = IMA.Cosf(v) * IMA.Cosf(u);
            result.y = IMA.Cosf(v) * IMA.Sinf(u);
            result.z = IMA.Sinf(v);
            return result;

        }

        public override void draw(float pas, Scene scene)
        {

            for (float u = 0; u <= 2 * IMA.PI; u += pas)
                for (float v = (-IMA.PI) / 2; v <= IMA.PI / 2; v += pas)
                {
                    Point p = this.generatePoint(u, v);
                    scene.drawPoint(ref p);

                }

        }

        public override void derivative(float u, float v, out V3 dmdu, out V3 dmdv)
        {
            dmdu = new V3();
            dmdv = new V3();

            dmdu.x = -IMA.Sinf(u) * IMA.Cosf(v);
            dmdu.y = IMA.Cosf(u) * IMA.Cosf(v);
            dmdv.x = -IMA.Cosf(u) * IMA.Sinf(v);
            dmdv.y = -IMA.Sinf(u) * IMA.Sinf(v);
            dmdv.z = IMA.Cosf(v);
        }

        public override bool getIntersectPoint(V3 camPos, V3 dirRayon, out float newt)
        {
            float t1, t2;
            float a, b, c, delta, deltaSqrt;
            a = dirRayon * dirRayon;
            b = 2 * dirRayon * camPos - 2 * this.position * dirRayon;
            c = this.position * this.position - 2 * this.position * camPos + camPos * camPos - this.rayon * this.rayon;
            newt = 0;
            delta = b * b - 4 * a * c;
            if (delta < 0)
                return false;
            deltaSqrt = IMA.Sqrtf(delta);
            t1 = -b + deltaSqrt / 2*a;
            t2 = -b - deltaSqrt / 2*a;
            if (t1 > 0 && t2 > 0)
            {
                newt = t1;
                return true;
            }
                
            if (t1 < 0 && t2 > 0)
            {
                newt = t2;
                return true;
            }
            return false;
        }

        public override void finduv(V3 p, out float u, out float v)
        {
            IMA.Invert_Coord_Spherique(p,this.rayon,out u,out v);
        }

        
    }
}
