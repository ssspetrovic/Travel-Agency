using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;
using TravelAgency.ViewModel;
using iTextSharp.text;
using System.Diagnostics;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationStatisticsView.xaml
    /// </summary>
    public partial class AccommodationStatisticsView : Page
    {
        AccommodationRepository accommodationRepository = new AccommodationRepository();
        AccommodationService accommodationService = new AccommodationService();
        private readonly AccommodationStatisticsViewModel _viewModel = new();
        public AccommodationStatisticsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
            cmbAccName.Text = "";
        }

        private void StatsByYearListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.StatsByMonthListView.Items.Clear();

            AccommodationDTO accommodation = accommodationRepository.GetByName(cmbAccName.SelectedItem.ToString());
            ObservableCollection<AccommodationStatDTO> statList = accommodationService.GetAccommodationStatByMonth(accommodation.Id, Convert.ToInt32(lblYear.Content));
            foreach (AccommodationStatDTO a in statList)
            {
                this.StatsByMonthListView.Items.Add(a);
            }

            lblMostBusyMonth.Content = accommodationService.GetMostBusyByMonth(Convert.ToInt32(lblYear.Content), accommodation.Id);
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            this.StatsByYearListView.Items.Clear();

            AccommodationDTO accommodation = accommodationRepository.GetByName(cmbAccName.SelectedItem.ToString());
            ObservableCollection<AccommodationStatDTO> statList = accommodationService.GetAccommodationStatByYear(accommodation.Id);
            foreach (AccommodationStatDTO a in statList)
            {
                this.StatsByYearListView.Items.Add(a);
            }

            lblMostBusyYear.Content = accommodationService.GetMostBusyByYear(accommodation.Id);
        }

        private void btnGeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            if (lblYear.Content != null)
            {
                var document = new Document();
                var filePath = $"PDF_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                var writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                AccommodationDTO accommodation = accommodationRepository.GetByName(cmbAccName.SelectedItem.ToString());
                var documentData = accommodationService.CreateDocumentData(accommodation.Id, Convert.ToInt32(lblYear.Content));

                string titl;
                if (CurrentLanguageAndTheme.languageId == 0)
                {
                    titl = "Statistics for accommodation '" + accommodation.Name + "' in year: " + lblYear.Content;
                }
                else
                {
                    titl = "Statistika za smeštaj '" + accommodation.Name + "' u godini: " + lblYear.Content;
                }

                document.Open();
                document.AddTitle(titl);
                document.AddAuthor(CurrentUser.Username);

                var titleParagraph = new Paragraph(titl, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 50
                };
                document.Add(titleParagraph);

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
            }
            else
            {
                MessageBox.Show("You need to select a year..", "Message");
            }
        }
    }
}
