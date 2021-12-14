Imports DevExpress.Pdf
Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Security.Cryptography.X509Certificates

Namespace SignPDFWithHardwareCertificate

    Friend Class Program

        Shared Sub Main(ByVal args As String())
            Dim cert As X509Certificate2 = GetCertificate()
            If cert IsNot Nothing Then
                SignPDF(cert)
            Else
                Console.WriteLine("There are no installed certificates on this machine.")
            End If
        End Sub

        Private Shared Function GetCertificate() As X509Certificate2
            'get a certificate from a Windows Store
            'You can adapt this code to read a certificate from SmartCard or USB Token
            ' https://stackoverflow.com/questions/63086592/how-to-enter-pin-for-x509certificate2-certificate-programmatically-when-signing
            Dim store As X509Store = New X509Store(StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)
            Dim selectedCertificates As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(store.Certificates, Nothing, Nothing, X509SelectionFlag.SingleSelection)
            For Each certificate In selectedCertificates
                If certificate.HasPrivateKey Then Return certificate
            Next

            Return Nothing
        End Function

        Private Shared Sub SignPDF(ByVal cert As X509Certificate2)
            Using signer = New PdfDocumentSigner(File.OpenRead("Demo.pdf"))
                Dim pkcs7Signature As Pkcs7Signer = New Pkcs7Signer(cert, DevExpress.Office.DigitalSignatures.HashAlgorithmType.SHA256)
                Dim signatureFieldInfo = New PdfSignatureFieldInfo(1)
                signatureFieldInfo.Name = "SignatureField"
                signatureFieldInfo.SignatureBounds = New PdfRectangle(20, 20, 150, 150)
                Dim cooperSignature = New PdfSignatureBuilder(pkcs7Signature, signatureFieldInfo)
                cooperSignature.SetImageData(File.ReadAllBytes("JaneCooper.jpg"))
                signer.SaveDocument("SignedDocument.pdf", cooperSignature)
            End Using

            Call Process.Start("SignedDocument.pdf")
        End Sub
    End Class
End Namespace
