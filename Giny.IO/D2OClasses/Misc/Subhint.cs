using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Subhint", "")]
    public class Subhint : IDataObject , IIndexedData
    {        public const string MODULE = "Subhints";

        public int Id => throw new NotImplementedException();

        public int hint_id;
        public string hint_parent_uid;
        public string hint_anchored_element;
        public int hint_anchor;
        public int hint_position_x;
        public int hint_position_y;
        public int hint_width;
        public int hint_height;
        public string hint_highlighted_element;
        public int hint_order;
        public uint hint_tooltip_text;
        public int hint_tooltip_position_enum;
        public string hint_tooltip_url;
        public int hint_tooltip_offset_x;
        public int hint_tooltip_offset_y;
        public int hint_tooltip_width;
        public double hint_creation_date;

        [D2OIgnore]
        public int Hint_id
        {
            get
            {
                return hint_id;
            }
            set
            {
                hint_id = value;
            }
        }
        [D2OIgnore]
        public string Hint_parent_uid
        {
            get
            {
                return hint_parent_uid;
            }
            set
            {
                hint_parent_uid = value;
            }
        }
        [D2OIgnore]
        public string Hint_anchored_element
        {
            get
            {
                return hint_anchored_element;
            }
            set
            {
                hint_anchored_element = value;
            }
        }
        [D2OIgnore]
        public int Hint_anchor
        {
            get
            {
                return hint_anchor;
            }
            set
            {
                hint_anchor = value;
            }
        }
        [D2OIgnore]
        public int Hint_position_x
        {
            get
            {
                return hint_position_x;
            }
            set
            {
                hint_position_x = value;
            }
        }
        [D2OIgnore]
        public int Hint_position_y
        {
            get
            {
                return hint_position_y;
            }
            set
            {
                hint_position_y = value;
            }
        }
        [D2OIgnore]
        public int Hint_width
        {
            get
            {
                return hint_width;
            }
            set
            {
                hint_width = value;
            }
        }
        [D2OIgnore]
        public int Hint_height
        {
            get
            {
                return hint_height;
            }
            set
            {
                hint_height = value;
            }
        }
        [D2OIgnore]
        public string Hint_highlighted_element
        {
            get
            {
                return hint_highlighted_element;
            }
            set
            {
                hint_highlighted_element = value;
            }
        }
        [D2OIgnore]
        public int Hint_order
        {
            get
            {
                return hint_order;
            }
            set
            {
                hint_order = value;
            }
        }
        [D2OIgnore]
        public uint Hint_tooltip_text
        {
            get
            {
                return hint_tooltip_text;
            }
            set
            {
                hint_tooltip_text = value;
            }
        }
        [D2OIgnore]
        public int Hint_tooltip_position_enum
        {
            get
            {
                return hint_tooltip_position_enum;
            }
            set
            {
                hint_tooltip_position_enum = value;
            }
        }
        [D2OIgnore]
        public string Hint_tooltip_url
        {
            get
            {
                return hint_tooltip_url;
            }
            set
            {
                hint_tooltip_url = value;
            }
        }
        [D2OIgnore]
        public int Hint_tooltip_offset_x
        {
            get
            {
                return hint_tooltip_offset_x;
            }
            set
            {
                hint_tooltip_offset_x = value;
            }
        }
        [D2OIgnore]
        public int Hint_tooltip_offset_y
        {
            get
            {
                return hint_tooltip_offset_y;
            }
            set
            {
                hint_tooltip_offset_y = value;
            }
        }
        [D2OIgnore]
        public int Hint_tooltip_width
        {
            get
            {
                return hint_tooltip_width;
            }
            set
            {
                hint_tooltip_width = value;
            }
        }
        [D2OIgnore]
        public double Hint_creation_date
        {
            get
            {
                return hint_creation_date;
            }
            set
            {
                hint_creation_date = value;
            }
        }

    }}
