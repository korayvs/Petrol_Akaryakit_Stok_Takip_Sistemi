using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Test_Petrol_Akaryakit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=Test_Benzin;Integrated Security=True");

        void temizle()
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            TxtKursunsuzFiyat.Text = "";
            TxtDizelFiyat.Text = "";
            TxtProDizelFiyat.Text = "";
            TxtGazyagiFiyat.Text = "";
            TxtOtogazFiyat.Text = "";
            TxtLitre.Text = "";
        }

        void listele()
        {
            //Kurşunsuz95
            bgl.Open();
            SqlCommand cmd = new SqlCommand("Select * From TBLBENZIN Where PETROLTUR = 'Kurşunsuz95'", bgl);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LblKursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                LblKursunsuzLitre.Text = dr[4].ToString();
            }
            bgl.Close();

            //Dizel
            bgl.Open();
            SqlCommand cmd2 = new SqlCommand("Select * From TBLBENZIN Where PETROLTUR = 'Dizel'", bgl);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                LblDizel.Text = dr2[3].ToString();
                progressBar2.Value = int.Parse(dr2[4].ToString());
                LblDizelLitre.Text = dr2[4].ToString();
            }
            bgl.Close();

            //ProDizel
            bgl.Open();
            SqlCommand cmd3 = new SqlCommand("Select * From TBLBENZIN Where PETROLTUR = 'ProDizel'", bgl);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                LblProDizel.Text = dr3[3].ToString();
                progressBar3.Value = int.Parse(dr3[4].ToString());
                LblProDizelLitre.Text = dr3[4].ToString();
            }
            bgl.Close();

            //Gazyağı
            bgl.Open();
            SqlCommand cmd4 = new SqlCommand("Select * From TBLBENZIN Where PETROLTUR = 'Gazyağı'", bgl);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                LblGazyagi.Text = dr4[3].ToString();
                progressBar4.Value = int.Parse(dr4[4].ToString());
                LblGazyagiLitre.Text = dr4[4].ToString();
            }
            bgl.Close();

            //Otogaz
            bgl.Open();
            SqlCommand cmd5 = new SqlCommand("Select * From TBLBENZIN Where PETROLTUR = 'Otogaz'", bgl);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                LblOtogaz.Text = dr5[3].ToString();
                progressBar5.Value = int.Parse(dr5[4].ToString());
                LblOtogazLitre.Text = dr5[4].ToString();
            }
            bgl.Close();

            bgl.Open();
            SqlCommand cmd6 = new SqlCommand("Select * From TBLKASA", bgl);
            SqlDataReader dr6 = cmd6.ExecuteReader();
            while (dr6.Read())
            {
                LblKasa.Text = dr6[0].ToString();
            }
            bgl.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz, litre, tutar;
            kursunsuz = Convert.ToDouble(LblKursunsuz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value);
            tutar = kursunsuz * litre;
            TxtKursunsuzFiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double dizel, litre, tutar;
            dizel = Convert.ToDouble(LblDizel.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = dizel * litre;
            TxtDizelFiyat.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double prodizel, litre, tutar;
            prodizel = Convert.ToDouble(LblProDizel.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = prodizel * litre;
            TxtProDizelFiyat.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double gazyagi, litre, tutar;
            gazyagi = Convert.ToDouble(LblGazyagi.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = gazyagi * litre;
            TxtGazyagiFiyat.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double otogaz, litre, tutar;
            otogaz = Convert.ToDouble(LblOtogaz.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = otogaz * litre;
            TxtOtogazFiyat.Text = tutar.ToString();
        }

        private void BtnDepoDoldur_Click(object sender, EventArgs e)
        {
            //Kurşunsuz95
            if (numericUpDown1.Value != 0)
            {
                bgl.Open();
                SqlCommand cmd = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@p2", "Kurşunsuz95");
                cmd.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtKursunsuzFiyat.Text));
                cmd.ExecuteNonQuery();
                bgl.Close();                

                bgl.Open();
                SqlCommand cmd2 = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR + @p1", bgl);
                cmd2.Parameters.AddWithValue("@p1", decimal.Parse(TxtKursunsuzFiyat.Text));
                cmd2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd3 = new SqlCommand("Update TBLBENZIN Set STOK = STOK - @p1 Where PETROLTUR = 'Kurşunsuz95'", bgl);
                cmd3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                cmd3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Satış Yapıldı");
                listele();
                temizle();
            }

            else if (numericUpDown2.Value != 0)
            {
                //Dizel
                bgl.Open();
                SqlCommand cmd = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@p2", "Dizel");
                cmd.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtDizelFiyat.Text));
                cmd.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd2 = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR + @p1", bgl);
                cmd2.Parameters.AddWithValue("@p1", decimal.Parse(TxtDizelFiyat.Text));
                cmd2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd3 = new SqlCommand("Update TBLBENZIN Set STOK = STOK - @p1 Where PETROLTUR = 'Dizel'", bgl);
                cmd3.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                cmd3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Satış Yapıldı");
                listele();
                temizle();
            }

            else if (numericUpDown3.Value != 0)
            {
                //ProDizel
                bgl.Open();
                SqlCommand cmd = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@p2", "ProDizel");
                cmd.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtProDizelFiyat.Text));
                cmd.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd2 = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR + @p1", bgl);
                cmd2.Parameters.AddWithValue("@p1", decimal.Parse(TxtProDizelFiyat.Text));
                cmd2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd3 = new SqlCommand("Update TBLBENZIN Set STOK = STOK - @p1 Where PETROLTUR = 'ProDizel'", bgl);
                cmd3.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                cmd3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Satış Yapıldı");
                listele();
                temizle();
            }

            else if (numericUpDown4.Value != 0)
            {
                //Gazyağı
                bgl.Open();
                SqlCommand cmd = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@p2", "Gazyağı");
                cmd.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtGazyagiFiyat.Text));
                cmd.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd2 = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR + @p1", bgl);
                cmd2.Parameters.AddWithValue("@p1", decimal.Parse(TxtGazyagiFiyat.Text));
                cmd2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd3 = new SqlCommand("Update TBLBENZIN Set STOK = STOK - @p1 Where PETROLTUR = 'Gazyağı'", bgl);
                cmd3.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                cmd3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Satış Yapıldı");
                listele();
                temizle();
            }

            else
            {
                //Otogaz
                bgl.Open();
                SqlCommand cmd = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                cmd.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                cmd.Parameters.AddWithValue("@p2", "Otogaz");
                cmd.Parameters.AddWithValue("@p3", numericUpDown5.Value);
                cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtOtogazFiyat.Text));
                cmd.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd2 = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR + @p1", bgl);
                cmd2.Parameters.AddWithValue("@p1", decimal.Parse(TxtOtogazFiyat.Text));
                cmd2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand cmd3 = new SqlCommand("Update TBLBENZIN Set STOK = STOK - @p1 Where PETROLTUR = 'Otogaz'", bgl);
                cmd3.Parameters.AddWithValue("@p1", numericUpDown5.Value);
                cmd3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Satış Yapıldı");
                listele();
                temizle();
            }
        }

        private void BtnKursunsuzEkle_Click(object sender, EventArgs e)
        {
            if (LblKursunsuzLitre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(TxtLitre.Text) * 35.85);
                bgl.Open();
                SqlCommand komut = new SqlCommand("Update TBLKASA set MIKTAR = MIKTAR - @p1", bgl);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut2 = new SqlCommand("Update TBLBENZIN Set STOK = STOK + @p1 Where PETROLTUR = 'Kurşunsuz95'", bgl);
                komut2.Parameters.AddWithValue("@p1", int.Parse(TxtLitre.Text));
                komut2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut3 = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                komut3.Parameters.AddWithValue("@p1", "Yakıt Alımı");
                komut3.Parameters.AddWithValue("@p2", "Kurşunsuz95");
                komut3.Parameters.AddWithValue("@p3", int.Parse(TxtLitre.Text));
                komut3.Parameters.AddWithValue("@p4", tutar);
                komut3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Yakıt Alımı Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo Zaten Dolu");
            }
        }

        private void BtnDizelEkle_Click(object sender, EventArgs e)
        {
            if (LblDizelLitre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(TxtLitre.Text) * 36.05);
                bgl.Open();
                SqlCommand komut = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR - @p1", bgl);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut2 = new SqlCommand("Update TBLBENZIN Set STOK = STOK + @p1 Where PETROLTUR = 'Dizel'", bgl);
                komut2.Parameters.AddWithValue("@p1", int.Parse(TxtLitre.Text));
                komut2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut3 = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                komut3.Parameters.AddWithValue("@p1", "Yakıt Alımı");
                komut3.Parameters.AddWithValue("@p2", "Dizel");
                komut3.Parameters.AddWithValue("@p3", int.Parse(TxtLitre.Text));
                komut3.Parameters.AddWithValue("@p4", tutar);
                komut3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Yakıt Alımı Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo Zaten Dolu");
            }
        }

        private void BtnProDizelEkle_Click(object sender, EventArgs e)
        {
            if (LblProDizelLitre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(TxtLitre.Text) * 36.10);
                bgl.Open();
                SqlCommand komut = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR - @p1", bgl);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut2 = new SqlCommand("Update TBLBENZIN Set STOK = STOK + @p1 Where PETROLTUR = 'ProDizel'", bgl);
                komut2.Parameters.AddWithValue("@p1", int.Parse(TxtLitre.Text));
                komut2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut3 = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                komut3.Parameters.AddWithValue("@p1", "Yakıt Alımı");
                komut3.Parameters.AddWithValue("@p2", "ProDizel");
                komut3.Parameters.AddWithValue("@p3", int.Parse(TxtLitre.Text));
                komut3.Parameters.AddWithValue("@p4", tutar);
                komut3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Yakıt Alımı Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo Zaten Dolu");
            }
        }

        private void BtnGazyagiEkle_Click(object sender, EventArgs e)
        {
            if (LblGazyagiLitre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(TxtLitre.Text) * 32.31);
                bgl.Open();
                SqlCommand komut = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR - @p1", bgl);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut2 = new SqlCommand("Update TBLBENZIN Set STOK = STOK + @p1 Where PETROLTUR = 'Gazyağı'", bgl);
                komut2.Parameters.AddWithValue("@p1", int.Parse(TxtLitre.Text));
                komut2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut3 = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                komut3.Parameters.AddWithValue("@p1", "Yakıt Alımı");
                komut3.Parameters.AddWithValue("@p2", "Gazyağı");
                komut3.Parameters.AddWithValue("@p3", int.Parse(TxtLitre.Text));
                komut3.Parameters.AddWithValue("@p4", tutar);
                komut3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Yakıt Alımı Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo Zaten Dolu");
            }
        }

        private void BtnOtogazEkle_Click(object sender, EventArgs e)
        {
            if (LblOtogazLitre.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(TxtLitre.Text) * 21.24);
                bgl.Open();
                SqlCommand komut = new SqlCommand("Update TBLKASA Set MIKTAR = MIKTAR - @p1", bgl);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut2 = new SqlCommand("Update TBLBENZIN Set STOK = STOK + @p1 Where PETROLTUR = 'Otogaz'", bgl);
                komut2.Parameters.AddWithValue("@p1", int.Parse(TxtLitre.Text));
                komut2.ExecuteNonQuery();
                bgl.Close();

                bgl.Open();
                SqlCommand komut3 = new SqlCommand("Insert Into TBLHAREKET (PLAKA, BENZINTURU, LITRE, FIYAT) Values (@p1, @p2, @p3, @p4)", bgl);
                komut3.Parameters.AddWithValue("@p1", "Yakıt Alımı");
                komut3.Parameters.AddWithValue("@p2", "Otogaz");
                komut3.Parameters.AddWithValue("@p3", int.Parse(TxtLitre.Text));
                komut3.Parameters.AddWithValue("@p4", tutar);
                komut3.ExecuteNonQuery();
                bgl.Close();
                MessageBox.Show("Yakıt Alımı Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo Zaten Dolu");
            }
        }
    }
}
