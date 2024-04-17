using GdPicture14;

namespace PdfKitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            @"D:\Nazer_Hussain\a_Reading_Files\Reading feb 27\Adient_Customer\IEE\INV-1905.pdf";
            @"D:\Nazer_Hussain\a_Reading_Files\Triway Pdf Reading\MITSUBA TEST PDF\test12.pdf";
            @"D:\Nazer_Hussain\a_Reading_Files\CargomenPDfReading\INV-1\INV and PL-10.pdf";
            @"D:\Nazer_Hussain\a_Reading_Files\CargomenPDfReading\1\INV-250.pdf";
            @"C:\Users\User\Downloads\MG01-1406-4-8.pdf";
            LicenseManager licenseManager = new LicenseManager();
            licenseManager.RegisterKEY("0414524817274085752481356");
             */
            string sourceFilePath = @"C:\Users\User\Downloads\INV_YAZAKI INDIA BOAT_1224.pdf";
            string ocrDataPath = @"D:\GdPicture.NET 14\Redist\OCR";
            var Products = new List<Dictionary<string, string>>();
            try
            {

                // Create an instance of GdPicturePDF
                using (GdPicturePDF gdpicturePDF = new GdPicturePDF())
                {
                    // Load the source document
                    if (gdpicturePDF.LoadFromFile(sourceFilePath) == GdPictureStatus.OK)
                    {
                        // Determine the number of pages
                        int pageCount = gdpicturePDF.GetPageCount();

                        // Loop through the pages of the source document
                        List<string> lines = new List<string>();
                        for (int i = 1; i <= pageCount; i++)
                        {
                            // Select a page and run the OCR process on it
                            gdpicturePDF.SelectPage(i);
                            gdpicturePDF.OcrPage("eng", ocrDataPath, "", 300);
                            string pageText = gdpicturePDF.GetPageText();
                            string[] pageLines = pageText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                            lines.AddRange(pageLines);
                            foreach (string item in lines)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine(lines.ToString());
                        }
                        using (var resultStream = new MemoryStream())
                        {
                            if (gdpicturePDF.SaveToStream(resultStream) == GdPictureStatus.OK)
                            {
                                //resultBytes = resultStream.ToArray();
                                Console.WriteLine("OCR completed successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to perform OCR.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to load the source PDF.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing page: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }
}


