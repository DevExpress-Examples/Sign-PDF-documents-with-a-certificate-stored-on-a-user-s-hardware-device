<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/438184414/21.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1052692)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# PDF Document API - How to sign PDF documents with a certificate stored on a user's hardware device

The DevExpress [PDF Document API](https://docs.devexpress.com/OfficeFileAPI/16491/pdf-document-api) library allows you to retrieve a certificate from a hardware device (Windows certificate store, SmartCard, USB Token, etc.). This example demonstrates how to use a certificate stored on a user's machine. You can also adapt this solution to sign documents using certificates from any physical store.  

## Implementation Details

First, you need to obtain a certificate from a Windows certificate store. In this example, the [System.Security.Cryptography.X509Certificates.X509Certificate2UI](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2ui?view=dotnet-plat-ext-6.0) class is used to display a system dialog for viewing and selecting an X.509 certificate.
Create a [Pkcs7Signer](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.Pkcs7Signer) object using the [Pkcs7Signer(X509Certificate2)](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.Pkcs7Signer.-ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)) constructor and pass the retrieved certificate to it. Use this class to sign a PDF document.


## Files to Look At

[Program.cs](./CS/SignPDFWithHardwareCertificate/Program.cs)


## Documentation

- [Sign PDF Documents](https://docs.devexpress.com/OfficeFileAPI/114623/pdf-document-api/document-security/sign-documents)

## More Examples

- [How to Apply Multiple Signatures](https://github.com/DevExpress-Examples/pdf-document-api-multiple-signatures)
- [How to sign a PDF document using Azure Key Vault API](https://github.com/DevExpress-Examples/How-to-sign-a-PDF-document-using-Azure-Key-Vault-API)

