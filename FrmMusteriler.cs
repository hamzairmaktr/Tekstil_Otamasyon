using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tekstil_Otamasyon
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        Connection bgl=new Connection();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Musteriler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource=dt;
        }

        void sehirListele()
        {
            SqlCommand cmd = new SqlCommand("select sehir from Tbl_Iller",bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
        }
        
        void ilceListele()
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand("select ilce from Tbl_Ilceler where sehir=@p1",bgl.baglanti());
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
            txtSoyad.Text = "";
            cmbFirma.Text = "";
            mskTel1.Text = "";
            mskTel2.Text = "";
            txtMail.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtVergiDaire.Text = "";
            rchAdres.Clear();
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirListele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" && txtSoyad.Text=="")
            {
                MessageBox.Show("Lütfen değerleri doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into Tbl_Musteriler (ad,soyad,firmaId,telefon,telefon2,mail,il,ilce,vergiDaire,adres) " +
                   "values(@ad,@soyad,@firmaId,@telefon,@telefon2,@mail,@il,@ilce,@vergiDaire,@adres)", bgl.baglanti());
                    cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                    cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                    cmd.Parameters.AddWithValue("@firmaId", cmbFirma.Text);
                    cmd.Parameters.AddWithValue("@telefon", mskTel1.Text);
                    cmd.Parameters.AddWithValue("@telefon2", mskTel2.Text);
                    cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                    cmd.Parameters.AddWithValue("@il", cmbIl.Text);
                    cmd.Parameters.AddWithValue("@ilce", cmbIlce.Text);
                    cmd.Parameters.AddWithValue("@vergiDaire", txtVergiDaire.Text);
                    cmd.Parameters.AddWithValue("@adres", rchAdres.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Müşteri başarı ile eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Müşteri eklenirken hata oluştu.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                finally
                {
                    bgl.baglanti().Close();
                    temizle();
                    listele();
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr=gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["id"].ToString();
                txtAd.Text = dr["ad"].ToString();
                txtSoyad.Text = dr["soyad"].ToString();
                cmbFirma.Text = dr["firmaId"].ToString();
                mskTel1.Text = dr["telefon"].ToString();
                mskTel2.Text = dr["telefon2"].ToString();
                txtMail.Text = dr["mail"].ToString();
                cmbIl.Text = dr["il"].ToString();
                cmbIlce.Text = dr["ilce"].ToString();
                txtVergiDaire.Text = dr["vergiDaire"].ToString();
                rchAdres.Text = dr["adres"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete Tbl_Musteriler where id=@id", bgl.baglanti());
                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Müşteri başarı ile silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Müşteri silinirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bgl.baglanti().Close();
                    temizle();
                    listele();
                }
            }          
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Tbl_Musteriler set ad=@ad,soyad=@soyad,firmaId=@firmaId,telefon=@telefon,telefon2=@telefon2,mail=@mail,il=@il,ilce=@ilce,vergiDaire=@vergiDaire,adres=@adres where id=@id", bgl.baglanti());
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@firmaId", cmbFirma.Text);
                cmd.Parameters.AddWithValue("@telefon", mskTel1.Text);
                cmd.Parameters.AddWithValue("@telefon2", mskTel2.Text);
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@il", cmbIl.Text);
                cmd.Parameters.AddWithValue("@ilce", cmbIlce.Text);
                cmd.Parameters.AddWithValue("@vergiDaire", txtVergiDaire.Text);
                cmd.Parameters.AddWithValue("@adres", rchAdres.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri başarı ile güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Müşteri güncellenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bgl.baglanti().Close();
                temizle();
                listele();
            }
        }
    }
}
