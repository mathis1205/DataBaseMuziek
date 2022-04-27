using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMuziek
{
    internal class muziek
    {
        public int MuziekID { get; set; }
        public string Liedje { get; set; }
        public string Duur { get; set; }
        public string Beoordeling { get; set; }
        public int TaalID { get; set; }
        public int FormaatID { get; set; }
        public int GenreID { get; set; }
        public int AlbumID { get; set; }
    }
}
