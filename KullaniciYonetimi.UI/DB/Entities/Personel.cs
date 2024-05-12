using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.Entities
{
    public class Personel
    {
        [Key]
        public Guid PersonelID { get; set; }
        [Required]
        public string AdSoyad { get; set; }
        [Required]
        public string TC { get; set; }
        public int KullaniciID { get; set; }
        public bool AktifMi { get; set; }

        [ForeignKey("KullaniciID")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual List<PersonelIletisim> PersonelIletisim { get; set; }

    }
}
