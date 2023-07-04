using Giny.IO.D2I;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.EnumsBuilder.Generation
{
    public class ItemTypes : CustomEnum
    {
        public override string ClassName => "ItemTypeEnum";

        protected override string GenerateEnumContent(List<D2OReader> readers, D2IFile d2i)
        {
            var types = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "ItemType")).EnumerateObjects().Cast<Giny.IO.D2OClasses.ItemType>();

            StringBuilder sb = new StringBuilder();

            foreach (var type in types)
            {
                var text = d2i.GetText((int)type.nameId);
                sb.AppendLine(ApplyRules(text) + "=" + type.id + ",");
            }

            return sb.ToString();

        }
    }
}
