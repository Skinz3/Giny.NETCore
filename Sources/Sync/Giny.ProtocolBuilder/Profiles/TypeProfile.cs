using Giny.AS3;
using Giny.ProtocolBuilder.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder.Profiles
{
    public class TypeProfile : Profile
    {
        public TypeProfile(string as3FilePath) : base(as3FilePath)
        {

        }

        public override string TemplateFileName => "TypeTemplate.tt";

        public override string RelativeOutputPath => @"Sources\com\ankamagames\dofus\network\types\";

        public override string OutputDirectory => Path.Combine(Environment.CurrentDirectory, Constants.TYPES_OUTPUT_PATH);

        public override DofusConverter CreateDofusConverter(AS3File file)
        {
            return new TypeConverter(file);
        }

        public override bool Skip(AS3File file)
        {
            return false;
        }
    }
}
