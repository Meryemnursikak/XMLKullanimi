using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XMLKullanimi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlTextWriter xmltext = new XmlTextWriter("C:\\XML\\musteriler.xml",
                System.Text.UTF8Encoding.UTF8);
            xmltext.WriteComment("Yorum Satırı");
            xmltext.WriteStartElement("Müşteriler"); //başlangıç için
            xmltext.WriteStartElement("Müşteri");    //aynı yine
            xmltext.WriteAttributeString("id", "1");  //müsteri yanında id= 1 yazıyorsa attribute yani bağlı demek olur
            xmltext.WriteElementString("Ad","Meryem"); //bir element
            xmltext.WriteElementString("Soyad", "Şikak");
            xmltext.WriteEndElement(); //elementi bitirdik xml kapattık.
            xmltext.WriteEndElement();
            xmltext.Close();

            MessageBox.Show("XML Dosya Oluşturuldu.");

        }

        private void button2_Click(object sender, EventArgs e)
        {
           //değişkene aldık döngüde kullanmak için
           XmlReader dosya= XmlReader.Create("C:\\XML\\musteriler.xml");

            while (dosya.Read())
            {
                //MessageBox.Show(dosya.Name.ToString()+dosya.Value.ToString());
                listBox1.Items.Add(dosya.Name.ToString() + dosya.Value.ToString());
            }
        }

        //linq ile yazımı
        private void button3_Click(object sender, EventArgs e)
        {
            XDocument dosya = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("Öğrenciler",
                new XComment("****Yorum Satırı****"), //yorum satırı komutu
                new XElement("Öğrenci", 
                new XAttribute("ID","1"),
                new XElement("ad", "Nur"),
                new XElement("soyad", "Şikak"))));

            dosya.Save("C:\\XML\\musteriler.xml");
            MessageBox.Show("LinQ Dosya Oluşturuldu.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();

            for (int i = 1; i < 11; i++)
            {
                Ogrenci ogr = new Ogrenci();
                ogr.id = i;
                ogr.ad = FakeData.NameData.GetFirstName();
                ogr.soyad = FakeData.NameData.GetSurname();
                ogrenciler.Add(ogr);
            }

            XDocument dosya = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("Ogrenciler", ogrenciler.Select(x => new XElement("Ogrenci",
                 new XElement("ID", x.id),
                 new XElement("AD", x.ad),
                 new XElement("SOYAD", x.soyad)))));
            dosya.Save("C:\\XML\\ogrenci.xml");

            MessageBox.Show("XML Dosya LinQ ile oluşturuldu.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XDocument dosya = XDocument.Load("C:\\XML\\ogrenci.xml");
            List<XElement> liste;
            liste = dosya.Descendants("Ogrenci").ToList(); //her bir ogrenci tagi arasında ad soyad..vs. yazıldığı için ogrenci deriz.
                                                           //okunduğu için tolist deriz tostring değil
            foreach (var item in liste)
            {
                listBox1.Items.Add(item.Element("ID").Value
                    +" "+item.Element("Soyad").Value)
                 

            }
        }


    }
}
