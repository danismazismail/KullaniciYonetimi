using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class PersonelDenemeAddDTO
    {
        public string AdSoyad { get; set; }
        public string TC { get; set; }
        public int KullaniciID { get; set; }
        public bool AktifMi { get; set; } = true;
        public Dictionary<PersonelIletisimTuruDTO,int> PersonelIletisimTuruDTO { get; set; }
    }
}
