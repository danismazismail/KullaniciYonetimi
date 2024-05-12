using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class PersonelIletisimTuruDTO
    {
        public string _mail;
        public string Mail { get { return _mail; } set { _mail = value.ToLower(); } }
        public string Tel { get; set; }
        public string Adres { get; set; }

        public override string ToString()
        {
            return (Mail,Tel,Adres).ToString();
        }
    }
}
