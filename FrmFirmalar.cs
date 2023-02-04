using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tekstil_Otamasyon
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        Connection bgl = new Connection();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Firmalar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirListele()
        {
            SqlCommand cmd = new SqlCommand("select sehir from Tbl_Iller", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
        }

        void ilceListele()
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand("select ilce from Tbl_Ilceler where sehir=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
        }

        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtFirmaTur.Text = "";
            txtYetkiliAd.Text = "";
            txtYetkiliStatu.Text = "";
            mskTel1.Text = "";
            mskTel2.Text = "";
            txtMail.Text = "";
            txtMail.Text = "";
            mskFax.Text = "";
            mskFax.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtVergiDaire.Text = "";
            rchAdres.Text = "";
            txtOzel1.Text = "";
            txtOzel2.Text = "";
            txtOzel3.Text = "";
        }

        void cariKodAciklamalar()
        {
            SqlCommand cmd = new SqlCommand("select firmaKod1 from Tbl_Kodlar",bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rchKod1.Text = dr[0].ToString();
            }
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirListele();
            cariKodAciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["id"].ToString();
                txtAd.Text = dr["ismi"].ToString();
                txtFirmaTur.Text = dr["firmaTur"].ToString();
                txtYetkiliAd.Text = dr["yetkiliAdSoyad"].ToString();
                txtYetkiliStatu.Text = dr["yetkiliStatu"].ToString();
                mskTel1.Text = dr["telefon1"].ToString();
                mskTel2.Text = dr["telefon2"].ToString();
                txtMail.Text = dr["mail"].ToString();
                txtMail.Text = dr["mail"].ToString();
                mskFax.Text = dr["fax"].ToString();
                mskFax.Text = dr["fax"].ToString();
                cmbIl.Text = dr["il"].ToString();
                cmbIlce.Text = dr["ilce"].ToString();
                txtVergiDaire.Text = dr["vergiDaire"].ToString();
                rchAdres.Text = dr["adres"].ToString();
                txtOzel1.Text = dr["ozelKod1"].ToString();
                txtOzel2.Text = dr["ozelKod2"].ToString();
                txtOzel3.Text = dr["ozelKod3"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Tbl_Firmalar (ismi,firmaTur,yetkiliAdSoyad,yetkiliStatu,telefon1,telefon2,mail,fax,il,ilce,vergiDaire,adres,ozelKod1,ozelKod2,ozelKod3) values (@ismi,@firmaTur,@yetkiliAdSoyad,@yetkiliStatu,@telefon1,@telefon2,@mail,@fax,@il,@ilce,@vergiDaire,@adres,@ozelKod1,@ozelKod2,@ozelKod3)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@ismi", txtAd.Text);
                cmd.Parameters.AddWithValue("@firmaTur", txtFirmaTur.Text);
                cmd.Parameters.AddWithValue("@yetkiliAdSoyad", txtYetkiliAd.Text);
                cmd.Parameters.AddWithValue("@yetkiliStatu", txtYetkiliStatu.Text);
                cmd.Parameters.AddWithValue("@telefon1", mskTel1.Text);
                cmd.Parameters.AddWithValue("@telefon2", mskTel2.Text);
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@fax", mskFax.Text);
                cmd.Parameters.AddWithValue("@il", cmbIl.Text);
                cmd.Parameters.AddWithValue("@ilce", cmbIlce.Text);
                cmd.Parameters.AddWithValue("@vergiDaire", txtVergiDaire.Text);
                cmd.Parameters.AddWithValue("@adres", rchAdres.Text);
                cmd.Parameters.AddWithValue("@ozelKod1", txtOzel1.Text);
                cmd.Parameters.AddWithValue("@ozelKod2", txtOzel2.Text);
                cmd.Parameters.AddWithValue("@ozelKod3", txtOzel3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Firma başarı ile eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Firma eklenirken hata oluştu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bgl.baglanti().Close();
                listele();
                temizle();
            }
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete Tbl_Firmalar where id=@id", bgl.baglanti());
                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Firma başarı ile silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Firma silinirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bgl.baglanti().Close();
                    temizle();
                    listele();
                }
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Tbl_Firmalar set ismi=@ismi,firmaTur=@firmaTur,yetkiliAdSoyad=@yetkiliAdSoyad,yetkiliStatu=@yetkiliAdSoyad,telefon1=@telefon1,telefon2=@telefon2,mail=@mail,fax=@fax,il=@il,ilce=@ilce,vergiDaire=@vergiDaire,adres=@adres,ozelKod1=@ozelKod1,ozelKod2=@ozelKod2,ozelKod3=ozelKod3 where id=@id", bgl.baglanti());
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@ismi", txtAd.Text);
                cmd.Parameters.AddWithValue("@firmaTur", txtFirmaTur.Text);
                cmd.Parameters.AddWithValue("@yetkiliAdSoyad", txtYetkiliAd.Text);
                cmd.Parameters.AddWithValue("@yetkiliStatu", txtYetkiliStatu.Text);
                cmd.Parameters.AddWithValue("@telefon1", mskTel1.Text);
                cmd.Parameters.AddWithValue("@telefon2", mskTel2.Text);
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@fax", mskFax.Text);
                cmd.Parameters.AddWithValue("@il", cmbIl.Text);
                cmd.Parameters.AddWithValue("@ilce", cmbIlce.Text);
                cmd.Parameters.AddWithValue("@vergiDaire", txtVergiDaire.Text);
                cmd.Parameters.AddWithValue("@adres", rchAdres.Text);
                cmd.Parameters.AddWithValue("@ozelKod1", txtOzel1.Text);
                cmd.Parameters.AddWithValue("@ozelKod2", txtOzel2.Text);
                cmd.Parameters.AddWithValue("@ozelKod3", txtOzel3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Firma başarı ile güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Firma güncellenirken hata oluştu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bgl.baglanti().Close();
                listele();
                temizle();
            }
        }
    }
}
