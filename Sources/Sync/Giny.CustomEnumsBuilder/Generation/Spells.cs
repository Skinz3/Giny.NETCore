using Giny.IO.D2I;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.EnumsBuilder.Generation
{
    public class Spells : CustomEnum
    {
        public override string ClassName => "SpellEnum";

        protected override string GenerateEnumContent(List<D2OReader> readers, D2IFile d2i)
        {
            var spells = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "Spell")).EnumerateObjects().Cast<Giny.IO.D2OClasses.Spell>();

            StringBuilder sb = new StringBuilder();

            Dictionary<string, int> values = new Dictionary<string, int>();

            foreach (var spell in spells)
            {
                string raw = d2i.GetText((int)spell.nameId);

                raw = Regex.Replace(raw, @"\s+", "");
                raw = Regex.Replace(raw, @"[\d-]", string.Empty);

                if (raw.Contains("UNKNOWN") || raw == string.Empty)
                {
                    raw = "Unknown" + spell.id.ToString();
                }

                raw = ApplyRules(raw);

                if (values.ContainsKey(raw))
                {
                    raw = raw + spell.id;
                }

                values.Add(raw, spell.id);
            }


            foreach (var pair in values)
            {
                sb.AppendLine(pair.Key + "=" + pair.Value + ",");
            }


            return sb.ToString();


        }
    }
}
