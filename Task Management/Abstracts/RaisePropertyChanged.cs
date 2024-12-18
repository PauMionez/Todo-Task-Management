﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_Management.Abstracts
{
    abstract class RaisePropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
