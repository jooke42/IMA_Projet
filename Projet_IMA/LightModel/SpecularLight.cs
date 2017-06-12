using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class SpecularLight : LightModel
    {
        private double specularPower;
        public SpecularLight(double _specularPower = 100) : base()
        {
            this.specularPower = _specularPower;
        }
        public override void apply(ref Point p, Lamp lamp)
        {
           
            V3 L = -lamp.direction;
            float cosAlpha = V3.prod_scal(ref p.normal, ref L);

            if (cosAlpha > 0)
            {
                V3 N = p.normal;
                V3 D = ProjetEleve.scene.getEyes() - p.position;
                D.Normalize();
                V3 R = 2*(cosAlpha * N) + lamp.direction;
                R.Normalize();
                p.couleur += lamp.getCouleur() * (float)Math.Pow(R*D, specularPower);
            }
           
        }
    }
}
