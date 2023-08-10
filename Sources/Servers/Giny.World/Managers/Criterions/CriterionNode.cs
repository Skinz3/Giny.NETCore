using Giny.World.Managers.Criterias;
using Giny.World.Managers.Criterions.Handlers;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Criterions
{
    public class CriterionNode : Node
    {
        public string Criterion
        {
            get;
            private set;
        }

        public Criterion CriterionHandler
        {
            get;
            private set;
        }


        public CriterionNode(string criterion)
        {
            this.Criterion = criterion;
            this.CriterionHandler = CriteriasManager.Instance.GetCriteriaHandler(criterion);
        }

        public override bool Eval(WorldClient client)
        {
            return CriterionHandler.Eval(client);
        }

        public override string ToString()
        {
            return this.Criterion;
        }

        public override IEnumerable<Criterion> FindCriterionHandlers()
        {
            return new Criterion[] { CriterionHandler };
        }
    }
}
