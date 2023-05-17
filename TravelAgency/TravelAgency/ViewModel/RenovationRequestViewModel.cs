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
        private int _accId;

        public int AccId
        {
            get => _accId;
            set
            {
                _accId = value;
                OnPropertyChanged();
            }
        }

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
            var _repository = new RenovationRequestRepository();
            RenovationRequest request = new RenovationRequest(Comment, Seriousness, AccId, 10);
            _repository.Add(request);
        }
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
