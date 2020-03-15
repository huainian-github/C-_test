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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("products.xml");
            if (radioButton1.Checked == true)
            {
                string name = textBox2.Text.Trim();
                string id = textBox3.Text.Trim();
                string price = textBox4.Text.Trim();
                string quantity = textBox5.Text.Trim();

                XmlNode root = doc.SelectSingleNode("商品列表");

                XmlElement xname = doc.CreateElement("商品名");
                xname.InnerText = name;
                XmlElement xprice = doc.CreateElement("价格");
                xprice.InnerText = price;
                XmlElement xquantity = doc.CreateElement("数量");
                xquantity.InnerText = quantity;

                XmlElement xproduct = doc.CreateElement("商品");
                xproduct.AppendChild(xname);
                xproduct.AppendChild(xprice);
                xproduct.AppendChild(xquantity);
                xproduct.SetAttribute("商品编号", id);

                root.AppendChild(xproduct);
                doc.Save("products.xml");
                MessageBox.Show("添加成功");
                set_gridview(productlist);
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                
            }
            else
            {
                XmlElement xe = doc.DocumentElement;
                string strPath = string.Format("/商品列表/商品[@商品编号=\"{0}\"]", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                XmlElement selectXe = (XmlElement)xe.SelectSingleNode(strPath);
                selectXe.SetAttribute("商品编号", textBox3.Text.Trim());

                XmlNodeList cnodes = selectXe.ChildNodes;
                cnodes.Item(0).InnerText = textBox2.Text.Trim();
                cnodes.Item(1).InnerText = textBox4.Text.Trim();
                cnodes.Item(2).InnerText = textBox5.Text.Trim();
                doc.Save("products.xml");
                set_gridview(productlist);
            }
        }

        private void set_gridview(List<product> productlist)
        {
            dataGridView1.DataSource = null;
            productlist.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load("products.xml");
            XmlNode root = doc.SelectSingleNode("商品列表");
            XmlNodeList nodes = root.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                product prod = new product();
                XmlElement ele = (XmlElement)node;
                prod.id = ele.GetAttribute("商品编号").ToString();

                XmlNodeList cnodes = node.ChildNodes;
                prod.name = cnodes.Item(0).InnerText;
                prod.price = cnodes.Item(1).InnerText;
                prod.quantity = cnodes.Item(2).InnerText;

                productlist.Add(prod);
            }
            dataGridView1.DataSource = productlist;
            dataGridView1.Columns["name"].HeaderText = "商品名";
            dataGridView1.Columns["id"].HeaderText = "商品编号";
            dataGridView1.Columns["price"].HeaderText = "价格";
            dataGridView1.Columns["quantity"].HeaderText = "数量";
        }
        List<product> productlist = new List<product>();
        private void button3_Click(object sender, EventArgs e)
        {
            set_gridview(productlist);
        }

        private void button4_Click(object sender, EventArgs e)
        {   XmlDocument doc = new XmlDocument();
            doc.Load("products.xml");
           // XmlElement xe = doc.DocumentElement;
            string strPath = string.Format("/商品列表/商品[@商品编号=\"{0}\"]", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //XmlElement selectXe = (XmlElement)xe.SelectSingleNode(strPath);
            XmlElement selectXe = (XmlElement)doc.SelectSingleNode(strPath);
            selectXe.ParentNode.RemoveChild(selectXe);
            doc.Save("products.xml");
            set_gridview(productlist);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            productlist.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load("products.xml");
            XmlElement xe = doc.DocumentElement;
            string temp = textBox1.Text.Trim();
            string strPath;
            if (as_id.Checked == true)
                strPath = string.Format("/商品列表/商品[@商品编号=\"{0}\"]", temp);
            else
                strPath = string.Format("/商品列表/商品[商品名=\"{0}\"]", temp);
            XmlNodeList selectXe = xe.SelectNodes(strPath);

            foreach (XmlNode node in selectXe)
            {
                product prod = new product();
                XmlElement ele = (XmlElement)node;
                prod.id = ele.GetAttribute("商品编号").ToString();

                XmlNodeList cnodes = node.ChildNodes;
                prod.name = cnodes.Item(0).InnerText;
                prod.price = cnodes.Item(1).InnerText;
                prod.quantity = cnodes.Item(2).InnerText;

                productlist.Add(prod);
            }
            dataGridView1.DataSource = productlist;
            dataGridView1.DataSource = productlist;
            dataGridView1.Columns["name"].HeaderText = "商品名";
            dataGridView1.Columns["id"].HeaderText = "商品编号";
            dataGridView1.Columns["price"].HeaderText = "价格";
            dataGridView1.Columns["quantity"].HeaderText = "数量";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                button2.Text = "添加";
            }
            else
            {
                button2.Text = "修改";
                if (dataGridView1.CurrentRow != null)
                {
                    textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
                if (dataGridView1.CurrentRow != null)
                {
                    textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //增
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("products.xml");
            XmlElement xdoc1 = xdoc.DocumentElement;
            //XmlNode root1 = xdoc1.SelectSingleNode("商品列表");
            XmlElement x1 = xdoc.CreateElement("商品名");
            x1.InnerText = "西瓜";
            XmlElement x2 = xdoc.CreateElement("价格");
            x2.InnerText = "6";
            XmlElement x3 = xdoc.CreateElement("数量");
            x3.InnerText = "4";

            XmlElement x4 = xdoc.CreateElement("商品");
            x4.SetAttribute("商品编号", "1009");
            x4.AppendChild(x1);
            x4.AppendChild(x2);
            x4.AppendChild(x3);
            xdoc1.AppendChild(x4);
            xdoc.Save("products.xml");
        }
    }
}
