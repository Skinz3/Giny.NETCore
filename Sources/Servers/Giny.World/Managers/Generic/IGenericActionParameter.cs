using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Generic
{
    public interface IGenericActionParameter
    {
        GenericActionEnum ActionIdentifier
        {
            get;
        }
        string Param1
        {
            get;
        }
        string Param2
        {
            get;
        }
        string Param3
        {
            get;
        }
        string Criteria
        {
            get;
        }
    }
}
