using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guide;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class PdfDateChoiceViewModel : HomePageViewModel
    {

        private TourService _tourService;
        private DateTime _startDate;
        private DateTime _endDate;

        public PdfDateChoiceViewModel()
        {
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
            _tourService = new TourService();
            ConfirmDeletion = new MyICommand(ConfirmDeletionCommand);
            CancelDeletion = new MyICommand(CancelDeletionCommand);
        }

        public MyICommand ConfirmDeletion { get; private set; }
        public MyICommand CancelDeletion { get; private set; }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        private void ConfirmDeletionCommand()
        {
            if (StartDate > EndDate)
            {
                MessageBox.Show("Enter a valid data relation!");
            }
            else
            {
                try
                {
                    var document = new Document();

                    var filePath = $"DateChoice_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                    var writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));


                    var documentData = _tourService.CreateDocumentData(StartDate, EndDate);

                    document.Open();
                    document.AddTitle("Tours in the Date Range: " + StartDate.ToString("MM/dd/yyyy") + " - " + EndDate.ToString("MM/dd/yyyy"));
                    document.AddAuthor(CurrentUser.Username);

                    var titleParagraph = new Paragraph("Tours in the Date Range: " + StartDate.ToString("MM/dd/yyyy") + " - " + EndDate.ToString("MM/dd/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 50
                    };
                    document.Add(titleParagraph);

                    if (documentData.Count == 0)
                    {
                        var noToursParagraph = new Paragraph("You are completely free in this date range!", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                        document.Add(noToursParagraph);
                    }
                    else
                        foreach (var data in documentData)
                        {
                            var lines = data.Content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            var lineHeight = data.Font.Size * 1.2f;
                            var requiredHeight = lines.Length * lineHeight;

                            if (writer.GetVerticalPosition(true) > document.TopMargin + requiredHeight)
                            {
                                document.Add(data);
                            }
                            else
                            {
                                document.NewPage();
                                document.Add(data);
                            }
                        }


                    document.Close();
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

                    new WindowManager().CloseWindow<PdfDateChoice>();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("PDF generation failed: " + ex.Message);
                }

            }
        }

        private void CancelDeletionCommand()
        {
            new WindowManager().CloseWindow<PdfDateChoice>();
        }
    }
}
