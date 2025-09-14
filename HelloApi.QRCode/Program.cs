using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using QRCoder;

public class TPersonEntity
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public DateTime DateBorn { get; set; }
    public DateTime? DateDead { get; set; }
    public string? Nationalite { get; set; }
}

class Program
{
    static void Main()
    {
        // Exemple de liste (remplace par tes données)
        var persons = new List<TPersonEntity>
        {
            new() { Nom = "raph", Prenom = "raph", DateBorn = new DateTime(2025, 9, 10) },
            new() { Nom = "marie", Prenom = "curie", DaeBorn = new DateTime(1867, 11, 7), DateDead = new DateTime(1934, 7, 4), Nationalite = "FR" },
            new() { Nom = "marc", Prenom = "curie", DaeBorn = new DateTime(1867, 11, 7), DateDead = new DateTime(1934, 7, 4), Nationalite = "FR" },
            new() { Nom = "audric", Prenom = "curie", DaeBorn = new DateTime(1867, 11, 7), DateDead = new DateTime(1934, 7, 4), Nationalite = "FR" },
        };

        string baseUrl = "https://192.168.1.25:5001/api/v1/TPerson/fromqr";

        var svc = new QrCodeGeneratorService();
        svc.Generate(persons, baseUrl, new QrCodeGeneratorService.Options
        {
            OutputDir = "QRCode",
            PdfFileName = "QRCodes.pdf",
            QrPixelsPerModule = 20,
            QrSizeOnPdf = 160, // tu peux ajuster
            //ColsPerPage = 3,
            //RowsPerPage = 4,
            FontSize = 8,
            MaxLinesUnderQr = 3
        });
    }
}