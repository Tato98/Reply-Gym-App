using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ReplyGym
{
    public class Field : INotifyPropertyChanged
    {
        public string Label { get; set; }

        private string _course;

        public List<String> Options { get; set; }

        public string SelectedOption
        {
            get => _course;
            set
            {
                if (_course != value)
                {
                    _course = value;
                    OnPropertyChanged(nameof(SelectedOption)); // Notifica il cambiamento
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
