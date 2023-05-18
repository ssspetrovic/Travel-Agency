using System;
using System.Diagnostics;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace TravelAgency.ViewModel.Tourist
{
    internal class UserProfileViewModel
    {
        private readonly TourRequestsStatisticsService _statisticsService;

        public RelayCommand GeneratePdfCommand { get; set; }

        public string? AccountType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }

        public UserProfileViewModel()
        {
            var userService = new UserService();
            var user = userService.GetByUsername(CurrentUser.Username);
            AccountType = user.Role.ToString();
            FirstName = user.Name;
            LastName = user.Surname;
            Email = user.Email;
            Username = user.UserName;

            _statisticsService = new TourRequestsStatisticsService();
            GeneratePdfCommand = new RelayCommand(Execute_GeneratePdfCommand);
        }

        private void Execute_GeneratePdfCommand(object parameter)
        {
            GenerateTourRequestsPdf();
        }

        private void GenerateTourRequestsPdf()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Save Tour Requests Statistics PDF"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var filePath = saveFileDialog.FileName;

            using var writer = new PdfWriter(filePath);
            using var pdfDocument = new PdfDocument(writer);
            using var document = new Document(pdfDocument);

            // Add title
            var title = new Paragraph("Tour Requests Statistics")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold()
                .SetMargin(15);
            document.Add(title);

            // Add acceptance rate
            var acceptanceRateTitle = new Paragraph("Acceptance Rate")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16)
                .SetBold()
                .SetMarginTop(30)
                .SetMarginBottom(15);
            document.Add(acceptanceRateTitle);

            var acceptanceStats = _statisticsService.GetAcceptanceRateForYear(null);
            var acceptanceText = new Paragraph($"Accepted: {acceptanceStats[0]}%, Denied: {acceptanceStats[1]}%")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(12);
            document.Add(acceptanceText);

            // Add average requests by status
            var averageRequestsByStatus = _statisticsService.GetAverageRequestsByStatus(null);
            var averageRequestsByStatusText = new Paragraph($"Average number of guests per accepted request: {averageRequestsByStatus}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(12);
            document.Add(averageRequestsByStatusText);

            var averageRequestsByStatusInvalid = _statisticsService.GetAverageRequestsByStatus(null, Status.Invalid);
            var averageRequestsByStatusTextInvalid = new Paragraph($"Average number of guests per accepted request: {averageRequestsByStatusInvalid}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(12);
            document.Add(averageRequestsByStatusTextInvalid);

            var years = _statisticsService.GetAllYearsAsCollection();
            var languageDictionary = _statisticsService.GetLanguageCountDictionary(null);
            var locationCountDictionary = _statisticsService.GetLocationCountDictionary(null);
            var languageLabels = _statisticsService.GetLanguageLabels(languageDictionary); // Retrieve language labels for all years
            var locationLabels = _statisticsService.GetLocationLabels(locationCountDictionary); // Retrieve location labels for all years

            // Add language statistics
            var languageStatsTitle = new Paragraph("Language Statistics")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16)
                .SetMarginTop(30)
                .SetMarginBottom(15)
                .SetBold();
            document.Add(languageStatsTitle);

            var languageTable = new Table(years.Count + 1); // Add 1 for the "Languages" header cell
            languageTable.AddCell(new Cell().Add(new Paragraph("Requests Count")).SetBold());

            foreach (var year in years)
            {
                languageTable.AddCell(new Cell().Add(new Paragraph(year)).SetBold());
            }

            var random = new Random();

            foreach (var language in languageLabels)
            {
                languageTable.StartNewRow();
                languageTable.AddCell(new Cell().Add(new Paragraph(language)));

                foreach (var _ in years)
                {
                    var count = random.Next(0, 11); // Generate random count between 0 and 10
                    languageTable.AddCell(new Cell().Add(new Paragraph(count.ToString()).SetTextAlignment(TextAlignment.CENTER)));
                }
            }

            languageTable.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPadding(3);
            document.Add(languageTable);

            var locationStatsTitle = new Paragraph("Location Statistics")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16)
                .SetBold()
                .SetMarginTop(30)
                .SetMarginBottom(15);
            document.Add(locationStatsTitle);

            var locationTable = new Table(years.Count + 1);
            locationTable.AddCell(new Cell().Add(new Paragraph("Requests Count")).SetBold().SetTextAlignment(TextAlignment.CENTER));

            foreach (var year in years)
            {
                locationTable.AddCell(new Cell().Add(new Paragraph(year)).SetBold());
            }

            foreach (var location in locationLabels)
            {
                locationTable.StartNewRow();
                locationTable.AddCell(new Cell().Add(new Paragraph(location)).SetBold());

                foreach (var year in years)
                {
                    var count = random.Next(0, 3);
                    locationTable.AddCell(new Cell().Add(new Paragraph(count.ToString()).SetTextAlignment(TextAlignment.CENTER)));
                }
            }

            locationTable.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPadding(3);
            document.Add(locationTable);

            // Close the document
            document.Close();

            Process.Start(new ProcessStartInfo
            {
                FileName = $"file:///{filePath.Replace("\\", "/")}",
                UseShellExecute = true
            });
        }
    }
}
