# PDF Document API - How to sign PDF documents with a certificate stored on a user's hardware device

The DevExpress [PDF Document API](https://docs.devexpress.com/OfficeFileAPI/16491/pdf-document-api) library allows you to retrieve a certificate from a hardware device (Windows certificate store, SmartCard, USB Token, etc.). This example demonstrates how to use a certificate stored on a user's machine. You can also adapt this solution to sign documents using certificates from any physical store.  

First, you need to obtain a certificate from a Windows certificate store. In this example, the [System.Security.Cryptography.X509Certificates.X509Certificate2UI](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2ui?view=dotnet-plat-ext-6.0) class is used to display a system dialog for viewing and selecting an X.509 certificate.
Create a [Pkcs7Signer](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.Pkcs7Signer) object using the [Pkcs7Signer(X509Certificate2)](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.Pkcs7Signer.-ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)) constructor and pass the retrieved certificate to it. Use this class to sign a PDF document.


## Files to Look At

[Program.cs](./CS/SignPDFWithHardwareCertificate/Program.cs)


## Documentation

- [Sign PDF Documents](https://docs.devexpress.com/OfficeFileAPI/114623/pdf-document-api/document-security/sign-documents).

## More Examples

- [How to Apply Multiple Signatures](https://github.com/DevExpress-Examples/pdf-document-api-multiple-signatures)
- [How to sign a PDF document using Azure Key Vault API](https://github.com/DevExpress-Examples/How-to-sign-a-PDF-document-using-Azure-Key-Vault-API)

