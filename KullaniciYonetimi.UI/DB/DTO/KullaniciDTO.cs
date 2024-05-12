using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class KullaniciDTO
    {
        public int KullaniciID { get; set; }
        public string KullaniciMail { get; set; }
        public string KullaniciSifre { get; set; }
        public int RolID { get; set; }
    }
}
