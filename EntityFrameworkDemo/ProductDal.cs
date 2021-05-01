using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
    public class ProductDal
    {
        internal List<Product> GetAll()
        {
            using (ETradeContext context = new ETradeContext()) //bu belllekte çok fazla yer kaplar f12 ile IDisponsable kısmına ulaşabilirsiniz using ise çöp toplayıcılar kodu bitirmeden nesneyi zorla bellekten atıyosunuz az yer kaplamanızı sağlıyor (Dispoble etmek)
            {
                return context.Products.ToList(); //form1de yazdığımız kodu kesip buraya yapıştırdık ve sadece burayı değiştirdik
            }
        }

        internal List<Product> GetByName(string key) //veri tabanına sormak için 
        {
            using (ETradeContext context = new ETradeContext()) 
            {
                return context.Products.Where(p=>p.Name.Contains(key)).ToList(); //burada direkt veri tabanıyla işimiz bitmeden ürüne ulaşmışız (çok veri olduğunda daha verimli bir yöntem)
            }
        }

        internal List<Product> GetByUnitPrice(decimal price) //fiyat olarak aramak istersek 
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.Where(p => p.UnitPrice >= price).ToList();
                //burada veri tabanını sorgulayıp ürünleri filtreleyip fiyat sorgulamak için bir kod yazdık
            }
        }
        internal List<Product> GetByUnitPrice(decimal min, decimal max) //iki fiyat arası için filtreleme
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.Where(p => p.UnitPrice >= min && p.UnitPrice <= max).ToList();
                //burada veri tabanını sorgulayıp ürünleri filtreleyip iki fiyat arasındaki verileri almak için kod yazdık
            }
        }
        internal Product GetById(int id) //tek bir ürünü aramak için
        {
            using (ETradeContext context = new ETradeContext()) 
            {
               var result = context.Products.FirstOrDefault(p => p.Id == id);
                //FirstOrDefault= id'ye uygun olan ilk kaydı veya null getir yazdık ardından parametre içine p için p'nin idsi == kullanıcının gönderdiği id anlamında bir komut yazdık
                return result;
            }
        }

        internal void Add(Product product) //add ve get all daha erişilebilir olduğu için internal yapmak zorunda kaldım
        {
            using (ETradeContext context = new ETradeContext()) 
            {
                //context.Products.Add(product); bu da olur
                var entity = context.Entry(product); //bu da olur
                //context'e product için abone olup eşitle
                entity.State = EntityState.Added;
                //durumuna da ekle demek
                context.SaveChanges();
            }
        }
        internal void Update(Product product)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var entity = context.Entry(product);
                //context'e product için abone olup eşitle
                entity.State = EntityState.Modified;
                //durumunu da güncelle demek
                context.SaveChanges();
            }
        }

        internal void Delete(Product product)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var entity = context.Entry(product);
                //context'e product için abone olup eşitle
                entity.State = EntityState.Deleted;
                //durumunu da sil demek
                context.SaveChanges();
            }
        }

    }
}
