using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Document", "")]
    public class Document : IDataObject , IIndexedData
    {        public const string MODULE = "Documents";

        public int Id => (int)id;

        public int id;
        public uint typeId;
        public bool showTitle;
        public bool showBackgroundImage;
        public uint titleId;
        public uint authorId;
        public uint subTitleId;
        public uint contentId;
        public string contentCSS;
        public string clientProperties;

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
        public bool ShowTitle
        {
            get
            {
                return showTitle;
            }
            set
            {
                showTitle = value;
            }
        }
        [D2OIgnore]
        public bool ShowBackgroundImage
        {
            get
            {
                return showBackgroundImage;
            }
            set
            {
                showBackgroundImage = value;
            }
        }
        [D2OIgnore]
        public uint TitleId
        {
            get
            {
                return titleId;
            }
            set
            {
                titleId = value;
            }
        }
        [D2OIgnore]
        public uint AuthorId
        {
            get
            {
                return authorId;
            }
            set
            {
                authorId = value;
            }
        }
        [D2OIgnore]
        public uint SubTitleId
        {
            get
            {
                return subTitleId;
            }
            set
            {
                subTitleId = value;
            }
        }
        [D2OIgnore]
        public uint ContentId
        {
            get
            {
                return contentId;
            }
            set
            {
                contentId = value;
            }
        }
        [D2OIgnore]
        public string ContentCSS
        {
            get
            {
                return contentCSS;
            }
            set
            {
                contentCSS = value;
            }
        }
        [D2OIgnore]
        public string ClientProperties
        {
            get
            {
                return clientProperties;
            }
            set
            {
                clientProperties = value;
            }
        }

    }}
