﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Scene
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<Lamp> lamps = new List<Lamp>();
        public Camera mainCamera;
        public float pas = 0.005f;

        public Scene()
        {
        }

        public void init()
        {
            int larg = BitmapEcran.GetWidth();
            int haut = BitmapEcran.GetHeight();
            mainCamera = new Camera(new V3(0, 0, 0), larg, haut, 3000);
            
            Texture testText = new Texture("uvtest.jpg");
            Texture testTextRect = new Texture("test.jpg");
            Texture gold = new Texture("gold.jpg");
            BumpMapping goldBump = new BumpMapping(new Texture("gold_bump.jpg"), 0.5f);
            Texture lead = new Texture("lead.jpg");
            BumpMapping leadBump = new BumpMapping(new Texture("lead_bump.jpg"),0.3f);
            BumpMapping bump38 = new BumpMapping(new Texture("bump38.jpg"),0.5f);
            BumpMapping bump1 = new BumpMapping(new Texture("bump1.jpg"), 1.5f);
            Texture stone = new Texture("stone2.jpg");
            
            Couleur Green = new Couleur(0.0f, 1.0f, 0.0f);
            Couleur Red = new Couleur(1.0f, 0.0f, 0.0f);
            
            AmbiantLight ambiantLight = new AmbiantLight();
            DiffuseLight diffuseLight = new DiffuseLight();
            SpecularLight specularLight = new SpecularLight();

           /* lamps.Add(
              new LampAmbiant(
                  new Couleur(0.20f, 0.20f, 0.20f),
                  new V3(0, 0, 0),
                  new List<LightModel> { ambiantLight }
                  )
              );*/

            lamps.Add(
                 new DirectionalLamp(
                     new Couleur(0.6f,0.6f, 0.6f),
                     new V3(0, 0, 0),//pos
                     new List<LightModel> { diffuseLight, specularLight }, // lights effects
                     new V3(0, 1, -1)//direction
                     )
                 );

            gameObjects.Add(new Sphere(new V3(50,50, 0), 10, Green)); // A
            gameObjects.Add(new Sphere(new V3(BitmapEcran.GetWidth(), 50, 0), 10, Green)); //B

            gameObjects.Add(new Sphere(new V3(50, 250, 0), 10, Red)); //C
            gameObjects.Add(new Sphere(new V3(BitmapEcran.GetWidth(), 250, 0), 10, Red)); // D

            gameObjects.Add(new Rectangle(new V3(50, 50, 0), new V3(1, 0, 0),new V3(0,1,0),
                BitmapEcran.GetWidth() - 50, 600, Green, stone, bump1));
            gameObjects.Add(new Rectangle(new V3(50, 650, 0), new V3(1, 0, 0), new V3(0, 0, 1),
                BitmapEcran.GetWidth() - 50, 600, Green, stone, bump1));
            gameObjects.Add(new Rectangle(new V3(50, 50, 0), new V3(1, 0, 0), new V3(0, 0, 1),
                BitmapEcran.GetWidth() - 50, 600, Green, stone, bump1));
            gameObjects.Add(new Rectangle(new V3(50, 650, 0), new V3(1, 0, 0), new V3(0, 0, 1),
                BitmapEcran.GetWidth() - 50, 600, Green, stone, bump1));
        }

        public void drawPoint(ref Point p)
        {
            
            if (getCamera().isDrawn(p))
            {
                int ecran_x = (int)p.position.x;
                int ecran_y = (int)p.position.z;
                if(p.gameObject.bumpMap != null)
                    p.gameObject.bumpMap.generateNewNormal(ref p);

                foreach(Lamp l in lamps)
                    l.apply(ref p);

                BitmapEcran.DrawPixel(ecran_x, ecran_y, p.couleur);

            }
        }

        public Camera getCamera()
        {
            return mainCamera;
        }

        public V3 getEyes()
        {
            return mainCamera.eyes;
        }

        public void draw()
        {
          mainCamera.generateView();
          /* foreach (GameObject g in gameObjects)
                g.draw(pas,this);*/
        }
    }

    
}
