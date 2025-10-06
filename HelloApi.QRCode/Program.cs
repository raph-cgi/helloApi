using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using HelloApi.Entities;
using HelloApi.Models.V1;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using QRCoder;

//public class TPersonEntity
//{
//    public int Id { get; set; }
//    public string Nom { get; set; }
//    public string Prenom { get; set; }
//    public DateTime DateBorn { get; set; }
//    public DateTime? DateDead { get; set; }
//    public string? Nationalite { get; set; }
//}

class Program
{
    static void Main()
    {
        //// Exemple de liste (remplace par tes données)
        //var persons_v1 = new List<HelloApi.Models.V1.TPerson>
        //{
        //    new() { Nom = "leprince", Prenom = "raph", DateBorn = new DateTime(2025, 9, 10) },
        //    new() { Nom = "leprince", Prenom = "pieter", DateBorn = new DateTime(1867, 11, 7) },
        //};


        var persons_v2 = new List<HelloApi.Models.V2.TPerson>
        {
            new() { Nom = "leprince", Prenom = "raph", DateBorn = new DateOnly(2025, 9, 10) },
            new() { Nom = "leprince", Prenom = "pieter", DateBorn = new DateOnly(1867, 11, 7) },
            new() { Nom = "leprince", Prenom = "pierre", DateBorn = new DateOnly(1938, 3, 18), DateDead = new DateOnly(2018,12,25), Nationalite="FR" },
            new() { Nom = "leprince", Prenom = "christiane", DateBorn = new DateOnly(1944, 11,6),  DateDead = new DateOnly(2013,04,21), Nationalite="FR"  },
        };

        string baseUrl = "https://192.168.1.25:5001/api/v2/TPerson/CreateTPersonFromQuery";

        baseUrl = "https://docs.google.com/forms/d/e/1FAIpQLSdLbzAmKd-p5z3SbRD8NQUo7sqSCmHYn-mIX9oJH2EO7ITL4g/viewform?usp=dialog";

        var svc = new QrCodeGeneratorService();
        svc.Generate(persons_v2, baseUrl, new QrCodeGeneratorService.Options
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