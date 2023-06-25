using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Npc", "")]
    public class Npc : IDataObject , IIndexedData
    {        public const string MODULE = "Npcs";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public List<List<int>> dialogMessages;
        public List<List<int>> dialogReplies;
        public List<uint> actions;
        public uint gender;
        public string look;
        public int tokenShop;
        public List<AnimFunNpcData> animFunList;
        public bool fastAnimsFun;
        public bool tooltipVisible;

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
        public uint NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        [D2OIgnore]
        public List<List<int>> DialogMessages
        {
            get
            {
                return dialogMessages;
            }
            set
            {
                dialogMessages = value;
            }
        }
        [D2OIgnore]
        public List<List<int>> DialogReplies
        {
            get
            {
                return dialogReplies;
            }
            set
            {
                dialogReplies = value;
            }
        }
        [D2OIgnore]
        public List<uint> Actions
        {
            get
            {
                return actions;
            }
            set
            {
                actions = value;
            }
        }
        [D2OIgnore]
        public uint Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }
        [D2OIgnore]
        public string Look
        {
            get
            {
                return look;
            }
            set
            {
                look = value;
            }
        }
        [D2OIgnore]
        public int TokenShop
        {
            get
            {
                return tokenShop;
            }
            set
            {
                tokenShop = value;
            }
        }
        [D2OIgnore]
        public List<AnimFunNpcData> AnimFunList
        {
            get
            {
                return animFunList;
            }
            set
            {
                animFunList = value;
            }
        }
        [D2OIgnore]
        public bool FastAnimsFun
        {
            get
            {
                return fastAnimsFun;
            }
            set
            {
                fastAnimsFun = value;
            }
        }
        [D2OIgnore]
        public bool TooltipVisible
        {
            get
            {
                return tooltipVisible;
            }
            set
            {
                tooltipVisible = value;
            }
        }

    }}
