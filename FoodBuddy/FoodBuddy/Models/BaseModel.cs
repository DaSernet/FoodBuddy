using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace FoodBuddy.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        string tags = string.Empty;
        public string Tags
        {
            get { return tags; }
            set { SetProperty(ref tags, value); }
        }

        protected bool SetProperty<K>(ref K backingStore, K value,
            [CallerMemberName] string propertyName = null,
            Action onChanged = null)
        {
            if (EqualityComparer<K>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
