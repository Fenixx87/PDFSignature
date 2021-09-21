using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DevExpress.Pdf;

namespace PDFSignature {
    class Program {
        static void Main(string[] args) {

            using (PdfDocumentProcessor documentProcessor = new PdfDocumentProcessor()) {
                //documento a firmar en pdf...*obligatorio
                documentProcessor.LoadDocument(@"..\..\docTest.pdf");
                //certificado de la firma (documento con la firma electronica)...*obligatorio, clave de la firma...*obligatorio
                X509Certificate2 certificate = new X509Certificate2(@"..\..\SignDemo.pfx", "dxdemo");
                //imagen de la firma...*obligatorio
                byte[] imageData = File.ReadAllBytes("..\\..\\firma1.jpg");
                //posicion de la firma respecto al numero de pagina del documento...*obligatorio
                int pageNumber = 1;
                //angulo  de la firma
                //int angleInDegrees = 0;
                //conversion a radianes
                //double angleInRadians = angleInDegrees * (Math.PI / 180);
                //coordenadas de la firma ((x,y),relacion de aspecto?,angulo de la firma)
                //punto 0 fuera de la pagina
                PdfOrientedRectangle signatureBounds = new PdfOrientedRectangle(new PdfPoint(70, 90), 70, 50 /*,angleInRadians*/);
                //metodo firma de pdf 
                PdfSignature signature = new PdfSignature(certificate, imageData, pageNumber,signatureBounds);

                //detalles de la firma
                signature.Location = "USA";
                signature.ContactInfo = "john.smith@example.com";
                signature.Reason = "Approved";

                //retorno de documento firmado
                documentProcessor.SaveDocument(@"..\..\SignedTest.pdf", new PdfSaveOptions() { Signature = signature });
            }
        }
    }
}
