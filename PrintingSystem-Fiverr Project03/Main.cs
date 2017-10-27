using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PrintingSystem_Fiverr_Project03
{


    public partial class Main : Form
    {
        string[] factuaraFiles=null;
        string[] garantieFiles = null;
        string[] otherFiles = null;
        string[] allFiles = null;
        

        string filePath;
        
        public Main()
        {
            InitializeComponent();
        }
        bool DoesntContain(FileInfo fileInfo, string text)
        {
            using (StreamReader r = fileInfo.OpenText())
            {
                var contents = r.ReadToEnd();
                return !contents.Contains(text);
            }
        }
        private void fileDrop_DragDrop(object sender, DragEventArgs e)
        {
           
            string[] files = null;
            files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any() && GetFileNames(files.First(), "*.pdf")!=null)
            {
                if (fileDrop.Items!=null) {
                    fileDrop.Items.Clear();
                }
                filePath = files.First();
                updateFiles(filePath);
                showList(allFiles);
               
            }
        }

        private void updateFiles(string newFileLocation) {
            if (allFiles == null)
            {
                allFiles = GetFileNames(newFileLocation, "*.pdf");
                factuaraFiles = GetFileNames(newFileLocation, "Factura*.pdf");
                garantieFiles = GetFileNames(newFileLocation, "Garantie*.pdf");
                otherFiles = allFiles.SkipWhile(a => a.Contains("Factura") || a.Contains("Garantie")).ToArray();
            }
            else if(allFiles!=null){
                allFiles = allFiles.Union(GetFileNames(newFileLocation, "*.pdf")).ToArray();
                Console.WriteLine(allFiles);
                factuaraFiles = factuaraFiles.Union(GetFileNames(newFileLocation, "Factura*.pdf")).ToArray();
                garantieFiles = garantieFiles.Union( GetFileNames(newFileLocation, "Garantie*.pdf")).ToArray();
                otherFiles = otherFiles.Union(allFiles.SkipWhile(a => a.Contains("Factura") || a.Contains("Garantie")).ToArray()).ToArray();
            }
        }
        private static string[] GetFileNames(string path, string filter)
        {
            
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path, filter);
                for (int i = 0; i < files.Length; i++)
                    files[i] = path+"/"+System.IO.Path.GetFileName(files[i]);

            }
            catch (IOException)
            {
                //MessageBox.Show("Please drag your pdf containing folder!(Don't drag your pdf's) ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                files = Directory.GetFiles(System.IO.Path.GetDirectoryName(path), filter);
               
            }
            return files;
        }

        void showList(string[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                fileDrop.Items.Add(p[i].ToString());
            }
        }

        private void fileDrop_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void createPrintList()
        {
            
            List<string> pOther = new List<string>();
            bool chkO = false;//check other
            int[] chkF=new int[factuaraFiles.Length];//check factuara
            List<string> page = new List<string>();
            try
            {
                for (int i = 0; i < factuaraFiles.Length; i++)
                {
                    string factuaraID = (System.IO.Path.GetFileName(factuaraFiles[i])).Substring(8, 9);
                    bool checkG = false;
                    for (int j = 0; j < garantieFiles.Length; j++)
                    {
                       
                        if (factuaraID.Equals((System.IO.Path.GetFileName(garantieFiles[j])).Substring(9, 9)))
                        {
                            
                            for (int h = 0; h < otherFiles.Length; h++)
                            {

                                int pageNo = ReadPdfFile(otherFiles[h], factuaraID);
                                if (pageNo != 0)
                                {
                                     page.Add(factuaraFiles[i]);
                                    page.Add(garantieFiles[j]);
                                    merge(page,"../../" + i + j + ".pdf");
                                    page.Clear();
                                    printD("../../" + i + j + ".pdf");
                                    printPN(otherFiles[h], pageNo);
                                    pOther.Add(otherFiles[h]);
                                    //MessageBox.Show(factuaraFiles[i]+" || "+garantieFiles[j]+" || "+ otherFiles[h]+" page no : "+ pageNo);
                                    chkO = true;;
                                    break;
                                    //file list and page no available here
                                }
                                else
                                {
                                    chkO = false;
                                }

                                
                            }
                            if(chkO==false)
                            {
                                 page.Add(factuaraFiles[i]);
                                 page.Add(garantieFiles[j]);
                                 merge(page, "../../" + i + j + ".pdf");
                                 page.Clear();
                                 printD("../../" + i + j + ".pdf");

                                //MessageBox.Show(factuaraFiles[i] + " || " + garantieFiles[j]);
                            }
                            checkG = true;
                            break;

                        }
                    }
                    if (checkG == false)
                    {
                        for (int h = 0; h < otherFiles.Length; h++)
                        {

                            int pageNo = ReadPdfFile(otherFiles[h], factuaraID);
                            if (pageNo != 0)
                            {
                                printS(factuaraFiles[i]);
                                printPN(otherFiles[h], pageNo);
                                pOther.Add(otherFiles[h]);
                                chkO = true; 

                               // MessageBox.Show(factuaraFiles[i] + " || " +  otherFiles[h] + " page no : " + pageNo);
                                break;
                                //file list and page no available here
                            }
                            else
                            {
                                chkO = false;
                            }
                        }
                        if (chkO == false)
                        {
                            printS(factuaraFiles[i]);

                            //MessageBox.Show(factuaraFiles[i]);
                        }
                    }
                }
                List<string> other = new List<string>();
                List<string> unique = pOther.Distinct().ToList();
                other.AddRange(otherFiles);
                other.RemoveAll(a=>a.StartsWith("Factura"));
                other.RemoveAll(a => a.StartsWith("Garantie"));
                
                foreach (var a in pOther)
                {
                    foreach(var b in other.ToList() )
                    {
                        if (a.Equals(b))
                        {
                            other.Remove(b);
                        }
                    }
                }
                printL(other);
               
               
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message,"Erorr",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
        }
       
       
        public void printD(string file)
        {
            
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(file);
 
            if(doc.PrintDocument.PrinterSettings.CanDuplex)
            {
                doc.PrintDocument.PrinterSettings.Duplex = Duplex.Vertical;
                doc.PrintDocument.Print();
                doc.Close();
            }
            else
            {
                MessageBox.Show("Your printer not supporting to duplex printing","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
        }
        public void printL(List<string> a)
        {
            foreach (var c in a)
            {
                Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                doc.LoadFromFile(c);
                doc.PrintDocument.Print();

                doc.Close();
            }
        }
        public void printS(string file)
        {
           
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(file);
            doc.PrintDocument.Print();

            doc.Close();
           
        }
        public void printPN(string file,int pN)
        {
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(file);

            doc.PrintFromPage = pN;
            doc.PrintToPage = pN;
            doc.PrintDocument.Print();

            doc.Close();
        }



        public static void merge(List<String> InFiles, String OutFile)
        {
            using (FileStream stream = new FileStream(OutFile, FileMode.Create))
            using (Document doc = new Document())
            using (PdfCopy pdf = new PdfCopy(doc, stream))
            {
                doc.Open();

                PdfReader reader = null;
                PdfImportedPage page = null;

                //fixed typo
                InFiles.ForEach(file =>
                {
                    reader = new PdfReader(file);

                    for (int i = 0; i < reader.NumberOfPages; i++)
                    {
                        page = pdf.GetImportedPage(reader, i + 1);
                        pdf.AddPage(page);
                    }

                    pdf.FreeReader(reader);
                    reader.Close();
                });
            }
        }
        public int ReadPdfFile(string fileName, String searthText)//read pdf and return page number 
        {
            
            int pageNo = 0;
            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                    string currentPageText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                    if (currentPageText.Contains(searthText))
                    {
                        pageNo = page;
                    }
                }
                pdfReader.Close();
            }
            return pageNo;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            createPrintList();
            //foreach (var file in Directory.GetFiles("../../", "*.pdf", SearchOption.AllDirectories))
            //{
            //    File.Delete(file);
            //}
        }
    }
}
