using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RSSOkuma
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btngetir_Click(object sender, EventArgs e)
        {
            XMLOKU();
        }

        private void XMLOKU()
        {
            //her item bir haber item içini liste ile doldurucaz
            XDocument kaynak = XDocument.Load(txturl.Text);
            List<XElement> liste = kaynak.Descendants("item").ToList();
            List<Haber> haberlistesi = new List<Haber>();

            //foreach ile bu liste içinde dönüyor ve h nesnesinden alınan her bilgi title,link nesnelerine atılıyor.
            foreach (var item in liste)
            {
                Haber h = new Haber();
                h.baslik = item.Element("title").Value;
                h.link = item.Element("link").Value;
                h.aciklama = item.Element("description").Value;

                haberlistesi.Add(h);
            }
            lsthaber.DataSource = haberlistesi;
        }

        private void lsthaber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Haber secilenhaber = (Haber)lsthaber.SelectedItem;

            webBrowser1.DocumentText = secilenhaber.aciklama;
        }
    }
}
