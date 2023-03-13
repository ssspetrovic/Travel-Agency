
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ModelView
{
    public class GuideModelView : BaseModelView
    {
        private UserModel _currentUserAccount;
        private BaseModelView _currentChildView;
        private string _caption;

        private IUserRepository userRepository;

        public UserModel CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set
            {
                _currentUserAccount = value; 
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public BaseModelView CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public ICommand ShowGuideViewCommand { get; }
        public ICommand ShowHomeViewCommand { get; }

        public GuideModelView()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserModel();
        }
    }
}
