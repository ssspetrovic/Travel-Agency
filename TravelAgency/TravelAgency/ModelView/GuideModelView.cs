
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ModelView
{
    public class GuideModelView : BaseModelView
    {
        private CurrentUserModel _currentUserAccount = null!;
        private BaseModelView _currentChildView = null!;
        private string _caption = null!;

        private readonly IUserRepository _userRepository;

        public CurrentUserModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value; 
                OnPropertyChanged();
            }
        }

        public BaseModelView CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged();
            }
        }

        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowGuideViewCommand { get; }

        private void LoadCurrentUserData()
        {
            //var user = _userRepository.GetByUsername(Environment.UserName);
            //if ( != null)
            //{
            //    CurrentUserAccount.Username = user.UserName;
            //    CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.Surname} ;)";
            //}
            CurrentUserAccount.DisplayName = "Invalid user, not logged in";
            
        }

        private void ExecuteShowGuideViewCommand(object obj)
        {
            CurrentChildView = new GuideModelView();
            Caption = "Guide";
        }

        public GuideModelView()
        {
            _userRepository = new UserRepository();
            CurrentUserAccount = new CurrentUserModel();

            ShowGuideViewCommand = new ModelViewCommand(ExecuteShowGuideViewCommand);

            LoadCurrentUserData();
        }

    }
}
