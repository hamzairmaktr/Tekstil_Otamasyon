using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tekstil_Otamasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        Connection bgl = new Connection();

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Urunler", bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Ürün listesi yüklenirken hata oluştu","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtTur.Text = "";
            txtRenk.Text = "";
            txtKg.Text = "";
            rchDetay.Text = "";
            txtAlisFiyat.Text = "";
            txtSatisFiyat.Text = "";
            nmrcAdet.Value = 0;
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmdKaydet = new SqlCommand("insert into Tbl_Urunler (tur,ad,renk,kg,adet,detay,alisFiyat,satisFiyat) values(@tur,@ad,@renk,@kg,@adet,@detay,@alisFiyat,@satisFiyat)", bgl.baglanti());
                cmdKaydet.Parameters.AddWithValue("@tur", txtTur.Text);
                cmdKaydet.Parameters.AddWithValue("@ad", txtAd.Text);
                cmdKaydet.Parameters.AddWithValue("@renk", txtRenk.Text);
                cmdKaydet.Parameters.AddWithValue("@kg", Convert.ToDecimal(txtKg.Text));
                cmdKaydet.Parameters.AddWithValue("@adet", Convert.ToInt32(nmrcAdet.Value));
                cmdKaydet.Parameters.AddWithValue("@detay", rchDetay.Text);
                cmdKaydet.Parameters.AddWithValue("@alisFiyat", Convert.ToDecimal(txtAlisFiyat.Text));
                cmdKaydet.Parameters.AddWithValue("@satisFiyat", Convert.ToDecimal(txtSatisFiyat.Text));
                cmdKaydet.ExecuteNonQuery();
                MessageBox.Show("Ürün başarı ile eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ürün eklenirken hata oluştu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bgl.baglanti().Close();
                listele();
                temizle();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmdSil = new SqlCommand("delete Tbl_Urunler where id=@p1", bgl.baglanti());
                    cmdSil.Parameters.AddWithValue("@p1", txtId.Text);
                    cmdSil.ExecuteNonQuery();
                    MessageBox.Show("Ürün başarı ile silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ürün silinirken hata oluştu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bgl.baglanti().Close();
                    listele();
                    temizle();
                }
            }           
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["id"].ToString();
            txtTur.Text = dr["tur"].ToString();
            txtAd.Text = dr["ad"].ToString();
            txtRenk.Text = dr["renk"].ToString();
            txtKg.Text = dr["kg"].ToString();
            nmrcAdet.Value = decimal.Parse(dr["adet"].ToString());
            rchDetay.Text = dr["detay"].ToString();
            txtAlisFiyat.Text = dr["alisFiyat"].ToString();
            txtSatisFiyat.Text = dr["satisFiyat"].ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmdGuncelle = new SqlCommand("update  Tbl_Urunler set tur=@tur,ad=@ad,renk=@renk,kg=@kg,adet=@adet,detay=@detay,alisFiyat=@alisFiyat,satisFiyat=@satisFiyat where id=@id", bgl.baglanti());
                cmdGuncelle.Parameters.AddWithValue("@tur", txtTur.Text);
                cmdGuncelle.Parameters.AddWithValue("@ad", txtAd.Text);
                cmdGuncelle.Parameters.AddWithValue("@renk", txtRenk.Text);
                cmdGuncelle.Parameters.AddWithValue("@kg", Convert.ToDecimal(txtKg.Text));
                cmdGuncelle.Parameters.AddWithValue("@adet", Convert.ToInt32(nmrcAdet.Value));
                cmdGuncelle.Parameters.AddWithValue("@detay", rchDetay.Text);
                cmdGuncelle.Parameters.AddWithValue("@alisFiyat", Convert.ToDecimal(txtAlisFiyat.Text));
                cmdGuncelle.Parameters.AddWithValue("@satisFiyat", Convert.ToDecimal(txtSatisFiyat.Text));
                cmdGuncelle.Parameters.AddWithValue("@id", txtId.Text);
                cmdGuncelle.ExecuteNonQuery();
                MessageBox.Show("Ürün başarı ile güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ürün güncellenirken hata oluştu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
