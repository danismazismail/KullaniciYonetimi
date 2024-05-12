using KullaniciYonetimi.UI.DB.Context;
using KullaniciYonetimi.UI.DB.DTO;
using KullaniciYonetimi.UI.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.DAL
{
    public class KullaniciDAL
    {
        public KullaniciDTO KullaniciRolSorgusu(KullaniciGirisiDTO kullaniciGirisiDTO)
        {
            using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
            {
                KullaniciDTO kullanici = db.Kullanici.Where(a => a.KullaniciMail == kullaniciGirisiDTO.KullaniciMail && a.KullaniciSifre == kullaniciGirisiDTO.KullaniciSifre).Select(a => new KullaniciDTO() 
                {
                    KullaniciID = a.KullaniciID,
                    KullaniciMail = a.KullaniciMail,
                    KullaniciSifre = a.KullaniciSifre,
                    RolID = a.RolID
                }).SingleOrDefault();

                return kullanici;
            }
        }
    }
}