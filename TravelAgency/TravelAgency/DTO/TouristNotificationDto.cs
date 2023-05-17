using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelAgency.Model;

namespace TravelAgency.Dto
{
    internal sealed class TouristNotificationDto : TouristNotification, INotifyPropertyChanged
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public TouristNotificationDto(int id, string touristUsername, string notificationText, string tourName, NotificationStatus status, NotificationType type) : 
            base(id, touristUsername, notificationText, tourName, status, type)
        {
            _isChecked = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
