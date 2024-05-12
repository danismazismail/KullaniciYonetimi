using KullaniciYonetimi.UI.DB.Context;
using KullaniciYonetimi.UI.DB.DTO;
using KullaniciYonetimi.UI.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace KullaniciYonetimi.UI.DB.DAL
{
    public class PersonelDAL
    {
        public List<PersonelSelectDTO> AdmineGorePersonelSorgusu(bool aktifMi)
        {
            List<PersonelSelectDTO> personel = null;
            using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
            {
                personel = (from p in db.Personel
                            where p.AktifMi == aktifMi
                            select new PersonelSelectDTO
                            {
                                PersonelID = p.PersonelID,
                                AktifMi = p.AktifMi,
                                AdSoyad = p.AdSoyad,
                                KullaniciID = p.KullaniciID,
                                TC = p.TC,
                            }).ToList();
            }
            return personel;
        }

        public List<PersonelSelectDTO> KullaniciIdyeGorePersonelSorgusu(int sorgulayanKullanici)
        {
            List<PersonelSelectDTO> personel = null;
            using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
            {
                personel = (from p in db.Personel
                            where p.KullaniciID == sorgulayanKullanici && p.AktifMi == true
                            select new PersonelSelectDTO
                            {
                                PersonelID = p.PersonelID,
                                AktifMi = p.AktifMi,
                                AdSoyad = p.AdSoyad,
                                KullaniciID = p.KullaniciID,
                                TC = p.TC,
                            }).ToList();
            }
            return personel;
        }

        //private Personel eklenenPersonel = null;

        //public bool PersonelAdd(PersonelAddDTO personelAddDTO)
        //{
        //    bool sonuc = false;
        //    using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
        //    {
        //        eklenenPersonel = db.Personel.Add(new Personel()
        //        {
        //            AdSoyad = personelAddDTO.AdSoyad,
        //            KullaniciID = personelAddDTO.KullaniciID,
        //            TC = personelAddDTO.TC,
        //            AktifMi = personelAddDTO.AktifMi
        //        });
        //        sonuc = db.SaveChanges() > 0;
        //    }
        //    return sonuc;
        //}

        public bool PersonelVePersonelIlestisimAdd(PersonelAddDTO personelAddDTO, Dictionary<PersonelIletisimTuruDTO, int> personelIletisimTuru)
        {
            bool personelKaydiAlindiMi = false;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    KullaniciYonetimiContext db = new KullaniciYonetimiContext();
                    Personel eklenenPersonel = db.Personel.Add(new Personel()
                    {
                        PersonelID = Guid.NewGuid(),
                        AdSoyad = personelAddDTO.AdSoyad,
                        KullaniciID = personelAddDTO.KullaniciID,
                        TC = personelAddDTO.TC,
                        AktifMi = personelAddDTO.AktifMi
                    });
                    int row = db.SaveChanges();
                    if (row <= 0)
                        throw new Exception("Bilinmeyen bir hata oluştu");

                    foreach (var item in personelIletisimTuru)
                    {
                        db.PersonelIletisim.Add(new PersonelIletisim()
                        {
                            PersonelID = eklenenPersonel.PersonelID,
                            IletisimTuruID = item.Value,
                            Bilgi = item.Key.ToString(),
                            AktifMi = true
                        });
                        db.SaveChanges();
                    }
                    tran.Complete();
                    personelKaydiAlindiMi = true;
                }
                catch (Exception ex)
                {
                    //tran.Dispose();
                    personelKaydiAlindiMi = false;
                }
            }

            return personelKaydiAlindiMi;
        }

        //public void PersonelIletisimAdd(Dictionary<int, PersonelIletisimTuruDTO> personelIletisimTuru, Personel personelID)
        //{
        //    using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
        //    {
        //        foreach (var item in personelIletisimTuru)
        //        {
        //            db.PersonelIletisim.Add(new PersonelIletisim()
        //            {
        //                PersonelID = eklenenPersonel.PersonelID,
        //                IletisimTuruID = item.Key,
        //                Bilgi = item.Value.ToString(),
        //                AktifMi = true
        //            });
        //        }
        //        db.SaveChanges();
        //    }
        //}

        public bool PersonelDel(PersonelSelectDTO personelSelectDTO)
        {
            bool sonuc = false;
            using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
            {
                var personel = db.Personel.Where(a => a.PersonelID == personelSelectDTO.PersonelID).SingleOrDefault();
                db.PersonelIletisim.Where(a => a.PersonelID == personel.PersonelID).SingleOrDefault().AktifMi = false;
                personel.AktifMi = false;
                sonuc = db.SaveChanges() > 0;
            }
            return sonuc;
        }

        public void PersonelUpdate(PersonelSelectDTO personelSelectDTO, PersonelIletisimTuruDTO personelIletisimTuruDTO)
        {
            using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
            {
                var personel = db.Personel.Where(a => a.PersonelID == personelSelectDTO.PersonelID).Select(a => new Personel()
                {
                    PersonelID = personelSelectDTO.PersonelID,
                    AdSoyad = personelSelectDTO.AdSoyad,
                    AktifMi = personelSelectDTO.AktifMi,
                    KullaniciID = personelSelectDTO.KullaniciID,
                    TC = personelSelectDTO.TC,
                }).SingleOrDefault();

                //db.PersonelIletisim.Where(a => a.PersonelID == personel.PersonelID).Select(a=> new PersonelIletisim()
                //{
                      
                //});
            }
        }
    }
}