using System;
using System.Reflection.Metadata;

namespace Not_Uygulaması_2
{
    public partial class Form1 : Form
    {

        string[] açıklamalist = new string[1024];
        string directionPath;
        string content;
        string ComputerName = Environment.UserName;
        int totalNotes;

        public Form1()
        {
            InitializeComponent();
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void minimizeet_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void titletext_Click(object sender, EventArgs e)
        {
            if (titletext.Text == "Title")
            {
                titletext.Text = "";
            }
        }

        private void eklebtn_Click(object sender, EventArgs e)
        {
            if (titletext.Text != "" && açıklama.Text != "")
            {
                string eklenecekPath = $"C:\\Users\\{ComputerName}\\NotesApp\\" + titletext.Text + ".txt";
                if (!File.Exists(eklenecekPath))
                {
                    string tarih = DateTime.Now.ToString();
                    FileStream fileStream = File.Create(eklenecekPath);
                    fileStream.Close();
                    StreamWriter sw = new StreamWriter(eklenecekPath);
                    sw.WriteLine(açıklama.Text);
                    sw.WriteLine(" ");
                    sw.WriteLine(tarih);
                    sw.Close();

                    File.AppendAllText(content, titletext.Text + "\n");

                    MessageBox.Show("Note Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    notlar.Items.Add(titletext.Text);

                    açıklamalist = File.ReadAllLines(content);

                    açıklama.Text = "";
                    titletext.Text = "";
                }
                else
                {
                    MessageBox.Show("Note Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void notlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notlar.SelectedItem != null)
            {
                string okunacakPath = $"C:\\Users\\{ComputerName}\\NotesApp\\" + notlar.SelectedItem.ToString() + ".txt";
                StreamReader sr = new StreamReader(okunacakPath);
                string içerik = sr.ReadToEnd();
                sr.Close();
                titletext.Text = notlar.SelectedItem.ToString();
                açıklama.Text = içerik;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            directionPath = $"C:\\Users\\{ComputerName}\\NotesApp";
            content = $"C:\\Users\\{ComputerName}\\NotesApp\\NoteNames321.txt";

            if (!Directory.Exists(directionPath))
            {
                Directory.CreateDirectory(directionPath);
            }

            if (!File.Exists(content))
            {

                FileStream fileStream = File.Create(content);
                fileStream.Close();
            }

            totalNotes = File.ReadLines(content).Count();

            if (totalNotes > 0)
            {
                açıklamalist = File.ReadAllLines(content);

                for (int i = 0; i <= totalNotes - 1; i++)
                {
                    notlar.Items.Add(açıklamalist[i]);
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (notlar.SelectedItem != null)
            {
                int silinecekİndex = notlar.SelectedIndex;
                string silinecekPath = $"C:\\Users\\{ComputerName}\\NotesApp\\" + notlar.SelectedItem.ToString() + ".txt";
                File.Delete(silinecekPath);
                string silbil_al = notlar.SelectedItem.ToString();
                string tempFile = Path.GetTempFileName();
                var linesToKeep = File.ReadLines(content).Where(l => l != silbil_al);
                File.WriteAllLines(tempFile, linesToKeep);
                File.Delete(content);
                File.Move(tempFile, content);
                açıklama.Text = "";
                titletext.Text = "";

                totalNotes = File.ReadLines(content).Count();

                if (totalNotes > 0)
                {
                    açıklamalist = File.ReadAllLines(content);
                }

                notlar.Items.RemoveAt(silinecekİndex);

                MessageBox.Show("Note Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
