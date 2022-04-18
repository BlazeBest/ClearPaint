using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ClearPaint
{
    class Program
    {
        static void Main(string[] args)
        {
            int len = args.Length;
            Console.WriteLine("Start program...");
            foreach (string fileName in args)
            {
                Console.WriteLine("Load a file: " + fileName);
                Image image = null;
                try
                {
                    image = Image.FromFile(fileName, true);
                    Console.WriteLine("File Loaded!");

                    string type = fileName.Split('.').Last();
                    PropertyItem[] propItems = image.PropertyItems;

                    foreach (PropertyItem propItem in image.PropertyItems)
                    {
                        propItem.Len = 0;
                        propItem.Value = new byte[0];
                        image.SetPropertyItem(propItem);
                    }
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    //
                    image.Save(fileName + ".new." + type.ToLower(), ImageFormat.Jpeg);
                    Console.WriteLine("File Saved!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ex: " + ex.ToString());
                }
                finally
                {
                    if (image != null)
                        image.Dispose();
                }
            }
            Console.WriteLine("Finish. Press any key.");
            Console.ReadKey();
        }
    }
}
