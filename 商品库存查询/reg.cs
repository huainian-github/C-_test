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

namespace 商品库存查询
{
    public partial class reg : Form
    {
        public reg()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Users.xml");
            string Account = textBox1.Text.Trim();
            string Password = textBox2.Text.Trim();
            XmlNode root = xmlDoc.SelectSingleNode("users");

            XmlElement xeluser = xmlDoc.CreateElement("user");

            XmlElement xelaccount = xmlDoc.CreateElement("account");
            xelaccount.InnerText = Account;

            XmlElement xelpassword = xmlDoc.CreateElement("password");
            xelpassword.InnerText = Password;
            xeluser.AppendChild(xelaccount);
            xeluser.AppendChild(xelpassword);
            root.AppendChild(xeluser);

            xmlDoc.Save("Users.xml");
            MessageBox.Show("注册成功！");
            this.Close();
        }
    }
}
