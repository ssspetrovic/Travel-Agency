using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Repository;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class RenovationRequestViewModel: BaseViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private string _seriousness;
        private string _comment;

        public string Seriousness
        {
            get => _seriousness;
            set
            {
                _seriousness = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        public RenovationRequestViewModel()
        {

        }

        public void SubmitRenovation()
        {
            var _repository = new RenovationRepository();
            var renovation = new Renovation();
            //TODO
        }
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
