using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastestWayProject.Enums;
using FastestWayProject.Interfaces;

namespace FastestWayProject.Models
{
    public class TagModel : ITag
    {
        public DataEnums TagType { get; }
        public string Value { get; }

        public TagModel(DataEnums tagType, string value)
        {
            TagType = tagType;
            Value = value;
        }
    }
}
