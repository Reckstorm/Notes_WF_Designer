using Notes_WF_Designer.Properties;
using System.ComponentModel;
using System.Globalization;

namespace Notes_WF_Designer
{
    public partial class Form1 : Form
    {
        private string _path = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_path.Equals(string.Empty))
            {
                File.WriteAllText(_path, textBox1.Text);
            }
            else
            {
                Save(sender, e);
            }
        }

        private void Save(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "text|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string temp = string.Empty;
                if (saveFileDialog.FileName.Length > 0)
                {
                    temp = $"{saveFileDialog.FileName}_Stats.txt";
                }
                string[] words = textBox1.Text.Trim().Split(' ');
                int count = 0;
                foreach (string word in words)
                {
                    if (word.Any(Char.IsLetter)) count++;
                }
                string content = $"Length:{textBox1.Text.Length}, Digits:{textBox1.Text.Count(x => Char.IsDigit(x))}, Punctuation:{textBox1.Text.Count(x => Char.IsPunctuation(x))}, Words:{count}";
                File.WriteAllText(saveFileDialog.FileName, textBox1.Text);
                File.WriteAllText(temp, content);
                _path = saveFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size + 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size - 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0) textBox1.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Form1));
            if ((_path.Equals(string.Empty) && !textBox1.Text.Equals(string.Empty)) || (!_path.Equals(string.Empty) && !textBox1.Text.Equals(File.ReadAllText(_path))))
            {
                DialogResult res = MessageBox.Show(manager.GetString("CloseText", CultureInfo.CurrentCulture), manager.GetString("CloseCaption", CultureInfo.CurrentCulture), MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes) button1_Click(sender, e);
                else if (res == DialogResult.Cancel) e.Cancel = true;
            }
        }

        private void ChangeLang(string lang)
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Form1));
            //CultureInfo culture = new CultureInfo(lang);
            CultureInfo.CurrentCulture = new CultureInfo(lang);
            foreach (Control item in panel1.Controls)
            {
                manager.ApplyResources(item, item.Name, CultureInfo.CurrentCulture);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeLang("en");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeLang("ru");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeLang("de");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeLang("es");
        }
    }
}