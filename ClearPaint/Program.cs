using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ClearPaint
{
    class Program
    {
        static void Main(string[] args)
        {
            // int len = args.Length;
            Console.WriteLine("Start program...");
            foreach (string arg1 in args)
            {
                string[] parts = arg1.Split('\\');
                int length = parts.Length;
                // C:\dir\di2\dir3\file.jpg
                if (Directory.Exists(arg1))
                {
                    if (Directory.Exists(arg1 + "_new"))
                        Directory.Delete(arg1 + "_new", true);
                    Directory.CreateDirectory(arg1 + "_new");
                    Console.WriteLine("[] Load directory: " + arg1);
                    string[] files = Directory.GetFiles(arg1);
                    int lenFiles = files.Length;
                    int iF = 0;
                    foreach (string file in files)
                    {
                        iF++;
                        string fileName = file.Split('\\').Last();
                        MethodClearJPEG(arg1, fileName, true);
                        Console.WriteLine($"[{iF}/{lenFiles}] File updated: " + file);
                    }
                }
                else if (File.Exists(arg1))
                {
                    string dir = string.Empty;
                    string file = string.Empty;
                    for (int i = 0; i < length; i++)
                    {
                        if (i < length - 1)
                        {
                            if (!string.IsNullOrWhiteSpace(dir))
                                dir += '\\';
                            dir += parts[i];
                        }
                        else
                        {
                            file = parts[i];
                        }
                    }
                    MethodClearJPEG(dir, file);
                }
            }
            Console.WriteLine("Finish. Press any key.");
            Console.ReadKey();
        }

        public static bool MethodClearJPEG(string dir, string file, bool isNewDir = false)
        {
            bool result = true;
            // Console.WriteLine("Load a file: " + dir + "\\" + file);
            Image image = null;
            try
            {
                image = Image.FromFile(dir + "\\" + file, true);
                // Console.WriteLine("File Loaded!");

                string type = file.Split('.').Last();
                PropertyItem[] propItems = image.PropertyItems;

                foreach (PropertyItem propItem in image.PropertyItems)
                {
                    propItem.Len = 0;
                    propItem.Value = new byte[0];
                    image.SetPropertyItem(propItem);
                }
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                
                if (isNewDir)
                    image.Save(dir + "_new\\" + file, image.RawFormat);
                else
                    image.Save(dir + "\\" + file + ".new." + type.ToLower(), image.RawFormat);
                // Console.WriteLine("File Saved!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex: " + ex.ToString());
                result = false;
            }
            finally
            {
                if (image != null)
                    image.Dispose();
            }
            return result;
        }
    }
}
