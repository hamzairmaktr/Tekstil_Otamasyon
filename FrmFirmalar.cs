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
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Firmalar",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
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
                txtYetkiliStatu.Text = dr["yetkiliStatu"].ToString();

            }
        }
    }
}
