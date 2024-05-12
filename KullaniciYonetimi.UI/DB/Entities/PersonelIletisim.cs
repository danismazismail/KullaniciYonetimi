using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.Entities
{
    public class PersonelIletisim
    {
        public int PersonelIletisimID { get; set; }
        public Guid PersonelID { get; set; }
        public int IletisimTuruID { get; set; }
        public string Bilgi { get; set; }
        public bool? AktifMi { get; set; } = true;



        [ForeignKey("PersonelID")]
        public virtual Personel Personel { get; set; }
        [ForeignKey("IletisimTuruID")]
        public virtual IletisimTuru IletisimTuru { get; set; }

    }
}
