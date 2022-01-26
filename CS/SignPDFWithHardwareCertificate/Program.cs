using DevExpress.Pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DevExpress.Office.DigitalSignatures;

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
        else
          Console.WriteLine("There are no installed certificates on this machine.");
    }

    static X509Certificate2 GetCertificate()
    {
      // Get a certificate from a Windows Store
      X509Store store = new X509Store(StoreLocation.CurrentUser);
      store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

      // Display a dialog box to select a certificate from the Windows Store
      X509Certificate2Collection selectedCertificates =
              X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);
            
      // Get the first certificate that has a primary key
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
       // Create a PKCS#7 signature
       Pkcs7Signer pkcs7Signature = new Pkcs7Signer(cert, HashAlgorithmType.SHA256);
                
       // Create a signature field on the first page
       var signatureFieldInfo = new PdfSignatureFieldInfo(1);
                
       // Specify the field's name and location
       signatureFieldInfo.Name = "SignatureField";
       signatureFieldInfo.SignatureBounds = new PdfRectangle(20, 20, 150, 150);
                
       // Apply a signature to a newly created signature field
       var cooperSignature = new PdfSignatureBuilder(pkcs7Signature, signatureFieldInfo);
       cooperSignature.SetImageData(System.IO.File.ReadAllBytes("JaneCooper.jpg"));

       // Sign and save the document
       signer.SaveDocument("SignedDocument.pdf", cooperSignature);
     }
          Process.Start("SignedDocument.pdf");
   }
 }
}
