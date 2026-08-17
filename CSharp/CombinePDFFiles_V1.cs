using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

class Program
{
    static void Main()
    {
        string targetPath = @"C:\path\to\target_content.pdf";
        string sourcePath = @"C:\path\to\source_pages.pdf";
        string outputPath = @"C:\path\to\final_merged.pdf";

        // 1. Open the file you want to insert pages into (Modify mode)
        using (PdfDocument targetDoc = PdfReader.Open(targetPath, PdfDocumentOpenMode.Modify))
        {
            // 2. Open the file you want to copy pages from (Import mode)
            using (PdfDocument sourceDoc = PdfReader.Open(sourcePath, PdfDocumentOpenMode.Import))
            {
                // Define where you want to "paste" the pages (e.g., at index 2, which is page 3)
                int insertionIndex = 2; 

                // 3. Loop through the source pages and insert them
                foreach (PdfPage page in sourceDoc.Pages)
                {
                    // If inserting sequentially at a specific index, use InsertPage:
                    targetDoc.InsertPage(insertionIndex, page);
                    insertionIndex++; // Increment to keep the relative order of pasted pages

                    // ALTERNATIVE: To simply append pages to the very end, use:
                    // targetDoc.AddPage(page);
                }
            }

            // 4. Save the modified document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine("Pages pasted successfully!");
    }
}
