using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductDal _productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void SearchProducts(string key) //arama kısmını oluşturmak için
        {
            //var result = _productDal.GetAll().Where(p=>p.Name.ToLower().Contains(key.ToLower())).ToList(); //burada veri tabanını sorgulayıp listeye geçiş yapmışız
            //where = nerden yararlanacağımız p = her bir elemanın p.Name'i kapsar(key)'i .ToList = result olarak liste şeklinde göstermesi için
            
            var result = _productDal.GetByName(key); //burada productDal kısmında yaptığımız search ile ilgili (LINQ) kısma sorguyu göndermiş olduk ve büyük küçük harfleri de kapsayan bir arama sistemi kurmuş olduk
            dgwProducts.DataSource = result;  


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)
            });
            LoadProducts();
            MessageBox.Show("Added!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productDal.Update(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = tbxNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text)
            });
            LoadProducts();
            MessageBox.Show("Updated!");
        }

        private void dgwProducts_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString(); 
            //dpwproducts'ın seçili olan satırının hücrelerinden birincisinin değerini stringe çevir demek
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString(); //update kısmına yazmış olduk
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value)
            });
            MessageBox.Show("Deleted!");
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProducts(tbxSearch.Text);
        }

        private void tbxGetById_Click(object sender, EventArgs e)
        {
            _productDal.GetById(3); //eğer buraya hangi id'yi yollarsak productDal'ın alt tabanına onun bilgilerini yollar eğer öyle bir id yoksa "null" yazar
        }
    }
}
