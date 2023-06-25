using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("NpcMessage", "")]
    public class NpcMessage : IDataObject , IIndexedData
    {        public const string MODULE = "NpcMessages";

        public int Id => (int)id;

        public int id;
        public uint messageId;
        public List<string> messageParams;

        [D2OIgnore]
        public int Id_
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        [D2OIgnore]
        public uint MessageId
        {
            get
            {
                return messageId;
            }
            set
            {
                messageId = value;
            }
        }
        [D2OIgnore]
        public List<string> MessageParams
        {
            get
            {
                return messageParams;
            }
            set
            {
                messageParams = value;
            }
        }

    }}
