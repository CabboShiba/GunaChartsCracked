using GunaChartsPatcher.Properties;
using System;
using System.Diagnostics;
using System.IO;

namespace GunaChartsPatcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = $"[{DateTime.Now}] GunaChartsPatcher by https://github.com/CabboShiba";
            if (!File.Exists(Environment.CurrentDirectory + @"\ReadBeforeUse.txt"))
            {
                File.WriteAllText(Environment.CurrentDirectory + @"\ReadBeforeUse.txt", "Run this program in the same folder as \"packages\" folder. You can find it in your project directories.\nOnce you have found it, you can run the program");
            }
            string TempFile = null;
            int count = 0;
            Log("Scanning for \"Guna.Charts.WinForms.1.0.8\" Directory...", "INFO", ConsoleColor.Yellow);
            string dir = Environment.CurrentDirectory + @"\packages\Guna.Charts.WinForms.1.0.8\lib";
            if (!Directory.Exists(dir))
            {
                Log($"Could not find {dir}. Please be sure that this program is running in the same folder as packages folder.\nPress enter to leave...", "ERROR", ConsoleColor.Red);
                Console.ReadLine();
                Process.GetCurrentProcess().Kill();
            }

            foreach (var item in Directory.GetDirectories(dir))
            {
                TempFile = item + @"\Guna.Charts.WinForms.dll";
                if (File.Exists(TempFile))
                {
                    Log($"Found Guna.Charts.WinForms.dll in {item} | Patching...", "INFO", ConsoleColor.Cyan);
                    try
                    {
                        File.Delete(TempFile);
                        Log("Succesfully deleted original Guna.Charts.WinForms.dll | Replacing...", "INFO", ConsoleColor.Yellow);
                    }
                    catch (Exception ex)
                    {
                        Log($"Could not delete: {TempFile}\n{ex.Message}", "ERROR", ConsoleColor.Red);
                    }
                }
                try
                {
                    File.WriteAllBytes(TempFile, (byte[])Properties.Resources.ResourceManager.GetObject("Guna_Charts_WinForms"));
                    count++;
                    Log("Succesfully copied cracked .DLL in: " + TempFile, "SUCCESS", ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    Log($"Could not overwrite cracked .DLL: {ex.Message}", "ERROR", ConsoleColor.Red);
                }
            }
            Log($"Succesfully patched {count} Guna.Charts.WinForms.dll File.", "SUCCESS", ConsoleColor.Green);
            Log("Finished. Press enter to leave...", "FINISH", ConsoleColor.Yellow);
            Console.ReadLine();
            Process.GetCurrentProcess().Kill();
        }



        public static void Log(string Data, string Type, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} - {Type}] {Data}");
            Console.ResetColor();
        }
    }
}
