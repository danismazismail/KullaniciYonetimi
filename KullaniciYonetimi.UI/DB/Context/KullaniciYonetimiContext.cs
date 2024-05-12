using KullaniciYonetimi.UI.DB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KullaniciYonetimi.UI.DB.Context
{
    //KullaniciYonetimiDB
    public class KullaniciYonetimiContext :DbContext
    {
        public KullaniciYonetimiContext():base("conn")
        {
            //conn
        }

        public DbSet<Personel> Personel { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<IletisimTuru> IletisimTuru { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<PersonelIletisim> PersonelIletisim { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<>().
            //constraint
            //relations
            //fluentApi
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
