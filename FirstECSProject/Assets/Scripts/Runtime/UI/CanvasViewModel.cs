using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

namespace Kulikova.UI
{
    [Binding]
    public class CanvasViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _health = "Health: ";
        
        [Binding] 
        public string Health
        {
            get => _health;
            set
            {
                if (_health == value) return;

                _health = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Health"));
            }
        }

        private string _score = "Score: 0";
        
        [Binding] 
        public string Score
        {
            get => _score;
            set
            {
                if (_score == value) return;

                _score = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
            }
        }
    }
}