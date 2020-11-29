using System;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;


namespace PdfMerge
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = Directory.GetCurrentDirectory() + @"\";
            string[] files = Directory.GetFiles(path, @"*.pdf");
            // Iterate files            
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }
            // If there are no available PDFs in the directory, exit.
            if (files.Length < 1)
            {
                Console.WriteLine("No PDF Files found :( .. ");
                Console.ReadKey(true);
                System.Environment.Exit(0);
            }
            System.Console.WriteLine("Ok, I've found these files:");
            // Get filename from user
            Console.WriteLine("Enter a name for your new PDF file: ");
            string fileName = path + Console.ReadLine() + ".pdf";
            Console.WriteLine("Your full file name is {0} ", fileName);
            Console.WriteLine("Is that Ok? [Y/N]");
            var answer = Console.ReadLine().ToLower();
            if (answer == "n")
            {
                Console.WriteLine("Enter a name for your new PDF file: ");
                fileName = path + Console.ReadLine() + ".pdf";
                Console.WriteLine("Your full file name is {0} ", fileName);
            }

            PdfDocument outputDocument = new PdfDocument();

            foreach (string file in files)
            {
                Console.Write(file);

                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }
            // Save the document...
            outputDocument.Save(fileName);
            // ...and start a viewer.
            Process.Start(fileName);

        }
    }
}


