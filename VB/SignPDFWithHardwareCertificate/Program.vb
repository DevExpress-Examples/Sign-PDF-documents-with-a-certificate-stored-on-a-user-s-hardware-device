Imports DevExpress.Pdf
Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports DevExpress.Office.DigitalSignatures

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
            ' Get a certificate from a Windows Store
            Dim store As X509Store = New X509Store(StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)
            ' Display a dialog box to select a certificate from the Windows Store
            Dim selectedCertificates As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(store.Certificates, Nothing, Nothing, X509SelectionFlag.SingleSelection)
            ' Get the first certificate that has a primary key
            For Each certificate In selectedCertificates
                If certificate.HasPrivateKey Then Return certificate
            Next

            Return Nothing
        End Function

        Private Shared Sub SignPDF(ByVal cert As X509Certificate2)
            Using signer = New PdfDocumentSigner(File.OpenRead("Demo.pdf"))
                ' Create a PKCS#7 signature
                Dim pkcs7Signature As Pkcs7Signer = New Pkcs7Signer(cert, HashAlgorithmType.SHA256)
                ' Create a signature field on the first page
                Dim signatureFieldInfo = New PdfSignatureFieldInfo(1)
                ' Specify the field's name and location
                signatureFieldInfo.Name = "SignatureField"
                signatureFieldInfo.SignatureBounds = New PdfRectangle(20, 20, 150, 150)
                ' Apply a signature to a newly created signature field
                Dim cooperSignature = New PdfSignatureBuilder(pkcs7Signature, signatureFieldInfo)
                cooperSignature.SetImageData(File.ReadAllBytes("JaneCooper.jpg"))
                ' Sign and save the document
                signer.SaveDocument("SignedDocument.pdf", cooperSignature)
            End Using

            Call Process.Start("SignedDocument.pdf")
        End Sub
    End Class
End Namespace
