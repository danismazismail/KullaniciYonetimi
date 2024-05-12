using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.Entities
{
    public class Kullanici
    {
        public int KullaniciID { get; set; }
        public string KullaniciMail { get; set; }
        public string KullaniciSifre { get; set; }
        public int RolID { get; set; }

        [ForeignKey("RolID")]
        public virtual Rol Rol { get; set; }

        public virtual List<Personel> Personel { get; set; }
    }
}
