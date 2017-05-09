using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class LampAmbiant : Lamp
    {
        public LampAmbiant(Couleur _couleur, V3 _positon, List<LightModel> _lightModels) 
            : base(_couleur, _positon, _lightModels)
        {
        }

        public override void apply(ref Point p)
        {
            foreach (LightModel lightModel in this.lightModels)
            {
                lightModel.apply(ref p,this);
            }
        }
    }
}
