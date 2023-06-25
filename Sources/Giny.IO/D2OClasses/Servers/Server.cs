using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Server", "")]
    public class Server : IDataObject , IIndexedData
    {        public const string MODULE = "Servers";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint commentId;
        public double openingDate;
        public string language;
        public int populationId;
        public uint gameTypeId;
        public int communityId;
        public List<string> restrictedToLanguages;
        public bool monoAccount;

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
        public uint CommentId
        {
            get
            {
                return commentId;
            }
            set
            {
                commentId = value;
            }
        }
        [D2OIgnore]
        public double OpeningDate
        {
            get
            {
                return openingDate;
            }
            set
            {
                openingDate = value;
            }
        }
        [D2OIgnore]
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }
        [D2OIgnore]
        public int PopulationId
        {
            get
            {
                return populationId;
            }
            set
            {
                populationId = value;
            }
        }
        [D2OIgnore]
        public uint GameTypeId
        {
            get
            {
                return gameTypeId;
            }
            set
            {
                gameTypeId = value;
            }
        }
        [D2OIgnore]
        public int CommunityId
        {
            get
            {
                return communityId;
            }
            set
            {
                communityId = value;
            }
        }
        [D2OIgnore]
        public List<string> RestrictedToLanguages
        {
            get
            {
                return restrictedToLanguages;
            }
            set
            {
                restrictedToLanguages = value;
            }
        }
        [D2OIgnore]
        public bool MonoAccount
        {
            get
            {
                return monoAccount;
            }
            set
            {
                monoAccount = value;
            }
        }

    }}
