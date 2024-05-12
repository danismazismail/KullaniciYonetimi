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

        public bool PersonelVePersonelIlestisimAdd(PersonelDenemeAddDTO personelDenemeAddDTO)
        {
            bool personelKaydiAlindiMi = false;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    KullaniciYonetimiContext db = new KullaniciYonetimiContext();
                    var personel = db.Personel.Where(a => a.AdSoyad == personelDenemeAddDTO.AdSoyad && a.TC == personelDenemeAddDTO.TC).SingleOrDefault();
                    if (personel == null)
                    {
                        Personel eklenenPersonel = db.Personel.Add(new Personel()
                        {
                            PersonelID = Guid.NewGuid(),
                            AdSoyad = personelDenemeAddDTO.AdSoyad,
                            KullaniciID = personelDenemeAddDTO.KullaniciID,
                            TC = personelDenemeAddDTO.TC,
                            AktifMi = personelDenemeAddDTO.AktifMi
                        });
                        int row = db.SaveChanges();
                        //System.Threading.Thread.Sleep(60000);
                        if (row <= 0)
                            throw new Exception("Bilinmeyen bir hata oluştu");

                        foreach (var item in personelDenemeAddDTO.PersonelIletisimTuruDTO)
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
                        personelKaydiAlindiMi = true;
                        tran.Complete();
                    }
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
            //bool sonuc = false;
            //using (KullaniciYonetimiContext db = new KullaniciYonetimiContext())
            //{
            //
            //    var personelIletisim = db.PersonelIletisim.Where(a => a.PersonelID == personel.PersonelID).SingleOrDefault();
            //    personel.AktifMi = false;
            //    if (personelIletisim != null)
            //        personelIletisim.AktifMi = false;
            //    sonuc = db.SaveChanges() > 0;
            //}
            //return sonuc;

            bool personelSilindiMi = false;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    KullaniciYonetimiContext db = new KullaniciYonetimiContext();
                    db.Personel.Where(a => a.PersonelID == personelSelectDTO.PersonelID).SingleOrDefault().AktifMi = false;
                    int row = db.SaveChanges();
                    if (row <= 0)
                        throw new Exception("Bilinmeyen bir hata oluştu.");

                    var personelIletisim = db.PersonelIletisim.Where(a => a.PersonelID == personelSelectDTO.PersonelID).ToList();
                    foreach (var item in personelIletisim)
                    {
                        item.AktifMi = false;
                        db.SaveChanges();
                    }
                    personelSilindiMi = true;
                    tran.Complete();
                }
                catch (Exception ex)
                {
                    personelSilindiMi = false;
                    tran.Dispose(); throw ex;
                }
            }
            return personelSilindiMi;
        }

        public bool PersonelUpdate(PersonelDenemeUpdateDTO personelDenemeUpdateDTO)
        {
            bool personelGuncellendiMi = false;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    KullaniciYonetimiContext db = new KullaniciYonetimiContext();

                    var personel = db.Personel.Where(a => a.PersonelID == personelDenemeUpdateDTO.PersonelSelect.PersonelID).SingleOrDefault();
                    if (personel.PersonelID == personelDenemeUpdateDTO.PersonelSelect.PersonelID)
                    {
                        personel.KullaniciID = personelDenemeUpdateDTO.PersonelSelect.KullaniciID;
                        personel.AktifMi = true;
                        personel.AdSoyad = personelDenemeUpdateDTO.PersonelSelect.AdSoyad;
                        personel.TC = personelDenemeUpdateDTO.PersonelSelect.TC;
                    }
                    int row = db.SaveChanges();
                    if (row == 0)
                        throw new Exception("Bilinmeyen bir hata oluştu.");

                    var list = db.PersonelIletisim.Where(a => a.PersonelID == personelDenemeUpdateDTO.PersonelSelect.PersonelID).ToList();
                    foreach (var item in list)
                    {
                        item.AktifMi = false;
                    }
                    foreach (var item in personelDenemeUpdateDTO.PersonelIletisimTuru)
                    {
                        db.PersonelIletisim.Add(new PersonelIletisim()
                        {
                            PersonelID = personelDenemeUpdateDTO.PersonelSelect.PersonelID,
                            IletisimTuruID = item.Value,
                            Bilgi = item.Key.ToString(),
                            AktifMi = true,
                        });
                        db.SaveChanges();
                    }
                    personelGuncellendiMi = true;
                    tran.Complete();
                }
                catch (Exception)
                {
                    tran.Dispose();
                    personelGuncellendiMi = false;
                }
            }
            return personelGuncellendiMi;
        }
    }
}