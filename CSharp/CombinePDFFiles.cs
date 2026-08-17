  // 2. Get files from employee folder and combine into especific PDF file
  string DestinataionSubFolder = @"C:\Users\vijuribe\Applications\Karina\Files2\";
  DirectoryInfo subdirectory = new DirectoryInfo(DestinataionSubFolder);
  DirectoryInfo[] subDestinationDirectories = subdirectory.GetDirectories();

  string sourcePdf = string.Empty;    
  string outputPdf = "Alex Luna PDF Combinado.pdf"; // REMOVE HARD CODED FILE NAME
  bool containComb = false;
  //Alex Luna PDF Combinado.pdf

  foreach (DirectoryInfo subDir in subDestinationDirectories)
  {
      string empFolder = DestinataionSubFolder + subDir.Name + @"\";
      Console.WriteLine("Inside Files2: " + subDir.Name);

      if(Directory.EnumerateFiles(empFolder, "*.pdf").Count() <= 0)
      {
          continue;
      }

      // validacion que verifique si existe archivo "Combinado" si no existe, lo debe crear
      foreach(string files in Directory.EnumerateFiles(empFolder))
      {
          string combFile = Path.GetFileName(files);

          if(combFile.Contains("Combinado", StringComparison.OrdinalIgnoreCase))
          {
              containComb = true;
          }
      }

      if(!containComb)
      {
          // Crear Archivo PDF para combinar archivos
          continue;
      }

      outputPdf = empFolder + outputPdf;

      foreach (string filePath in Directory.EnumerateFiles(empFolder, "*.pdf"))
      {
          
          string empFile = Path.GetFileName(filePath);
          if(empFile.Substring(0,5) == subDir.Name.Substring(0,5))
          {
              sourcePdf = empFolder + Path.GetFileName(filePath);

              // 1. Open the file you want to insert pages into (Modify mode)
              using (PdfDocument targetDoc = PdfReader.Open(outputPdf, PdfDocumentOpenMode.Modify))
              {
                  // 2. Open the file you want to copy pages from (Import mode)
                  using (PdfDocument sourceDoc = PdfReader.Open(sourcePdf, PdfDocumentOpenMode.Import))
                  {
                      // Define where you want to "paste" the pages (e.g., at index 2, which is page 3)
                      int insertionIndex = targetDoc.PageCount;                       

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
                  targetDoc.Save(outputPdf);
              }
          }
      }

      containComb = false;

      //if (subDir.Name.StartsWith(noEmpleado))
      //{
      //    sourceFolder = filePath;
      //    DestinataionEmp += subDir.Name + @"\" + nameSourceFile;
      //    File.Move(sourceFolder, DestinataionEmp);
      //}
  }
