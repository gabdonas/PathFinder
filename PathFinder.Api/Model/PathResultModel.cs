using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PathFinder.Api.Model
{
    public class PathResult
    {
        [Key]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InputArrayString { get; set; }
        public bool IsTraversable { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ResultArrayString { get; set; }

        [NotMapped]
        public int[] InputArray
        {
            get => Utils.StrToArray(InputArrayString);
            set => InputArrayString = Utils.ArrayToStr(value);
        }
        [NotMapped]
        public int[] ResultArray
        {
            get => Utils.StrToArray(ResultArrayString);
            set => ResultArrayString = Utils.ArrayToStr(value);
        }


    }
}
