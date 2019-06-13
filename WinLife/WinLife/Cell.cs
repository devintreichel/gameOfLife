using System;
using System.ComponentModel;

namespace WinLife
{
    class Cell : INotifyPropertyChanged
    {
        private bool state;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool State
        {
            get { return state; }
            set
            {
                state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("State"));
            }
        }

        public Cell(bool state)
        {
            this.State = state;
        }

        public static implicit operator int(Cell c)
        {
            return Convert.ToInt32(c.State);
        }

    }
}
