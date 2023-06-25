using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ServerCommunity", "")]
    public class ServerCommunity : IDataObject , IIndexedData
    {        public const string MODULE = "ServerCommunities";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public string shortId;
        public List<string> defaultCountries;
        public List<int> supportedLangIds;
        public int namingRulePlayerNameId;
        public int namingRuleGuildNameId;
        public int namingRuleAllianceNameId;
        public int namingRuleAllianceTagId;
        public int namingRulePartyNameId;
        public int namingRuleMountNameId;
        public int namingRuleNameGeneratorId;
        public int namingRuleAdminId;
        public int namingRuleModoId;
        public int namingRulePresetNameId;

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
        public string ShortId
        {
            get
            {
                return shortId;
            }
            set
            {
                shortId = value;
            }
        }
        [D2OIgnore]
        public List<string> DefaultCountries
        {
            get
            {
                return defaultCountries;
            }
            set
            {
                defaultCountries = value;
            }
        }
        [D2OIgnore]
        public List<int> SupportedLangIds
        {
            get
            {
                return supportedLangIds;
            }
            set
            {
                supportedLangIds = value;
            }
        }
        [D2OIgnore]
        public int NamingRulePlayerNameId
        {
            get
            {
                return namingRulePlayerNameId;
            }
            set
            {
                namingRulePlayerNameId = value;
            }
        }
        [D2OIgnore]
        public int NamingRuleGuildNameId
        {
            get
            {
                return namingRuleGuildNameId;
            }
            set
            {
                namingRuleGuildNameId = value;
            }
        }
        [D2OIgnore]
        public int NamingRuleAllianceNameId
        {
            get
            {
                return namingRuleAllianceNameId;
            }
            set
            {
                namingRuleAllianceNameId = value;
            }
        }
        [D2OIgnore]
        public int NamingRuleAllianceTagId
        {
            get
            {
                return namingRuleAllianceTagId;
            }
            set
            {
                namingRuleAllianceTagId = value;
            }
        }
        [D2OIgnore]
        public int NamingRulePartyNameId
        {
            get
            {
                return namingRulePartyNameId;
            }
            set
            {
                namingRulePartyNameId = value;
            }
        }
        [D2OIgnore]
        public int NamingRuleMountNameId
        {
            get
            {
                return namingRuleMountNameId;
            }
            set
            {
                namingRuleMountNameId = value;
            }
        }
        [D2OIgnore]
        public int NamingRuleNameGeneratorId
        {
            get
            {
                return namingRuleNameGeneratorId;
            }
            set
            {
                namingRuleNameGeneratorId = value;
            }
        }
        [D2OIgnore]
        public int NamingRuleAdminId
        {
            get
            {
                return namingRuleAdminId;
            }
            set
            {
                namingRuleAdminId = value;
            }
        }
        [D2OIgnore]
        public int NamingRuleModoId
        {
            get
            {
                return namingRuleModoId;
            }
            set
            {
                namingRuleModoId = value;
            }
        }
        [D2OIgnore]
        public int NamingRulePresetNameId
        {
            get
            {
                return namingRulePresetNameId;
            }
            set
            {
                namingRulePresetNameId = value;
            }
        }

    }}
