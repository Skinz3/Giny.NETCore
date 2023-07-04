using Giny.IO.D2I;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.EnumsBuilder.Generation
{
    public class OptionalFeatures : CustomEnum
    {
        public override string ClassName => "OptionalFeaturesEnum";

        protected override string GenerateEnumContent(List<D2OReader> readers, D2IFile d2i)
        {
            var opts = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "OptionalFeature")).EnumerateObjects().Cast<Giny.IO.D2OClasses.OptionalFeature>();

            StringBuilder sb = new StringBuilder();

            foreach (var opt in opts)
            {
                sb.AppendLine(ApplyRules(opt.keyword) + "=" + opt.id + ",");
            }
            return sb.ToString();
        }
    }
}
