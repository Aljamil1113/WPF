using Evernote.Model;
using Evernote.ViewModel.Commands;
using Evernote.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Evernote.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
       

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set 
            { 
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Visibility isVisible;

        public Visibility IsVisible
        {
            get { return isVisible; }
            set 
            { 
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set 
            { 
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }


        public ObservableCollection<Note> Notes { get; set; }

        //Commands
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }
        public DeleteNotebookCommand DeleteNotebookCommand { get; set; }
        public DeleteNoteCommand DeleteNoteCommand { get; set; }

        private Notebook selectedNotebook;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            DeleteNotebookCommand = new DeleteNotebookCommand(this);
            DeleteNoteCommand = new DeleteNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed;

            GetNotebooks();
        }

        public async void CreateNotebook()
        {
            Notebook notebook = new Notebook()
            {
                Name = "New Notebook",
                UserId = App.UserId
                
            };
            await DatabaseHelper.Insert(notebook);

            GetNotebooks();
        }

        public async void CreateNote(string notebookId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Title = $"New Note for {DateTime.Now.ToString()}"
            };
            await DatabaseHelper.Insert(newNote);

            GetNotes();
        }


        public async void GetNotebooks()
        {
           
              var notebooks = (await DatabaseHelper.Read<Notebook>()).Where(n => n.UserId == App.UserId).ToList();

              Notebooks.Clear();

              foreach (var notebook in notebooks)
              {
                 Notebooks.Add(notebook);
              }
        }


        private async void GetNotes()
        {
            if (selectedNotebook != null)
            {
                var notes = (await DatabaseHelper.Read<Note>()).Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();

                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void StartEditing()
        {
            IsVisible = Visibility.Visible;
        }

        public async void StopEditing(Notebook notebook)
        {
            IsVisible = Visibility.Collapsed;
            await DatabaseHelper.Update(notebook);
            GetNotebooks();
        }

        public async void DeleteNotebook(Notebook notebook)
        {
            try
            {
                await DatabaseHelper.Delete(notebook);
                GetNotebooks();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async void DeleteNote(Note note)
        {
            try
            {
                await DatabaseHelper.Delete(note);
                GetNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
