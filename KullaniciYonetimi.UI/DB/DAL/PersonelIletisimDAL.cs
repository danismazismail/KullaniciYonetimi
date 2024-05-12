using KullaniciYonetimi.UI.DB.Context;
using KullaniciYonetimi.UI.DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KullaniciYonetimi.UI.DB.DAL
{
    public class PersonelIletisimDAL
    {
        public List<PersonelIletisimSelectDTO> PersonelIletisimBilgileri(Guid gelenVeri)
        {
            List<PersonelIletisimSelectDTO> personelIletisimSelectDTO = null;
            using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
            {
                personelIletisimSelectDTO = (from p in db.PersonelIletisim
                                             join t in  db.IletisimTuru on p.IletisimTuruID equals t.IletisimTuruID
                                             where p.PersonelID == gelenVeri
                                             select new PersonelIletisimSelectDTO()
                                             {
                                                 PersonelID = p.PersonelID,
                                                 AktifMi = p.AktifMi,
                                                 Bilgi = p.Bilgi,
                                                 IletisimTuruID = p.IletisimTuruID,
                                                 PersonelIletisimID = p.PersonelIletisimID,
                                                 IletisimTuruAdi = t.Adi,
                                             }).ToList();
            }
            return personelIletisimSelectDTO;
        }
    }
}