using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tekstil_Otamasyon
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {
            InitializeComponent();
        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            
        }
        

        FrmUrunler frmUrunler;
        FrmMusteriler frmMusteriler;
        private void btnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            if (frmUrunler == null)
            {
                frmUrunler = new FrmUrunler();
                frmUrunler.MdiParent = this;
                frmUrunler.Show();
            }
            
        }

        private void btnMusteriler_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frmMusteriler == null)
            {
                frmMusteriler = new FrmMusteriler();
                frmMusteriler.MdiParent = this;
                frmMusteriler.Show();
            }
        }

        FrmFirmalar frmFirmalar;
        private void btnFirmalar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frmFirmalar == null)
            {
                frmFirmalar = new FrmFirmalar();
                frmFirmalar.MdiParent = this;
                frmFirmalar.Show();
            }
        }

        FrmPersonel frmPersonel;
        private void btnPersoneller_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frmPersonel == null)
            {
                frmPersonel = new FrmPersonel();
                frmPersonel.MdiParent = this;
                frmPersonel.Show();
            }
        }
    }
}
