using Giny.IO.D2I;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.EnumsBuilder.Generation
{
    class Jobs : CustomEnum
    {
        public override string ClassName => "JobTypeEnum";

        protected override string GenerateEnumContent(List<D2OReader> readers, D2IFile d2i)
        {
            var skills = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "Job")).EnumerateObjects().Cast<Giny.IO.D2OClasses.Job>();

            StringBuilder sb = new StringBuilder();

            foreach (var skill in skills)
            {
                sb.AppendLine(ApplyRules(d2i.GetText((int)skill.NameId)) + "=" + skill.id + ",");
            }
            return sb.ToString();
        }
    }
}
