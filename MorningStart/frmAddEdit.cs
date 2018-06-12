using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MorningStart
{
    public partial class frmAddEdit : Form
    {

        string fstrFilePath = Globals.mstrFilePath;
        Boolean fblnFileExists;
        char DELIM = Globals.DELIMITER;

        public frmAddEdit()
        {
            InitializeComponent();
        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            string lstrExePath;
            string lstrExe;
            string[] lstrExePathArray;

            fblnFileExists = Globals.CheckForTextFile(fstrFilePath);
            if (!fblnFileExists)
            {
                CreateNewTextFile();
            }

            using (FileStream fs = File.Open(fstrFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                Byte[] lbytInfo = new byte[1024];
                UTF8Encoding lutfTemp = new UTF8Encoding(true);

                while (fs.Read(lbytInfo, 0, lbytInfo.Length) > 0)
                {
                    lstrExePath = lutfTemp.GetString(lbytInfo);
                    lstrExePathArray = lstrExePath.Split(',');
                    foreach (string str in lstrExePathArray)
                    {
                        lstrExe = str.Replace("\r\n", "");
                        if (lstrExe.Trim() == "")
                            continue;
                        lstExes.Items.Add(lstrExe.Trim());
                    }
                }
                fs.Close();
            }
            lstExes.SelectedIndex = lstExes.Items.Count - 1;
        }

        private void CreateNewTextFile()
        {
            using (StreamWriter sw = File.CreateText(fstrFilePath))
            {
                //default applications get written here
                //but others can be added thru the interface

                //sw.WriteLine("C:/Program Files (x86)/Google/Chrome/Application/chrome.exe" + DELIM);
                //sw.WriteLine("C:/Program Files (x86)/Notepad++/notepad++.exe"+ DELIM);

                sw.WriteLine("" + DELIM);
            }
        }

        private void tsAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string lstrExePath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                lstrExePath = openFileDialog1.FileName;
                lstrExePath = lstrExePath.Replace("\\","/");
                lstrExePath = lstrExePath.Replace('\\', '/');
                lstExes.Items.Add(lstrExePath);
                sr.Close();
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            int SelectedIndex = lstExes.SelectedIndex;

            try
            {
                lstExes.Items.RemoveAt(SelectedIndex);
            }
            catch(IndexOutOfRangeException err) 
            {
                
            }
            if (SelectedIndex < lstExes.Items.Count)
            {
                lstExes.SelectedIndex = SelectedIndex - 1;
            }
            else
            {
                lstExes.SelectedIndex = lstExes.Items.Count-1;
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            File.Delete(fstrFilePath);
            string lstrExePath;

            using (StreamWriter sw = File.CreateText(fstrFilePath))
            {
                for (int i = 0; i < lstExes.Items.Count; i++)
                {
                    lstrExePath = lstExes.Items[i].ToString().Replace('\0', ' ');
                    if (i == lstExes.Items.Count-1)
                    {
                        if (lstrExePath.Trim() != "") sw.WriteLine(lstrExePath.Trim()); //last item, don't add a delimiter on it
                    }
                    else
                    {
                        if (lstrExePath.Trim() != "") sw.WriteLine(lstrExePath.Trim() + DELIM.ToString());
                    }
                }
                sw.Close();
            }
            Globals.mintProgramCount = lstExes.Items.Count;
            cmdCancel_Click(sender, e);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
