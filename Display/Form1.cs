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
using System.IO;
namespace Display
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if(folderBrowserDialog1.ShowDialog()== DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            StreamWriter MarvelDetails = new StreamWriter(textBox2.Text +"\\Data.txt");
            
            string xmlfile = textBox1.Text;
            Console.WriteLine(xmlfile);
            //Declaring xml document
            XmlDocument marvel = new XmlDocument();
            //loading original document
            marvel.Load(xmlfile);
            //locating the data in the following tags in the xml document.
            XmlNodeList id = marvel.SelectNodes("./film/items/MARVEL[@content='id']");
            XmlNodeList title = marvel.SelectNodes("./film/items/MARVEL[@content='title']");
            XmlNodeList releasedate = marvel.SelectNodes("./film/items/MARVEL[@content='release_date']");
            //declaring the int value for each set of data
            int value = 0;
            //foreach loop to create extract the data
            foreach (var film in title)
            {
                //Displaying each set value with an equals sign at the end of each one.
                MarvelDetails.WriteLine(id.Item(value).InnerText + "=" +
                                        title.Item(value).InnerText + "=" +
                                        releasedate.Item(value).InnerText);
                //increasing the value by one eachtime to extract the new piece data
                ++value;
                //Save the data into the textfile 
                MarvelDetails.Flush();
            }
            //close the text file
            MarvelDetails.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();

            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //creating the new xml template
            var xml = new StringBuilder();
            xml.Append("<film> \n");
            //importing the data from the text file that was made previously 
            foreach (var line in File.ReadAllLines(textBox2.Text + "\\Data.txt"))
            {
                //setting the vals value to the split the data everytime it sees an = symbol therefore the allow the data to split fairly.
                var vals = line.Split('=');

                xml.AppendFormat("  <movie>\n" +
                                 "      <id>{0}</id>\n" +
                                 "      <title>{1}</title>\n" +
                                 "      <releasedate>{2}</releasedate>\n" +
                                 "  </movie>\n",
                                 //declaring all values for the data to be displayed. 
                                 vals[0].Trim(), vals[1].Trim(), vals[2].Trim());
            }
            xml.Append("</film>");
            //Saves the new xml database into a new xml file. 
            File.WriteAllText(textBox3.Text+"\\Marvel2.0.xml", xml.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //creating the new xml template
            var word = new StringBuilder();
            //importing the data from the text file that was made previously 
            foreach (var line in File.ReadAllLines(textBox2.Text + "\\Data.txt"))
            {
                //setting the vals value to the split the data everytime it sees an = symbol therefore the allow the data to split fairly.
                var vals = line.Split('=');

                word.AppendFormat("Id:   {0} \n" +
                                 "Title: {1}\n" +
                                 "Date:  {2}\n" +
                                 "______________________ \n",
                                 //declaring all values for the data to be displayed. 
                                 vals[0].Trim(), vals[1].Trim(), vals[2].Trim());

            
            }
            //Saves the new xml database into a new word file. 
            File.WriteAllText(textBox3.Text+"\\Marvel2.0.doc", word.ToString());
        }
    }
}
