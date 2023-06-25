using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Dialogs
{
    public class BookDialog : Dialog
    {
        public override DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_BOOK;

        public int DocumentId
        {
            get;
            set;
        }

        public BookDialog(Character character, int documentId)
            : base(character)
        {
            this.DocumentId = documentId;
        }
        public override void Open()
        {
            Character.Client.Send(new DocumentReadingBeginMessage((short)DocumentId));
        }
    }
}
