﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static Scene scene;


        public static void Go()
        {

            scene = new Scene();

            scene.init();

            scene.draw();

        }
    }
}
