using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class PersonelIletisimSelectDTO
    {
        public int PersonelIletisimID { get; set; }
        public Guid PersonelID { get; set; }
        public int IletisimTuruID { get; set; }
        public string IletisimTuruAdi { get; set; }
        public string Bilgi { get; set; }
        public bool? AktifMi { get; set; }
    }
}
