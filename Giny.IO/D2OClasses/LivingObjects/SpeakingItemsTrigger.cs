using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SpeakingItemsTrigger", "")]
    public class SpeakingItemsTrigger : IDataObject , IIndexedData
    {        public const string MODULE = "SpeakingItemsTriggers";

        public int Id => throw new NotImplementedException();

        public int triggersId;
        public List<int> textIds;
        public List<int> states;

        [D2OIgnore]
        public int TriggersId
        {
            get
            {
                return triggersId;
            }
            set
            {
                triggersId = value;
            }
        }
        [D2OIgnore]
        public List<int> TextIds
        {
            get
            {
                return textIds;
            }
            set
            {
                textIds = value;
            }
        }
        [D2OIgnore]
        public List<int> States
        {
            get
            {
                return states;
            }
            set
            {
                states = value;
            }
        }

    }}
