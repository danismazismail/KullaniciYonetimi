using KullaniciYonetimi.UI.DB.DAL;
using KullaniciYonetimi.UI.DB.DTO;
using KullaniciYonetimi.UI.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KullaniciYonetimi.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// kullanici giriş formu hazırla
        /// Frm Personel formu yonlendir.
        /// rolune göre formun içerigi değişsin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string kullaniciRolu = "";
            if (!string.IsNullOrWhiteSpace(txtMail.Text) && !string.IsNullOrWhiteSpace(txtSifre.Text) && txtMail.Text.Contains("@"))
            {
                KullaniciGirisiDTO kullaniciGirisiDTO = new KullaniciGirisiDTO();
                kullaniciGirisiDTO.KullaniciMail = txtMail.Text.ToLower();
                kullaniciGirisiDTO.KullaniciSifre = txtSifre.Text;

                KullaniciDAL kullaniciDAL = new KullaniciDAL();
                KullaniciDTO kullanici = kullaniciDAL.KullaniciRolSorgusu(kullaniciGirisiDTO);

                if (kullanici == null)
                {
                    MessageBox.Show("Kullanici mail veya şifre hatalı.");
                    AlanlarıTemizle();
                    return;
                }
                else if (kullanici.RolID == 1)
                    kullaniciRolu = "admin";
                else
                    kullaniciRolu = "user";

                PersonelYonetimPaneli pyp = new PersonelYonetimPaneli(kullanici, kullaniciRolu);
                pyp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen gerekli alanları doldur.. Bu son uyarım.");
                AlanlarıTemizle();
            }
        }

        private void AlanlarıTemizle()
        {
            txtMail.Text = string.Empty;
            txtSifre.Text = string.Empty;
        }
    }
}