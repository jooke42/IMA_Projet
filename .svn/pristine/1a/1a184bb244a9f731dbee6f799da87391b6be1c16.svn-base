using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA 
{
    class DiffuseLight : LightModel
    {
    
        public override void apply(ref Point p, Lamp lamp)
        {
            V3 l = -lamp.direction;
            float angle = V3.prod_scal(ref p.normal, ref l);
            if(angle > 0)
                p.couleur += (lamp.getCouleur() * p.gameObject.getPointBasicColor(p))*(angle);
        }
    }
}
