using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("InfoMessage", "")]
    public class InfoMessage : IDataObject , IIndexedData
    {        public const string MODULE = "InfoMessages";

        public int Id => throw new NotImplementedException();

        public uint typeId;
        public uint messageId;
        public uint textId;

        [D2OIgnore]
        public uint TypeId
        {
            get
            {
                return typeId;
            }
            set
            {
                typeId = value;
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
        public uint TextId
        {
            get
            {
                return textId;
            }
            set
            {
                textId = value;
            }
        }

    }}
