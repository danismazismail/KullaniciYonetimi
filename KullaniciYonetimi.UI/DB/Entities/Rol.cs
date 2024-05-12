using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.Entities
{
    public class Rol
    {
        public int RolID { get; set; }
        public string RolAdi { get; set; }


        public virtual List<Kullanici> Kullanici { get; set; }
    }
}
