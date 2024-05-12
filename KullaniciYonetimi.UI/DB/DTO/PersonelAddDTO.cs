using KullaniciYonetimi.UI.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DTO
{
    public class PersonelAddDTO
    {
        //personel Tablosuna   adSoyad-Tc-KullanıcıID-AktifMi

        public string AdSoyad { get; set; }
        public string TC { get; set; }
        public int KullaniciID { get; set; }
        public bool AktifMi { get; set; } = true;
    }
}
