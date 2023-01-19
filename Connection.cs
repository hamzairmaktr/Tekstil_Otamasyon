using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekstil_Otamasyon
{
    internal class Connection
    {
        string bglCumle = ConfigurationManager.ConnectionStrings["baglantiCumle"].ConnectionString;
        public SqlConnection baglanti()
        {
            SqlConnection baglanti = new SqlConnection(bglCumle);
            baglanti.Open();
            return baglanti;
        }
    }
}
