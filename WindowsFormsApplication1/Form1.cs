using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class formProgramWindow : Form
    {

        //Declarin' me vars
        List<string> listFilePaths = new List<string>();
        List<string> listDestinationFilePaths = new List<string>();
        bool bCopyFiles;

        //List<CheckBox> listDoResizeCheckBox = new List<CheckBox>();
        //listDoResizeCheckBox.Add(checkboxDoResize01);

        //end

        public formProgramWindow()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox21_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboboxSourceYRes01_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSourceDir_Click(object sender, EventArgs e)
        {
            if (dialogSourceDir.ShowDialog() == DialogResult.OK)
            {
                textboxSourceDir.Text = dialogSourceDir.SelectedPath;
            }
        }

        private void textboxSourceDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void dialogSourceDir_HelpRequest(object sender, EventArgs e)
        {

        }

        private void checkboxDoResizeAll_CheckedChanged(object sender, EventArgs e)
        {

            //Shitty code as I couldn't figure out why I couldn't add the gui forms to a list variable.

            if (checkboxDoResizeAll.Checked)
            {
                checkboxDoResize01.Checked = true;
                checkboxDoResize02.Checked = true;
                checkboxDoResize03.Checked = true;
                checkboxDoResize04.Checked = true;
                checkboxDoResize05.Checked = true;
                checkboxDoResize06.Checked = true;
                checkboxDoResize07.Checked = true;
                checkboxDoResize08.Checked = true;
                checkboxDoResize09.Checked = true;
                checkboxDoResize10.Checked = true;
                checkboxDoResize11.Checked = true;
                checkboxDoResize12.Checked = true;
                checkboxDoResize13.Checked = true;
                checkboxDoResize14.Checked = true;
                checkboxDoResize15.Checked = true;
            }

            else
            {
                checkboxDoResize01.Checked = false;
                checkboxDoResize02.Checked = false;
                checkboxDoResize03.Checked = false;
                checkboxDoResize04.Checked = false;
                checkboxDoResize05.Checked = false;
                checkboxDoResize06.Checked = false;
                checkboxDoResize07.Checked = false;
                checkboxDoResize08.Checked = false;
                checkboxDoResize09.Checked = false;
                checkboxDoResize10.Checked = false;
                checkboxDoResize11.Checked = false;
                checkboxDoResize12.Checked = false;
                checkboxDoResize13.Checked = false;
                checkboxDoResize14.Checked = false;
                checkboxDoResize15.Checked = false;
            }

        }

        private void checkboxDoUseDestinationDir_CheckedChanged(object sender, EventArgs e)
        {
            //Redundant function, as I can't be fagged making it so we can overwrite the files in the same directories
            //that we found them in.

            if (checkboxDoUseDestinationDir.Checked)
            {
                buttonDestinationDir.Visible = true;
                textboxDestinationDir.Visible = true;
            }

            else
            {
                buttonDestinationDir.Visible = false;
                textboxDestinationDir.Visible = false;
            }
        }

        public void DirSearch(string sDir)
        {
            //Okay, so, we get all subdirectories in a folder via the Directory.GetDirectories() function.
            //We then get the .dds files present in each subfolder by calling Directory.GetFiles() on each
            //subdirectory (which we got from Directory.GetDirectories()
            //
            //The downside to this approach, is that it won't catch any .dds files in the original folder
            //as we're checking for .dds files in the SUBDIRECTORIES!
            //Probably isn't hard to workaround, but, well, don't fix what ain't broken. It works, so I'm good.

            try
            {
                foreach (string strDir in Directory.GetDirectories(sDir))
                {
                    foreach (string strFile in Directory.GetFiles(strDir, "*.dds"))
                    {
                        listFilePaths.Add(strFile);
                        //MessageBox.Show(this, strFile, "File Path is", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    DirSearch(strDir);
                }
            }
            catch (System.Exception excpt) //In case there's an invalid folder kicking about.
            {
                MessageBox.Show(this, excpt.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                Console.WriteLine(excpt.Message);
            }
        }

        public void GetDDSDimensions(string strDDSFilename, out int iSourceDDSx, out int iSourceDDSy)
        {
            //Pretty simple stuff, aside from the whole "using" thing. Basically, when the function ends @ the } bracket
            //C# takes care of making sure the file is not retained in memory, and that we don't have a zombie file-reading thing.
            //So eh. It works, and saves effort. 10/10 points

            using (FileStream fsSourceDDS = new FileStream(strDDSFilename, FileMode.Open, FileAccess.Read))
            using (BinaryReader binaryreaderDDS = new BinaryReader(fsSourceDDS))
            {
                fsSourceDDS.Seek(0x0c, SeekOrigin.Begin);
                ushort ushortHeight = binaryreaderDDS.ReadUInt16();
                fsSourceDDS.Seek(0x10, SeekOrigin.Begin);
                ushort ushortWidth = binaryreaderDDS.ReadUInt16();

                iSourceDDSx = ushortWidth;
                iSourceDDSy = ushortHeight;

                //string strMessageWidth;
                //string strMessageHeight;

                //MessageBox.Show(this, strMessageWidth = iSourceDDSx.ToString(), "Width is");
                //MessageBox.Show(this, strMessageHeight = iSourceDDSx.ToString(), "Height is");
            }
        }

        public bool IsComboBoxTextInteger(ComboBox comboboxToCheckFirst, ComboBox comboboxToCheckSecond, ComboBox comboboxToCheckThird, ComboBox comboboxToCheckFourth)
        {
            Boolean bComboBoxFirst = comboboxToCheckFirst.Text.All(char.IsDigit);
            Boolean bComboBoxSecond = comboboxToCheckSecond.Text.All(char.IsDigit);
            Boolean bComboBoxThird = comboboxToCheckThird.Text.All(char.IsDigit);
            Boolean bComboBoxFourth = comboboxToCheckFourth.Text.All(char.IsDigit);

            //Check to make sure we actually have text in the drop-down list, and that it's a number.

            if (comboboxToCheckFirst.Text.Length > 0
                && bComboBoxFirst
                && comboboxToCheckSecond.Text.Length > 0
                && bComboBoxSecond
                && comboboxToCheckThird.Text.Length > 0
                && bComboBoxThird
                && comboboxToCheckFourth.Text.Length > 0
                && bComboBoxFourth)
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        public string GetNewMipmapLevels(string strWidth, string strHeight)
        {
            int iWidth = Int32.Parse(strWidth);
            int iHeight = Int32.Parse(strHeight);
            int iResToCheck = 1; // Easier to just set it to 1 to begin with, rather than have the failsafe in an else statement.

            if (iWidth > iHeight)
            {
                iResToCheck = iHeight;
            }

            else if (iWidth == iHeight)
            {
                iResToCheck = iWidth;
            }

            else if (iWidth < iHeight)
            {
                iResToCheck = iHeight;
            }

            // Now that we have set iResToCheck (failsale of a value of 1), we'll check it for the right number of mipmaps.

            if (iResToCheck == 1) // Remember that failsafe? Yah, good times.
            {
                return "0";
            }

            else if (iResToCheck == 2)
            {
                return "1";
            }

            else if (iResToCheck == 4)
            {
                return "2";
            }

            else if (iResToCheck == 8)
            {
                return "3";
            }

            else if (iResToCheck == 16)
            {
                return "4";
            }

            else if (iResToCheck == 32)
            {
                return "5";
            }

            else if (iResToCheck == 64)
            {
                return "6";
            }

            else if (iResToCheck == 128)
            {
                return "7";
            }

            else if (iResToCheck == 256)
            {
                return "8";
            }

            else if (iResToCheck == 512)
            {
                return "9";
            }

            else if (iResToCheck == 1024)
            {
                return "10";
            }

            else if (iResToCheck == 2048)
            {
                return "11";
            }

            else if (iResToCheck == 4096)
            {
                return "12";
            }

            else if (iResToCheck == 8192)
            {
                return "13";
            }

            else if (iResToCheck == 16384)
            {
                return "14";
            }

            else
            {
                return "0"; // Just in case.
            }
        }

        public bool IsAlphaPresent(string strDDSFileName)
        {

            // Function that always returned false, isn't used and is useless as a result.
            //Keep it anyway because it's my baby and I don't want to backspace-away my effort.

            using (FileStream fsSourceDDSForAlpha = new FileStream(strDDSFileName, FileMode.Open, FileAccess.Read))
            using (BinaryReader binaryreaderDDSForAlpha = new BinaryReader(fsSourceDDSForAlpha))
            {
                fsSourceDDSForAlpha.Position = 0x50;
                uint uintDWFlags = binaryreaderDDSForAlpha.ReadUInt32();
                bool boolIsValueSetInBitfield = (uintDWFlags & 1) == 1;

                if (boolIsValueSetInBitfield)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public void RunTexConv(string strFinalWidth, string strFinalHeight, string strFinalDDS, string strSourceDDS)
        {

           /*
            string strAlphaArgument;
            if (IsAlphaPresent(strSourceDDS))
            {
                strAlphaArgument = "-pmalpha ";
                MessageBox.Show(strFinalDDS + " Has an alpha channel!");
            }
            else
            {
                strAlphaArgument = "";
                MessageBox.Show(strFinalDDS + " No alpha found.");
            }
            */

            Process processTexConv = new Process(); // Since TexConv kills itself, we don't have to do shit in a using{} block.
            processTexConv.StartInfo.FileName = System.IO.Directory.GetCurrentDirectory() + "\\texconv.exe";

            //Sanitize the file path, so we can get the folder of the .dds file.
            //No idea why Path.GetDirectoryName() throws an exception when there are ""s present.
            //Probably because they're invalid folder chars? Still shitty on Micro$oft's part.

            string strPathOfDDSFile = strFinalDDS.Remove(0, 1);
            strPathOfDDSFile = strPathOfDDSFile.Remove(strPathOfDDSFile.Length - 1);
            strPathOfDDSFile = Path.GetDirectoryName(strPathOfDDSFile);

            //Aaaaand now we add back the quotes to make things nice again.

            strPathOfDDSFile = strPathOfDDSFile.Insert(0, "\"");
            strPathOfDDSFile = strPathOfDDSFile.Insert(strPathOfDDSFile.Length, "\"");

            //The glory of not-curt-and-autistic variable names!
            //Really, if you can't figure out what the below line does...Yikes.

            string strMipmapLevels = GetNewMipmapLevels(strFinalWidth, strFinalHeight);

            //MessageBox.Show(this, strPathOfDDSFile, "Path for the DDS file");
            //MessageBox.Show(this, strFinalDDS, "DDS Name is");

            //You know how in a command line, the programs make you add conditions with a - before them?
            //We're just chucking that all in one big ass string.

            //The "ProcessWindowStyle.Hidden" part makes it so that we aren't bombarded with a bazillion command prompt windows
            //which would make the pc pretty unusable due to the window focus being tortured.

            string strArguments = "-sepalpha " + "-w " + strFinalWidth + " -h " + strFinalHeight + " -m " + strMipmapLevels + " -o " + strPathOfDDSFile + " " + strFinalDDS;
            processTexConv.StartInfo.Arguments = strArguments;
            processTexConv.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //MessageBox.Show(this, strArguments, "String passed is");
            processTexConv.Start();
            processTexConv.WaitForExit();
        }

        public void ProcessDDSDimensions(string strSourceWidth, string strSourceHeight, string strFinalDDS, string strSourceDDS)
        {
            strFinalDDS = '\u0022' + strFinalDDS + '\u0022';

            if (checkboxDoResize01.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes01, comboboxSourceYRes01, comboboxDestinationXRes01, comboboxDestinationYRes01)
                && strSourceWidth == comboboxSourceXRes01.Text
                && strSourceHeight == comboboxSourceYRes01.Text)
            {
                string strFinalWidth = comboboxDestinationXRes01.Text;
                string strFinalHeight = comboboxDestinationYRes01.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize02.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes02, comboboxSourceYRes02, comboboxDestinationXRes02, comboboxDestinationYRes02)
                && strSourceWidth == comboboxSourceXRes02.Text
                && strSourceHeight == comboboxSourceYRes02.Text)
            {
                string strFinalWidth = comboboxDestinationXRes02.Text;
                string strFinalHeight = comboboxDestinationYRes02.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize03.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes03, comboboxSourceYRes03, comboboxDestinationXRes03, comboboxDestinationYRes03)
                && strSourceWidth == comboboxSourceXRes03.Text
                && strSourceHeight == comboboxSourceYRes03.Text)
            {
                string strFinalWidth = comboboxDestinationXRes03.Text;
                string strFinalHeight = comboboxDestinationYRes03.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize04.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes04, comboboxSourceYRes04, comboboxDestinationXRes04, comboboxDestinationYRes04)
                && strSourceWidth == comboboxSourceXRes04.Text
                && strSourceHeight == comboboxSourceYRes04.Text)
            {
                string strFinalWidth = comboboxDestinationXRes04.Text;
                string strFinalHeight = comboboxDestinationYRes04.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize05.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes05, comboboxSourceYRes05, comboboxDestinationXRes05, comboboxDestinationYRes05)
                && strSourceWidth == comboboxSourceXRes05.Text
                && strSourceHeight == comboboxSourceYRes05.Text)
            {
                string strFinalWidth = comboboxDestinationXRes05.Text;
                string strFinalHeight = comboboxDestinationYRes05.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize06.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes06, comboboxSourceYRes06, comboboxDestinationXRes06, comboboxDestinationYRes06)
                && strSourceWidth == comboboxSourceXRes06.Text
                && strSourceHeight == comboboxSourceYRes06.Text)
            {
                string strFinalWidth = comboboxDestinationXRes06.Text;
                string strFinalHeight = comboboxDestinationYRes06.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize07.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes07, comboboxSourceYRes07, comboboxDestinationXRes07, comboboxDestinationYRes07)
                && strSourceWidth == comboboxSourceXRes07.Text
                && strSourceHeight == comboboxSourceYRes07.Text)
            {
                string strFinalWidth = comboboxDestinationXRes07.Text;
                string strFinalHeight = comboboxDestinationYRes07.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize08.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes08, comboboxSourceYRes08, comboboxDestinationXRes08, comboboxDestinationYRes08)
                && strSourceWidth == comboboxSourceXRes08.Text
                && strSourceHeight == comboboxSourceYRes08.Text)
            {
                string strFinalWidth = comboboxDestinationXRes08.Text;
                string strFinalHeight = comboboxDestinationYRes08.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize09.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes09, comboboxSourceYRes09, comboboxDestinationXRes09, comboboxDestinationYRes09)
                && strSourceWidth == comboboxSourceXRes09.Text
                && strSourceHeight == comboboxSourceYRes09.Text)
            {
                string strFinalWidth = comboboxDestinationXRes09.Text;
                string strFinalHeight = comboboxDestinationYRes09.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize10.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes10, comboboxSourceYRes10, comboboxDestinationXRes10, comboboxDestinationYRes10)
                && strSourceWidth == comboboxSourceXRes10.Text
                && strSourceHeight == comboboxSourceYRes10.Text)
            {
                string strFinalWidth = comboboxDestinationXRes10.Text;
                string strFinalHeight = comboboxDestinationYRes10.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize11.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes11, comboboxSourceYRes11, comboboxDestinationXRes11, comboboxDestinationYRes11)
                && strSourceWidth == comboboxSourceXRes11.Text
                && strSourceHeight == comboboxSourceYRes11.Text)
            {
                string strFinalWidth = comboboxDestinationXRes11.Text;
                string strFinalHeight = comboboxDestinationYRes11.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize12.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes12, comboboxSourceYRes12, comboboxDestinationXRes12, comboboxDestinationYRes12)
                && strSourceWidth == comboboxSourceXRes12.Text
                && strSourceHeight == comboboxSourceYRes12.Text)
            {
                string strFinalWidth = comboboxDestinationXRes12.Text;
                string strFinalHeight = comboboxDestinationYRes12.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize13.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes13, comboboxSourceYRes13, comboboxDestinationXRes13, comboboxDestinationYRes13)
                && strSourceWidth == comboboxSourceXRes13.Text
                && strSourceHeight == comboboxSourceYRes13.Text)
            {
                string strFinalWidth = comboboxDestinationXRes13.Text;
                string strFinalHeight = comboboxDestinationYRes13.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize14.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes14, comboboxSourceYRes14, comboboxDestinationXRes14, comboboxDestinationYRes14)
                && strSourceWidth == comboboxSourceXRes14.Text
                && strSourceHeight == comboboxSourceYRes14.Text)
            {
                string strFinalWidth = comboboxDestinationXRes14.Text;
                string strFinalHeight = comboboxDestinationYRes14.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }

            if (checkboxDoResize15.Checked
                && IsComboBoxTextInteger(comboboxSourceXRes15, comboboxSourceYRes15, comboboxDestinationXRes15, comboboxDestinationYRes15)
                && strSourceWidth == comboboxSourceXRes15.Text
                && strSourceHeight == comboboxSourceYRes15.Text)
            {
                string strFinalWidth = comboboxDestinationXRes15.Text;
                string strFinalHeight = comboboxDestinationYRes15.Text;

                RunTexConv(strFinalWidth, strFinalHeight, strFinalDDS, strSourceDDS);
                return;
            }
        }

        private void buttonProcessDDSFiles_Click(object sender, EventArgs e)
        {

            if (checkboxDoUseDestinationDir.Checked)
            {
                bCopyFiles = true;
            }

            else
            {
                bCopyFiles = false;
            }


            DirSearch(textboxSourceDir.Text);


            if (bCopyFiles)
            {
                for (int i = 0; i < listFilePaths.Count; i++)
                {
                    string strDuplicatedFilePath = listFilePaths[i];
                    strDuplicatedFilePath = strDuplicatedFilePath.Replace(textboxSourceDir.Text, textboxDestinationDir.Text);
										
                    listDestinationFilePaths.Add(strDuplicatedFilePath);
                    Directory.CreateDirectory(Path.GetDirectoryName(strDuplicatedFilePath));
                    File.Copy(listFilePaths[i], listDestinationFilePaths[i], true);
										
                    int iSourceDDSWidth;
                    int iSourceDDSHeight;
										
                    GetDDSDimensions(listDestinationFilePaths[i], out iSourceDDSWidth, out iSourceDDSHeight);
                    ProcessDDSDimensions(iSourceDDSWidth.ToString(), iSourceDDSHeight.ToString(), listDestinationFilePaths[i], listFilePaths[i]);
                    
                }

                MessageBox.Show(this, "We're done captain, ya can stop touchin' yerself now.");
            }

            else
            {
                MessageBox.Show(this, "Fuckin' add a destination path ya twonk");
            }

        }

        private void buttonDestinationDir_Click(object sender, EventArgs e)
        {
            if (dialogDestinationDir.ShowDialog() == DialogResult.OK)
            {
                textboxDestinationDir.Text = dialogDestinationDir.SelectedPath;
            }
        }

    }
}
