using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TravelAgency.Model;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class SingleForumViewModel: BaseViewModel, INotifyPropertyChanged
    {

        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;
        private Forum _forum;
        public SingleForumViewModel(NavigationService navigationService, Forum forum) {
            _windowManager = new WindowManager();
            _navigationService = navigationService;
            _forum = forum;
        }
    }
}
