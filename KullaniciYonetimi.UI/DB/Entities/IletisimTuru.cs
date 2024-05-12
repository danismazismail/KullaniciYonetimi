using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.Entities
{
    public class IletisimTuru
    {
        public int IletisimTuruID { get; set; }
        public string Adi { get; set; }


        public virtual List<PersonelIletisim> PersonelIletisim { get; set; }
    }
}
