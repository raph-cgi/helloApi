using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;                 // -> System.Web.HttpUtility (sur .NET Core: ajoute package System.Web.HttpUtility si besoin)
using PdfSharpCore.Drawing;       // -> PdfSharpCore
using PdfSharpCore.Pdf;
using QRCoder;                    // -> QRCoder

public class QrCodeGeneratorService
{
    public class Options
    {
        public string OutputDir { get; set; } = "QRCode";
        public string PdfFileName { get; set; } = "QRCodes.pdf";
        public int QrPixelsPerModule { get; set; } = 20; // qualité PNG
        public int QrSizeOnPdf { get; set; } = 160;      // taille (pt) dans le PDF
        public int Margin { get; set; } = 40;            // marges de page
        public int FontSize { get; set; } = 8;
        public int MaxLinesUnderQr { get; set; } = 3;    // nombre max de lignes d'URL visibles sous le QR (0 = illimité)
        public int LineSpacing { get; set; } = 24;       // espace vertical après chaque bloc QR+texte
        public int UrlBoxHeight { get; set; } = 60;      // hauteur allouée à l’URL sous le QR
    }

    /// <summary>
    /// Génère les QR codes (PNG) + un PDF (1 QR par ligne).
    /// baseUrl = ex. "https://192.168.1.25:5001/api/v1/TPerson/fromqr"
    /// </summary>
    public void Generate(IEnumerable<TPersonEntity> persons, string baseUrl, Options? opts = null)
    {
        opts ??= new Options();

        // 1) Préparer le dossier (créer/vider)
        PrepareOutputDir(opts.OutputDir);

        // 2) Créer PDF
        var pdf = new PdfDocument();
        var page = pdf.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", opts.FontSize);

        // Dimensions utiles
        double pageWidth = page.Width;
        double pageHeight = page.Height;
        double contentX = opts.Margin;
        double contentW = pageWidth - 2 * opts.Margin;

        // Position courante verticale (un bloc = QR + URL)
        double y = opts.Margin;

        using var generator = new QRCodeGenerator();
        int index = 1;

        foreach (var p in persons)
        {
            // 3) Construire & nettoyer l'URL
            string url = Clean(BuildUrl(baseUrl, p));

            // 4) Générer le QR (bytes PNG)
            using var data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(data);
            var qrBytes = qrCode.GetGraphic(opts.QrPixelsPerModule);

            // 5) Sauver le PNG
            string pngPath = Path.Combine(opts.OutputDir, $"person_{index}.png");
            File.WriteAllBytes(pngPath, qrBytes);

            // 6) Saut de page si pas la place pour tout le bloc
            double blockHeight = opts.QrSizeOnPdf + 5 + opts.UrlBoxHeight + opts.LineSpacing;
            if (y + blockHeight > pageHeight - opts.Margin)
            {
                page = pdf.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                y = opts.Margin;
            }

            // 7) Dessiner le QR **centré**
            using var ms = new MemoryStream(qrBytes);
            var img = XImage.FromStream(() => ms);
            double xQr = contentX + (contentW - opts.QrSizeOnPdf) / 2.0;

            gfx.DrawImage(img, xQr, y, opts.QrSizeOnPdf, opts.QrSizeOnPdf);

            // 8) URL multi-lignes **sur toute la largeur utile**
            string displayUrl = WrapUrlToWidth(gfx, font, url, contentW);
            if (opts.MaxLinesUnderQr > 0)
            {
                var lines = displayUrl.Split('\n');
                if (lines.Length > opts.MaxLinesUnderQr)
                    displayUrl = string.Join("\n", lines.Take(opts.MaxLinesUnderQr)) + "\n…";
            }

            gfx.DrawString(
                displayUrl,
                font,
                XBrushes.Black,
                new XRect(contentX, y + opts.QrSizeOnPdf + 5, contentW, opts.UrlBoxHeight),
                XStringFormats.TopLeft
            );

            // 9) Avancer à la "ligne" suivante
            y += blockHeight;
            index++;
        }

        // 10) Sauver le PDF dans le dossier QRCode
        string pdfPath = Path.Combine(opts.OutputDir, opts.PdfFileName);
        pdf.Save(pdfPath);
        pdf.Close();
        Console.WriteLine($"PDF généré : {pdfPath}");
    }

    private static void PrepareOutputDir(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            return;
        }
        // vider PNG et PDF précédents
        foreach (var file in Directory.GetFiles(dir, "*.png"))
            File.Delete(file);
        foreach (var file in Directory.GetFiles(dir, "*.pdf"))
            File.Delete(file);
    }

    private static string BuildUrl(string baseUrl, TPersonEntity p)
    {
        var sb = new StringBuilder();
        sb.Append(baseUrl);
        sb.Append($"?nom={HttpUtility.UrlEncode(p.Nom)}");
        sb.Append($"&prenom={HttpUtility.UrlEncode(p.Prenom)}");
        sb.Append($"&dateBorn={p.DateBorn:yyyy-MM-dd}");

        if (p.DateDead.HasValue)
            sb.Append($"&dateDead={p.DateDead:yyyy-MM-dd}");

        if (!string.IsNullOrWhiteSpace(p.Nationalite))
            sb.Append($"&nationalite={HttpUtility.UrlEncode(p.Nationalite)}");

        return sb.ToString();
    }

    // Supprime \r \n et espaces parasites (évite les carrés dans le PDF)
    private static string Clean(string s) =>
        (s ?? string.Empty).Replace("\r", "").Replace("\n", "").Trim();

    /// <summary>
    /// Coupe la chaîne en plusieurs lignes en respectant la largeur max (priorité casse sur & ? / = ,).
    /// </summary>
    private static string WrapUrlToWidth(XGraphics gfx, XFont font, string url, double maxWidth)
    {
        var seps = new HashSet<char>(new[] { '&', '?', '/', '=', ',' });
        var lines = new List<string>();
        int start = 0, i = 0, lastBreak = -1;

        while (i < url.Length)
        {
            char c = url[i];
            if (seps.Contains(c)) lastBreak = i;

            string seg = url.Substring(start, i - start + 1);
            var size = gfx.MeasureString(seg, font);

            if (size.Width > maxWidth)
            {
                int cut = (lastBreak > start) ? lastBreak : Math.Max(start, i - 1);
                if (cut <= start) cut = i; // fallback

                lines.Add(url.Substring(start, cut - start + 1).Trim());
                start = cut + 1;
                lastBreak = -1;
                i = start;
                continue;
            }
            i++;
        }

        if (start < url.Length)
            lines.Add(url.Substring(start).Trim());

        return string.Join("\n", lines);
    }
}
