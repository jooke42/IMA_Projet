﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA 
{
    class AmbiantLight : LightModel
    {
    
        public override void apply(ref Point p, Lamp lamp)
        {
            p.couleur += lamp.getCouleur() * p.gameObject.getPointBasicColor(p);
        }
    }
}
