using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

namespace Kulikova.UI
{
    [Binding]
    public class CanvasViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string PropertyArgsName = "Health";
        
        private string _health = "Health: ";
        
        [Binding] 
        public string Health
        {
            get => _health;
            set
            {
                if (_health == value) return;

                _health = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyArgsName));
            }
        }
    }
}