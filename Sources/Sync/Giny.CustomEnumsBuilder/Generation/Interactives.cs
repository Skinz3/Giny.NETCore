using Giny.IO.D2I;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.EnumsBuilder.Generation
{
    public class Interactives : CustomEnum
    {
        public override string ClassName => "InteractiveTypeEnum";

        protected override string GenerateEnumContent(List<D2OReader> readers)
        {
            var interactives = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "Interactive")).EnumerateObjects().Cast<Giny.IO.D2OClasses.Interactive>();

            StringBuilder sb = new StringBuilder();

            foreach (var interactive in interactives)
            {
                sb.AppendLine(ApplyRules(D2IManager.GetText((int)interactive.NameId, "en")) + interactive.id + "=" + interactive.id + ",");
            }
            return sb.ToString();
        }
    }
}
