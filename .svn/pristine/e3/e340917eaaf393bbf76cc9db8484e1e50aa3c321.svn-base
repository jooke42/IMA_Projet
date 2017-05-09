namespace Projet_IMA
{
     class BumpMapping
    {
        public Texture bumpMap;
        public float k = 0.5f;

        public BumpMapping(Texture _bumpMap, float _k  = 0.2f)
        {
            this.bumpMap = _bumpMap;
            this.k = _k;
        }

        public void generateNewNormal(ref Point p)
        {
            float dhdu, dhdv;
            V3 dmdu, dmdv;
            bumpMap.Bump(p.u, p.v, out dhdu, out dhdv);
            p.gameObject.derivative(p.u, p.v, out dmdu, out dmdv);
            
            p.normal = p.normal + k * ((dmdu ^ (dhdv * p.normal))+((dhdu * p.normal)^ dmdv));

        }
    }
}