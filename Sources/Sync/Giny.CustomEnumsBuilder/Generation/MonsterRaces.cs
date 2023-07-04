using Giny.IO.D2I;
using Giny.IO.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.EnumsBuilder.Generation
{
    public class MonsterRaces : CustomEnum
    {
        public override string ClassName => "MonsterRacesEnum";

        protected override string GenerateEnumContent(List<D2OReader> readers, D2IFile d2i)
        {
            var races = readers.FirstOrDefault(x => x.Classes.Any(w => w.Value.Name == "MonsterRace")).EnumerateObjects().Cast<Giny.IO.D2OClasses.MonsterRace>();

            StringBuilder sb = new StringBuilder();

            foreach (var race in races)
            {
                var text = d2i.GetText((int)race.nameId);
                sb.AppendLine(ApplyRules(text) + "=" + race.id + ",");
            }

            return sb.ToString();
        }
    }
}
