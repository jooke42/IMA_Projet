using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Camera
    {
        public V3 origin;
        public  V3 eyes;
        public int larg,haut;
        public float maxDist;
        public  int[,] zBuffer;


        public Camera(V3 _origin,int _larg,int _haut,float _maxDist)
        {
            this.origin = _origin;
           
            this.larg = _larg;
            this.haut = _haut;
            this.zBuffer = new int[larg, haut];
            float distEyes = -1.5f * larg;
            this.maxDist = _maxDist - distEyes;
            this.eyes = new V3(_larg / 2, distEyes, _haut / 2);
            init();

        }

       public Boolean rayCast(V3 camPos ,V3 DirRayon,out Point p)
        {
            float t = maxDist;
            float newt;
            GameObject selectGameObject= null;
            foreach (GameObject gameObject in ProjetEleve.scene.gameObjects)
            {
                try
                {
                    bool hit = gameObject.getIntersectPoint(camPos, DirRayon,out newt);
                    if(hit && t > newt)
                    {
                        t = newt;
                        selectGameObject = gameObject;
                    }
                    
                }
                catch(Exception e)
                {

                }
                
            }
            p = new Point();
            if (selectGameObject == null)
                return false;
            float u, v;

            V3 pPos = camPos + t * DirRayon;
            V3 centered = selectGameObject.getCenteredPoint(pPos);
            selectGameObject.finduv(centered, out u, out v);
            p = selectGameObject.generatePoint(u, v);

            //apply bumpMap
            if (p.gameObject.bumpMap != null)
                p.gameObject.bumpMap.generateNewNormal(ref p);

            //apply lights
            foreach (Lamp l in ProjetEleve.scene.lamps)
                l.apply(ref p);
            return true;
        }

        public void generateView()
        {
            Point p;
            for (int x_ecran = 0; x_ecran <= larg ; x_ecran++) {
                for (int y_ecran = 0; y_ecran <= haut ; y_ecran++)
                {
                    V3 PosPixScene = new V3(x_ecran, 0, y_ecran);
                    
                    V3 dirRayon = PosPixScene - eyes;
                    dirRayon.Normalize();
                    
                    if(rayCast(eyes, dirRayon,out p))
                        BitmapEcran.DrawPixel(x_ecran, y_ecran, p.couleur);
                }

            }
               
        }
        public Boolean isDrawn(Point p)
        {
            int ecran_x = (int)p.position.x;
            int ecran_y = (int)p.position.z;
            int ecran_depth = (int)p.position.y;

            if (ecran_x >= 0 && ecran_x < larg && ecran_y >= 0 && ecran_y < haut && zBuffer[ecran_x, ecran_y] > ecran_depth)
            {
                zBuffer[ecran_x, ecran_y] = ecran_depth;
                return true;
            }

            return false;

        }



        public void init()
        {
            for (int row = 0; row < zBuffer.GetLength(0); row++)
            {
                for (int col = 0; col < zBuffer.GetLength(1); col++)
                {
                    zBuffer[row, col] =(int) maxDist;
                }
            }
        }
        
    }
}
