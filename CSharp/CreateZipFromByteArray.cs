using System;
using System.IO;
using System.IO.Compression;

public class ZipFileCreator
{
    public static byte[] CreateZipFromByteArray(byte[] fileData, string fileNameWithinZip)
    {
        // 1. Create a MemoryStream to hold the final ZIP file data
        using (var outputStream = new MemoryStream())
        {
            // 2. Wrap the MemoryStream in a ZipArchive using 'Create' mode
            using (var archive = new ZipArchive(outputStream, ZipArchiveMode.Create, true))
            {
                // 3. Create a new entry (file) inside the ZIP archive
                var zipEntry = archive.CreateEntry(fileNameWithinZip, CompressionLevel.Optimal);

                // 4. Open the entry stream and write your raw byte array into it
                using (var entryStream = zipEntry.Open())
                {
                    entryStream.Write(fileData, 0, fileData.Length);
                }
            } // The archive must be disposed/closed HERE before reading outputStream

            // 5. Return the completed ZIP file as a byte array
            return outputStream.ToArray();
        }
    }

    public static void Main()
    {
        // Example: Dummy text file data converted to a byte array
        byte[] originalBytes = System.Text.Encoding.UTF8.GetBytes("Hello, this is file content!");
        
        // Generate the ZIP byte array
        byte[] zippedBytes = CreateZipFromByteArray(originalBytes, "hello.txt");

        // Optional: Save the resulting ZIP byte array directly to disk
        File.WriteAllBytes("OutputArchive.zip", zippedBytes);
        Console.WriteLine("ZIP file created successfully!");
    }
}
