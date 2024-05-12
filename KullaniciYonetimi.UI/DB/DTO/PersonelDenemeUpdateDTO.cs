using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class PersonelDenemeUpdateDTO
    {
        public PersonelSelectDTO PersonelSelect { get; set; }
        public Dictionary<PersonelIletisimTuruDTO, int> PersonelIletisimTuru { get; set; }
    }
}
