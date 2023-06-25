using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("OptionalFeature", "")]
    public class OptionalFeature : IDataObject , IIndexedData
    {        public const string MODULE = "OptionalFeatures";

        public int Id => (int)id;

        public int id;
        public string keyword;
        public bool isClient;
        public bool isServer;
        public bool isActivationOnLaunch;
        public bool isActivationOnServerConnection;
        public string activationCriterions;

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
        public string Keyword
        {
            get
            {
                return keyword;
            }
            set
            {
                keyword = value;
            }
        }
        [D2OIgnore]
        public bool IsClient
        {
            get
            {
                return isClient;
            }
            set
            {
                isClient = value;
            }
        }
        [D2OIgnore]
        public bool IsServer
        {
            get
            {
                return isServer;
            }
            set
            {
                isServer = value;
            }
        }
        [D2OIgnore]
        public bool IsActivationOnLaunch
        {
            get
            {
                return isActivationOnLaunch;
            }
            set
            {
                isActivationOnLaunch = value;
            }
        }
        [D2OIgnore]
        public bool IsActivationOnServerConnection
        {
            get
            {
                return isActivationOnServerConnection;
            }
            set
            {
                isActivationOnServerConnection = value;
            }
        }
        [D2OIgnore]
        public string ActivationCriterions
        {
            get
            {
                return activationCriterions;
            }
            set
            {
                activationCriterions = value;
            }
        }

    }}
