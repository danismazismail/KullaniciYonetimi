using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class PersonelIletisimTuruDTO
    {
        public string Mail { get; set; }
        public string Tel { get; set; }
        public string Adres { get; set; }

        public override string ToString()
        {
            return (Mail,Tel,Adres).ToString();
        }
    }
}
