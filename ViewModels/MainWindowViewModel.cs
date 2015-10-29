using System;

using MMVVM.ViewModelBase;
using MMVVM.Commands;

namespace MVVMBehaviorExample.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private string _text = String.Empty;
        public string Text 
        { 
            get { return _text; }
            set
            {
                if (value == null) return;
                _text = value;
                _textSaved = false;
                Notify("Text");
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        
        
        private bool _textSaved = true;



        public MainWindowViewModel()
        {
            SaveCommand = new RelayCommand(Save, () => !_textSaved);

            CloseCommand = new RelayCommand(Save, () => _textSaved);
        }

        /* Just a fake save, but code to save text could be placed here*/
        private void Save()
        {
            _textSaved = true;
            SaveCommand.RaiseCanExecuteChanged();
        }
    }
}
