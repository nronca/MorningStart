using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Threading.Tasks;
using System.Management;
using System.ServiceProcess;

namespace MorningStart
{

    public partial class frmMorningStart : Form
    {
        Boolean fblnFileExists;
        char DELIM = Globals.DELIMITER;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetExitCodeProcess(IntPtr hProcess, out uint ExitCode);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        public static extern int ExitWindowsEx(int operationFlag, int operationReason);

        public frmMorningStart()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            string lstrExePath;
            string lstrExe;
            string[] lstrExePathArray;
            int lintProgCount=0;
            Process lprocess;

            fblnFileExists = Globals.CheckForTextFile(Globals.mstrFilePath);
            if (!fblnFileExists)
            {
                MessageBox.Show("No File Found To Find Path To Exes. Please Create One.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Globals.GetLineCount(Globals.mstrFilePath);
                using (FileStream fs = File.Open(Globals.mstrFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] lbytInfo = new byte[1024];
                    UTF8Encoding lutfTemp = new UTF8Encoding(true);

                    while (fs.Read(lbytInfo, 0, lbytInfo.Length) > 0)
                    {
                        lstrExePath = lutfTemp.GetString(lbytInfo);
                        lstrExePathArray = lstrExePath.Split(',');
                        Globals.mintProgramCount = lstrExePathArray.Length;
                        if (Globals.mintProgramCount != 0)
                        {
                            foreach (string str in lstrExePathArray)
                            {
                                if (lintProgCount == Globals.mintProgramCount) { break; }
                                lstrExe = str.Replace("\r\n", "");
                                if (lstrExe.Trim() == "")
                                    continue;
                                else
                                {
                                    try
                                    {
                                        lprocess = Process.Start(lstrExe);
                                        //lprocess.WaitForInputIdle();
                                        System.Threading.Thread.Sleep(Globals.mintTimeout);
                                        AutoCompleteExeLogins(lstrExe, lprocess);
                                    }
                                    catch (Exception err)
                                    {
                                        MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                lintProgCount++;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Programs Found. Please Add Exes.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fs.Close();
                            return;
                        }

                    }
                    fs.Close();
                }
                System.Threading.Thread.Sleep(Globals.mintTimeout);
                this.Dispose();
            }
        }

        private void frmMorningStart_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            timer1.Enabled = false;
        }

        private void cmdCloseAll_Click(object sender, EventArgs e)
        {
            Process lprocess;
            int ltabs;
            DialogResult lDR;
            uint exitCode;
            Shell32.Shell shell;
            String lstrProcessServiceName;
            bool lblnFoundServiceName;
            ServiceController[] scServices;
            var ldesktopPrograms = new List<DesktopWindow>();
            uint pid;
            uint temp = 0;
            Process tempProcess;

            lDR = MessageBox.Show("You Are About To Close All Active Programs.\n Continue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (lDR == System.Windows.Forms.DialogResult.No)
            {
                return; //get out, we don't want to continue
            }

            //shell = new Shell32.Shell();
            //scServices = ServiceController.GetServices();
            
            lprocess = Process.GetCurrentProcess();

            ldesktopPrograms = User32Helper.GetDesktopWindows();

            //go thru each window, if it's true and has a title then try to kill it
            for (int i=0; i< ldesktopPrograms.Count; i++) 
            {
                if (ldesktopPrograms[i].IsVisible && ldesktopPrograms[i].Title.Trim() != "")
                {
                    pid = GetWindowThreadProcessId(ldesktopPrograms[i].Handle, out temp);
                    try
                    {
                        tempProcess = Process.GetProcessById((int)temp);
                        if (tempProcess.Id != lprocess.Id && tempProcess.MainWindowHandle != IntPtr.Zero && tempProcess.ProcessName != "explorer")
                        {
                            try 
                            { 
                                tempProcess.CloseMainWindow();
                                System.Threading.Thread.Sleep(Globals.mintTimeout);    
                            }

                            catch (InvalidOperationException err) { tempProcess.Kill(); }

                            catch (Win32Exception err) { tempProcess.Kill(); }

                            catch (Exception err)
                            {
                                tempProcess.Kill(); //no error reporting just kill it
                            }

                            //if we haven't thrown an error and the program still hasn't closed, kill it
                            if (!tempProcess.HasExited)
                            {
                                tempProcess.Kill();
                                System.Threading.Thread.Sleep(Globals.mintTimeout);
                            }
                        }
                    }
                    catch (System.ArgumentException err) 
                    {
                        //MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //just skip this ID
                    }
                }
            }

            System.Threading.Thread.Sleep(Globals.mintTimeout);
            ExitWindowsEx(0, 0);
            this.Dispose();
            return;

            /*
             * 
             * Dead Code Below
             * First attempt at gracefully exiting programs
             */

            foreach (Process p in Process.GetProcesses(System.Environment.MachineName))
            {   
                if ((p.Id != lprocess.Id && p.MainWindowHandle != IntPtr.Zero && p.ProcessName != "explorer"))//make sure we aren't closing this program and that the current process has a GUI window
                {
                        
                    try 
                    { 
                        p.CloseMainWindow();
                        System.Threading.Thread.Sleep(Globals.mintTimeout);    
                    }

                    catch (InvalidOperationException err) { p.Kill(); }

                    catch (Win32Exception err) { p.Kill(); }

                    catch (Exception err)
                    {
                        //MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        p.Kill(); //screw error reporting just kill it
                    }

                    //}
                    if (!p.HasExited)
                    {
                        p.WaitForExit(Globals.mintTimeout*2);

                        try
                        {
                            GetExitCodeProcess(p.Handle, out exitCode);
                            {
                                if (exitCode != 0) p.Kill();
                            }
                        }
                        catch (Win32Exception err) { p.Kill(); }

                        catch (InvalidOperationException err) { p.Kill(); }

                        catch (Exception err) { p.Kill(); }

                        System.Threading.Thread.Sleep(Globals.mintTimeout);
                        ltabs = 0;

                        /*while (!p.HasExited) //means that most likely we have a popup window asking if we are sure we want to exit
                        {                    //try enter if control is on the yes button
                            //else tab a few times in case the control doesn't default to the yes button
                            TryTabbing(ltabs);
                            SendKeys.Send("{ENTER}");
                            System.Threading.Thread.Sleep(Globals.mintTimeout);

                            ltabs++;

                            if (ltabs > 3) //we've tried closing the program nicely a few times, just kill it at this point ¯\_(ツ)_/¯
                            {
                                try { p.Kill(); }
                                catch (Exception err) { MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                System.Threading.Thread.Sleep(Globals.mintTimeout);
                                break;
                            }
                            try { p.CloseMainWindow(); }
                            catch (Exception err) { p.Kill(); }// MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            System.Threading.Thread.Sleep(Globals.mintTimeout);
                        }*/
                    }
                }
            }
            System.Threading.Thread.Sleep(Globals.mintTimeout);
            this.Dispose();
        }

        //this will try tabbing the number of times we pass in to eventually hit enter on a logout screen
        private void TryTabbing(int NumberOfTabs)
        {
            for (int i = 0; i < NumberOfTabs; i++)
            {
                SendKeys.Send("{TAB}");
                System.Threading.Thread.Sleep(Globals.mintTimeout);
            }
        }

        //can't run this code in debug mode
        private void AutoCompleteExeLogins(String path, Process p)
        {
            int lintInstr;
            /*Process lprocess;
            Process[] lByName;
            IntPtr lintptr;
            string lstrMessage;
            */

            lintInstr = path.IndexOf("program.exe");
            if (lintInstr != -1)
            {
                SendKeys.Send("{ENTER}");
                timer1.Enabled = true;
                System.Threading.Thread.Sleep(Globals.mintTimeout);
                return;
            }

            //might eventually come back to this one

            /*lintInstr = path.IndexOf("Login.exe");
            if (lintInstr != -1)
            {

                //p = Process.GetProcessesByName(path)[];
                lprocess = Process.GetCurrentProcess();
                lByName = Process.GetProcessesByName(p.ProcessName); //will be empty if process isn't running
                //lintptr = Globals.FindWindow("", "");
                lintptr = Globals.FindWindow(null, "");
                //pid looks like 7892
                //lintptr = Globals.FindWindow("", "");
                if (lintptr != IntPtr.Zero)
                {
                    //luserNamePtr = Globals.FindWindowEx(lintptr, IntPtr.Zero, "", null);
                    //if (!luserNamePtr.Equals(IntPtr.Zero))
                    //{
                    return;    
                    Globals.SetForegroundWindow(lintptr);
                    lstrMessage = "{TAB}CSS{TAB}pass1{TAB}0{TAB}0{ENTER}";

                    for (int i = 0; i < lstrMessage.Length; i++)
                    {
                        //Globals.PostMessage(lintptr, 0x100, Globals.VkKeyScan(lstrMessage[i]), 0);
                    }   
                        //System.Threading.Thread.Sleep(500);
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("username");
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("{TAB}");
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("password");
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("{TAB}");
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("0");
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("{TAB}");
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("0");
                        //System.Threading.Thread.Sleep(500);
                        //SendKeys.SendWait("{ENTER}");
                        //System.Threading.Thread.Sleep(500);
                        return;
                    //}
                }
            }*/
        }
    }

    public static class Globals
    {
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern byte VkKeyScan(char ch);

        public static String mstrFilePath = Application.StartupPath + "\\exes\\exes.dat"; //"C:/Users/Nathan Ronca/Documents/MorningStart/exes.dat";
        public const char DELIMITER = ',';
        public static int mintProgramCount = 0;
        public static bool mblnDBLoginEnter = false;
        public static int mintTimeout = 250; //ms we wait for anything

        public static Boolean CheckForTextFile(string lstrFilePath)
        {
            if (File.Exists(lstrFilePath)) { return true; }

            else { return false; }

        }

        public static void GetLineCount(string lstrFilePath)
        {
            string lstrExePath;
            string lstrExe;
            string[] lstrExePathArray;

            using (FileStream fs = File.Open(lstrFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
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
                        mintProgramCount++;    
                    }
                }
                fs.Close();
            }
        }
    }
}

/*
 * The following are experiments to get window handles to close out of running applications without blowing up windows
 */
 
public class DesktopWindow
{
    public IntPtr Handle { get; set; }
    public string Title { get; set; }
    public bool IsVisible { get; set; }
}

public class User32Helper
{
    public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

    [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
        IntPtr lParam);

    public static List<DesktopWindow> GetDesktopWindows()
    {
        var collection = new List<DesktopWindow>();
        EnumDelegate filter = delegate(IntPtr hWnd, int lParam)
        {
            var result = new StringBuilder(255);
            GetWindowText(hWnd, result, result.Capacity + 1);
            string title = result.ToString();

            var isVisible = !string.IsNullOrEmpty(title) && IsWindowVisible(hWnd);

            collection.Add(new DesktopWindow { Handle = hWnd, Title = title, IsVisible = isVisible });

            return true;
        };

        EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
        return collection;
    }
}