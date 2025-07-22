using iTextSharp.text;
using iTextSharp.text.pdf;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using static NPOI.HSSF.Util.HSSFColor;

namespace CamlifeAPI1.Class
{
    public class TextSharp
    {
        public static  void PDF( HttpContext contact)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.Add(new Paragraph("Hello World"));
                document.Close();
                writer.Close();
                contact. Response.ContentType = "pdf/application";
                contact.Response.AddHeader("content-disposition",
                "attachment;filename=First PDF document.pdf");
                contact. Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);

            }
        }

    }
}