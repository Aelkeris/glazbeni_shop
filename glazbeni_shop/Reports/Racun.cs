using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Hosting;
using glazbeni_shop.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace glazbeni_shop.Reports
{
    public class Racun
    {
        public byte[] Podaci { get; private set; }

        public Racun(List<Album> album, string prezime,string ime,string adresa,string mail,string ulica)
        {
            Document pdfDokument = new Document(PageSize.A4, 50, 50, 20, 50);
            
            MemoryStream memStream = new MemoryStream();
            PdfWriter.GetInstance(pdfDokument, memStream).CloseStream = false;

            pdfDokument.Open();
            
            BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, false);
            Font header = new Font(font, 12, Font.NORMAL, BaseColor.DARK_GRAY);
            Font naslov = new Font(font, 14, Font.BOLDITALIC, BaseColor.BLACK);
            Font tekst = new Font(font, 10, Font.NORMAL, BaseColor.BLACK);

            var logo = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/Slike/slika.png"));
            
            logo.Alignment = Element.ALIGN_RIGHT;
            logo.ScaleAbsoluteWidth(150); 
            logo.ScaleAbsoluteHeight(150);
            pdfDokument.Add(logo);

            Paragraph p = new Paragraph("MEVsic račun" + "\nPrezime : " + prezime + "\nIme : " + ime + "\nMail : " + mail + "\nMjesto : " + adresa + "\nUlica : " + ulica, header);
            
            p.SpacingBefore = -75;
            pdfDokument.Add(p);

            
            p = new Paragraph("NARUDŽBA", naslov);
            p.Alignment = Element.ALIGN_CENTER;
            p.SpacingBefore = 30;
            p.SpacingAfter = 30;
            pdfDokument.Add(p);

            PdfPTable t = new PdfPTable(3); 
            t.WidthPercentage = 100; 
            t.SetWidths(new float[] { 2, 2, 1 }); 

          
            t.AddCell(VratiCeliju("Izvođač", tekst, BaseColor.LIGHT_GRAY, true));
            t.AddCell(VratiCeliju("Naziv albuma", tekst, BaseColor.LIGHT_GRAY, true));
            t.AddCell(VratiCeliju("Cijena", tekst, BaseColor.LIGHT_GRAY, true));


           
            double cijena = 0;
            foreach (var s in album)
            {
                t.AddCell(VratiCeliju(s.nazivIzvodaca.nazivizvodaca, tekst, BaseColor.WHITE, false));
                t.AddCell(VratiCeliju(s.imealbuma.ToString(), tekst, BaseColor.WHITE, false));
                t.AddCell(VratiCeliju(s.cijena.ToString() + " HRK", tekst, BaseColor.WHITE, false));
                cijena += s.cijena;
            }

            
            pdfDokument.Add(t);

            p = new Paragraph("Bazna cijena:  " + cijena * 0.75 + " HRK" + "\nPorez:  " + cijena * 0.25 + " HRK" + "\nUkupna cijena:  " + cijena + " HRK", header);
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 10;
            pdfDokument.Add(p);

            p = new Paragraph("Čakovec, " + System.DateTime.Now.ToString("dd.MM.yyyy"), header);
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 50;
            pdfDokument.Add(p);

            
            pdfDokument.Close();
            Podaci = memStream.ToArray();
        }

        
        private PdfPCell VratiCeliju(string labela, Font font, BaseColor boja, bool wrap)
        {
            PdfPCell c1 = new PdfPCell(new Phrase(labela, font));
            c1.BackgroundColor = boja;
            c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            c1.Padding = 5;
            c1.NoWrap = wrap;
            return c1;
        }
    }

}