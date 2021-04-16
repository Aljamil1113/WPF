using Evernote.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class DeleteNotebookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM ViewModel { get; set; }

        public DeleteNotebookCommand(NotesVM vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var notebook = parameter as Notebook;

            if (notebook != null)
                ViewModel.DeleteNotebook(notebook);
        }
    }
}
