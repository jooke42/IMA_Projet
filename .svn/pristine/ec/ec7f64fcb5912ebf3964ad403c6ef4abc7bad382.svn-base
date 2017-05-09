using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
   
    abstract class Lamp
    {
        protected V3 position;
        protected List<LightModel> lightModels = new List<LightModel>();
        protected Couleur couleur;
        public V3 direction;
    

        public Lamp(Couleur _couleur,V3 _positon , List<LightModel> _lightModels, V3 _direction= new V3())
        {
            this.lightModels = _lightModels;
            this.position = _positon;
            this.couleur = _couleur;
            this.direction = _direction;
        }

        public abstract void apply(ref Point p);

        public Couleur getCouleur()
        {
            return couleur;
        }
        public V3 getDirection()
        {
            return direction;
        }
        
    }
}
