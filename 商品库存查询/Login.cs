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
    public partial class Login : Form
    {
        
        public Login()
        {
            
            InitializeComponent();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            Font f = new Font("宋体", 9, FontStyle.Underline);
            label1.Font = f;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Font f = new Font("宋体", 9, FontStyle.Regular);
            label1.Font = f;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            reg r = new reg();
            r.Show();
        }

        private void login_bottom_Click(object sender, EventArgs e)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("Users.xml");
            XmlNode root = doc.SelectSingleNode("users");
            XmlNodeList items = root.ChildNodes;
            int islogin = 0;
            foreach(XmlNode item in items)
            {
                XmlNodeList temp = item.ChildNodes;
                if (textBox1.Text == temp.Item(0).InnerText && textBox2.Text == temp.Item(1).InnerText)
                {
                    islogin = 1;
                    break;
                }
            }
            if (islogin == 1)
            {
                MessageBox.Show("登陆成功！");
                this.Hide();
                mainForm form = new mainForm();
                form.Show();
            }
            else
                MessageBox.Show("账号或密码错误！");
                
        }
    }
}
