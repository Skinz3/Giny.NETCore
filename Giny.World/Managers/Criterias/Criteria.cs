using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterias
{
    public abstract class Criteria
    {
        private const char EqualSymbol = '=';

        private const char InferiorSymbol = '<';

        private const char SuperiorSymbol = '>';

        public char ComparaisonSymbol => CriteriaFull.Remove(0, 2).Take(1).ToArray()[0]; 

        public string CriteriaValue => CriteriaFull.Remove(0, 3); 

        public string CriteriaFull { get; set; }

        public abstract bool Eval(WorldClient client);

        public static bool BasicEval(string criteriavalue, char comparaisonsymbol, int delta)
        {
            int criterialDelta = int.Parse(criteriavalue);

            switch (comparaisonsymbol)
            {
                case EqualSymbol:
                    if (delta == criterialDelta)
                        return true;
                    break;
                case InferiorSymbol:
                    if (delta < criterialDelta)
                        return true;
                    break;
                case SuperiorSymbol:
                    if (delta > criterialDelta)
                        return true;
                    break;
            }

            return false;
        }
    }
}
