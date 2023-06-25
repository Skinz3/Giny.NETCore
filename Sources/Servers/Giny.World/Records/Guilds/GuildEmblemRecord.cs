using Giny.Core.DesignPattern;
using Giny.Protocol.Types;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Guilds
{
    [ProtoContract]
    public class GuildEmblemRecord
    {
        [ProtoMember(1)]
        public short SymbolShape
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public int SymbolColor
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public byte BackgroundShape
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public int BackgroundColor
        {
            get;
            set;
        }

        public GuildEmblemRecord()
        {

        }
        public GuildEmblemRecord(short symbolShape, int symbolColor, byte backgroundShape, int backgroundColor)
        {
            this.SymbolShape = symbolShape;
            this.SymbolColor = symbolColor;
            this.BackgroundShape = backgroundShape;
            this.BackgroundColor = backgroundColor;
        }

        public GuildEmblem ToGuildEmblem()
        {
            return new GuildEmblem()
            {
                backgroundColor = BackgroundColor,
                backgroundShape = BackgroundShape,
                symbolColor = SymbolColor,
                symbolShape = SymbolShape,
            };
        }

        [WIP("override operators.")]
        public override bool Equals(object obj)
        {
            GuildEmblemRecord emblem = obj as GuildEmblemRecord;

            if (emblem == null)
            {
                return false;
            }

            return SymbolShape == emblem.SymbolShape && SymbolColor == emblem.SymbolColor && BackgroundShape == emblem.BackgroundShape && BackgroundColor == emblem.BackgroundColor;
        }
    }
}
