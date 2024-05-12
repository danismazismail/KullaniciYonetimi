using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class PersonelSelectDTO
    {
        public Guid PersonelID { get; set; }
        public string AdSoyad { get; set; }
        public string TC { get; set; }
        public int KullaniciID { get; set; }
        public bool AktifMi { get; set; }

        public override string ToString()
        {
            return AdSoyad +" - "+TC;
        }
    }
}
