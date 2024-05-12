using KullaniciYonetimi.UI.DB.DAL;
using KullaniciYonetimi.UI.DB.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KullaniciYonetimi.UI
{
    public partial class PersonelYonetimPaneli : Form
    {
        private KullaniciDTO kullanici = null;

        public PersonelYonetimPaneli(KullaniciDTO girisYapanKullanici, string kullaniciRolu)
        {
            InitializeComponent();
            lblKullaniciTuru.Text = kullaniciRolu;
            kullanici = girisYapanKullanici;
            lblSakliAdres.Visible = false;
            lblSakliMail.Visible = false;
            lblSakliTel.Visible = false;
            cmbPersonelAdres.Visible = false;
            cmbPersonelMail.Visible = false;
            cmbPersonelTel.Visible = false;
        }

        private void PersonelYonetimPaneli_Load(object sender, EventArgs e)
        {
            if (lblKullaniciTuru.Text != "admin")
            {
                btnPersonelGuncelle.Visible = false;
                btnPersonelSil.Visible = false;
                radioButton1.Visible = false;
            }
        }

        private Dictionary<PersonelIletisimTuruDTO, int> personelIletisimTuru = new Dictionary<PersonelIletisimTuruDTO, int>();

        private void btnMailEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMail.Text) && txtMail.Text.Contains("@"))
            {
                lblPersonelMailAdedi.Visible = true;
                lblPersonelMailAdedi.Text = (Convert.ToInt32(lblPersonelMailAdedi.Text) + 1).ToString();
                PersonelIletisimTuruDTO personelIletisimTuruDTO = new PersonelIletisimTuruDTO();
                personelIletisimTuruDTO.Mail = txtMail.Text;
                personelIletisimTuru.Add(personelIletisimTuruDTO, 2);
                txtMail.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Mail alanını eksiksiz doldurunuz.");
            }
        }

        private void btnTelEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mstTel.Text))
            {
                lblPersonelTelAdedi.Visible = true;
                lblPersonelTelAdedi.Text = (Convert.ToInt32(lblPersonelTelAdedi.Text) + 1).ToString();
                PersonelIletisimTuruDTO personelIletisimTuruDTO = new PersonelIletisimTuruDTO();
                personelIletisimTuruDTO.Tel = mstTel.Text;
                personelIletisimTuru.Add(personelIletisimTuruDTO, 1);
                mstTel.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Telefon alanını eksiksiz doldurunuz.");
            }
        }

        private void btnAdresEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAdres.Text))
            {
                lblPersonelAdresAdedi.Visible = true;
                lblPersonelAdresAdedi.Text = (Convert.ToInt32(lblPersonelAdresAdedi.Text) + 1).ToString();
                PersonelIletisimTuruDTO personelIletisimTuruDTO = new PersonelIletisimTuruDTO();
                personelIletisimTuruDTO.Adres = txtAdres.Text;
                personelIletisimTuru.Add(personelIletisimTuruDTO, 3);
                txtAdres.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Adres alanını eksiksiz doldurunuz.");
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btnPersonelAra_Click(object sender, EventArgs e)
        {
            cmbPersonelAdres.SelectedItem = null;
            cmbPersonelMail.SelectedItem = null;
            cmbPersonelTel.SelectedItem = null;
            //lstPersonelListesi.Items.Clear();
            if (kullanici.RolID == 1)
            {
                //admin'e göre arama metodu olacak
                PersonelDAL personelDAL = new PersonelDAL();
                List<PersonelSelectDTO> personelSelectDTO = personelDAL.AdmineGorePersonelSorgusu(aktifOlanPersonelListesi);
                lstPersonelListesi.DataSource = personelSelectDTO;
            }
            else
            {
                //kullanici ID 'ye göre arama metodu olacak
                PersonelDAL personelDAL = new PersonelDAL();
                List<PersonelSelectDTO> personelSelectDTO = personelDAL.KullaniciIdyeGorePersonelSorgusu(kullanici.KullaniciID);
                lstPersonelListesi.DataSource = personelSelectDTO;
            }
        }

        public void AlanTemizligi()
        {
            txtAdSoyad.Text = string.Empty;
            //txtAranacakPersonel.Text = string.Empty;
            mstTc.Text = string.Empty;
            mstTel.Text = string.Empty;
            txtAdres.Text = string.Empty;
            txtMail.Text = string.Empty;
        }

        private bool aktifOlanPersonelListesi = false;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            aktifOlanPersonelListesi = radioButton1.Checked;
        }

        private void btnPersonelEkle_Click(object sender, EventArgs e)
        {
            btnMailEkle_Click(sender, e);
            btnTelEkle_Click(sender, e);
            btnAdresEkle_Click(sender, e);

            //personel Tablosuna   adSoyad-Tc-KullanıcıID-AktifMi

            //PersonelIletisim Tablosuna  PersonelID -  IletisimTuruID  -  Bilgi  -  AktifMi

            //ForeingKey'den kaynaklı once personel kayıt olacak onun ıdsini alıp personel iletişime oyle kayıt yapılacak

            if (!string.IsNullOrWhiteSpace(txtAdSoyad.Text) && !string.IsNullOrWhiteSpace(mstTc.Text) && personelIletisimTuru.Count > 0)
            {
                PersonelDenemeAddDTO personelDenemeAddDTO = new PersonelDenemeAddDTO();
                personelDenemeAddDTO.AdSoyad = txtAdSoyad.Text;
                personelDenemeAddDTO.TC = mstTc.Text;
                personelDenemeAddDTO.KullaniciID = kullanici.KullaniciID;
                personelDenemeAddDTO.PersonelIletisimTuruDTO = personelIletisimTuru;

                //PersonelAddDTO personelAddDTO = new PersonelAddDTO();
                //personelAddDTO.AdSoyad = txtAdSoyad.Text;
                //personelAddDTO.TC = mstTc.Text;
                //personelAddDTO.KullaniciID = kullanici.KullaniciID;
                PersonelDAL personelDAL = new PersonelDAL();
                var sonuc = personelDAL.PersonelVePersonelIlestisimAdd(personelDenemeAddDTO);
                if (sonuc)
                    MessageBox.Show($"{personelDenemeAddDTO.AdSoyad} Personel Listesine Başarı ile Eklenmştir.");
                else
                    MessageBox.Show("Bir hata oluştu tekrar deneyiniz.");
            }
            else
            {
                MessageBox.Show("Lütfen alanları eksiksiz doldurunuz.");
            }
            lstPersonelListesi.Items.Clear();
            btnPersonelAra_Click(sender, e);
        }

        private PersonelSelectDTO personelSelectDTO = null;
        private List<PersonelIletisimSelectDTO> personelIletisimSelectDTO = null;

        private void lstPersonelListesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSakliAdres.Visible = true;
            lblSakliMail.Visible = true;
            lblSakliTel.Visible = true;
            cmbPersonelAdres.Visible = true;
            cmbPersonelMail.Visible = true;
            cmbPersonelTel.Visible = true;
            cmbPersonelAdres.Items.Clear();
            cmbPersonelMail.Items.Clear();
            cmbPersonelTel.Items.Clear();
            //if (lstPersonelListesi.SelectedItem != lstPersonelListesi.SelectedItem)
            //{
            //}

            personelSelectDTO = (PersonelSelectDTO)lstPersonelListesi.SelectedItem;
            personelSelectDTO.PersonelID = ((PersonelSelectDTO)lstPersonelListesi.SelectedItem).PersonelID;
            txtAdSoyad.Text = personelSelectDTO.AdSoyad = ((PersonelSelectDTO)lstPersonelListesi.SelectedItem).AdSoyad;
            mstTc.Text = personelSelectDTO.TC = ((PersonelSelectDTO)lstPersonelListesi.SelectedItem).TC;
            personelSelectDTO.AktifMi = ((PersonelSelectDTO)lstPersonelListesi.SelectedItem).AktifMi;
            personelSelectDTO.KullaniciID = ((PersonelSelectDTO)lstPersonelListesi.SelectedItem).KullaniciID;

            PersonelIletisimDAL personelIletisimDAL = new PersonelIletisimDAL();
            personelIletisimSelectDTO = personelIletisimDAL.PersonelIletisimBilgileri(personelSelectDTO.PersonelID);

            foreach (var item in personelIletisimSelectDTO)
            {
                if (item.IletisimTuruID == 1)
                    cmbPersonelTel.Items.Add(item.Bilgi);
                else if (item.IletisimTuruID == 2)
                    cmbPersonelMail.Items.Add(item.Bilgi);
                else
                    cmbPersonelAdres.Items.Add(item.Bilgi);
            }
        }

        private void cmbPersonelMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMail.Text = cmbPersonelMail.SelectedItem.ToString();
        }

        private void cmbPersonelTel_SelectedIndexChanged(object sender, EventArgs e)
        {
            mstTel.Text = cmbPersonelTel.SelectedItem.ToString();
        }

        private void cmbPersonelAdres_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAdres.Text = cmbPersonelAdres.SelectedItem.ToString();
        }

        private void btnPersonelGuncelle_Click(object sender, EventArgs e)
        {
            // Personel Id uzerinden yapacağımız için önce Id bul
            // Bulunan Idye göre önce personeli güncelle
            //Personel guncellenmesi tamamlandı ise personel iletisimi güncelle
            //Güncellemeyi sadece admin yapabiliyor o yüzden kullanıcı ID değişme ihtimali var giren kullanıcınında bilgisini tut
            //kullanıcı hiç değişmeden sadece iletişim güncellemesi bile olabilir metot tasarımı ona göre olmalı.

            //personel Tablosunda   adSoyad - Tc - KullanıcıID - AktifMi
            //PersonelIletisim Tablosuna  PersonelID -  IletisimTuruID  -  Bilgi  -  AktifMi
            //PersonelIletisimTuruDTO personelIletisimTuruDTO = new PersonelIletisimTuruDTO();
            //personelIletisimTuruDTO.Tel = mstTel.Text;
            //personelIletisimTuruDTO.Adres = txtAdres.Text;
            //personelIletisimTuruDTO.Mail = txtMail.Text;
            btnMailEkle_Click(sender, e);
            btnTelEkle_Click(sender, e);
            btnAdresEkle_Click(sender, e);
            PersonelDenemeUpdateDTO personelDenemeUpdateDTO = new PersonelDenemeUpdateDTO();
            personelDenemeUpdateDTO.PersonelSelect = personelSelectDTO;
            personelDenemeUpdateDTO.PersonelSelect.KullaniciID = personelSelectDTO.KullaniciID;
            personelDenemeUpdateDTO.PersonelSelect.AdSoyad = txtAdSoyad.Text;
            personelDenemeUpdateDTO.PersonelSelect.TC = mstTc.Text;
            personelDenemeUpdateDTO.PersonelIletisimTuru = personelIletisimTuru;

            PersonelDAL personelDAL = new PersonelDAL();
            var sonuc = personelDAL.PersonelUpdate(personelDenemeUpdateDTO);
            if (sonuc)
                MessageBox.Show($"{personelDenemeUpdateDTO.PersonelSelect.AdSoyad} İsimli personel güncellenmiştir.");
            else
                MessageBox.Show($"{personelDenemeUpdateDTO.PersonelSelect.AdSoyad} İsimli personel güncellenirken bir sorun oluştu tekrar deneyiniz.");

            lstPersonelListesi.Items.Clear();
            btnPersonelAra_Click(sender, e);
        }

        private void btnPersonelSil_Click(object sender, EventArgs e)
        {
            //Veri tabanımızın tasarımında silme işlemi yapılmadığı işin aktifmi kolonlarını false çekecegiz.
            //personel pasife alınmayıp sadece iletişimide pasife alınabilir metot tasarımında dikkate al
            //iletişim pasife sadece textbox boş gelirse alınsın.

            PersonelDAL personelDAL = new PersonelDAL();
            bool sonuc = personelDAL.PersonelDel(personelSelectDTO);
            MessageBox.Show(sonuc ? $"{personelSelectDTO.AdSoyad} İsimli personel listeden başarı ile silinmiştir." : "Bir hata oluştu tekrar deneyiniz.");

            lstPersonelListesi.Items.Clear();
            btnPersonelAra_Click(sender, e);
        }
    }
}