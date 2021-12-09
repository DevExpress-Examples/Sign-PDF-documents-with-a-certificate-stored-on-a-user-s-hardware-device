using DevExpress.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SignPDFWithHardwareCertificate
{
    class Program
    {
        static void Main(string[] args)
        {
            X509Certificate2 cert = GetCertificate();
            if (cert != null)
            {
                SignPDF(cert);
            }
        }
        static X509Certificate2 GetCertificate()
        {
            //get a certificate from a Windows Store
            //You can adapt this code to read a certificate from SmartCard or USB Token
            // https://stackoverflow.com/questions/63086592/how-to-enter-pin-for-x509certificate2-certificate-programmatically-when-signing
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection selectedCertificates = X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);
            if (selectedCertificates.Count == 0)
            {
                Console.WriteLine("There are no installed certificates on this machine.");
                return null;
            }

            foreach (var certificate in selectedCertificates)
            {
                if (certificate.HasPrivateKey)
                    return certificate;
            }
                        
            return null;
        }
        static void SignPDF(X509Certificate2 cert)
        {
            using (var signer = new PdfDocumentSigner(File.OpenRead("Demo.pdf")))
            {
                Pkcs7Signer pkcs7Signature = new Pkcs7Signer(cert, DevExpress.Office.DigitalSignatures.HashAlgorithmType.SHA256);
                var signatureFieldInfo = new PdfSignatureFieldInfo(1);
                signatureFieldInfo.Name = "SignatureField";
                signatureFieldInfo.SignatureBounds = new PdfRectangle(20, 20, 150, 150);
                var cooperSignature = new PdfSignatureBuilder(pkcs7Signature, signatureFieldInfo);
                cooperSignature.SetImageData(System.IO.File.ReadAllBytes("JaneCooper.jpg"));

                signer.SaveDocument("SignedDocument.pdf", cooperSignature);
            }
            Process.Start("SignedDocument.pdf");
        }
    }
}
