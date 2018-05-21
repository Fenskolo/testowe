using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Class1
    {
        private SqlConnection con;
        private Form2 a;
        public Class1(Form2 f)
        {
            a = f;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "ANTONINA";
            builder.InitialCatalog = "testowy";
            builder.IntegratedSecurity = true;
            con = new SqlConnection(builder.ConnectionString);
            DataSet ds = new DataSet();
            con.InfoMessage += new SqlInfoMessageEventHandler(Connection_InfoMessage);
            Task task = Task.Factory.StartNew(() =>
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "dbo.test";
                    cmd.CommandTimeout = 120;


                    cmd.Connection.Open();

                    using (SqlDataAdapter sa = new SqlDataAdapter(cmd))
                    {
                        Task.Delay(300).Wait();
                        sa.Fill(ds);
                        cmd.Connection.Close();
                        a.Invoke((MethodInvoker)delegate ()
                        {
                            a.Close();
                        });
                    }
                }
            }
            );

        }

        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            a.label1.Invoke((MethodInvoker)delegate ()
            {
                a.label1.Text = e.Message;
            });
        }

        //s. = "server=(local);user id=ab;" +
        //     "password= a!Pass113;initial catalog=AdventureWorks";
    }
}
