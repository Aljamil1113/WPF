using Evernote.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Evernote.View
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        LoginVM viewModel;
        public LogInWindow()
        {
            InitializeComponent();

            viewModel = Resources["vm"] as LoginVM;
            viewModel.Authenticated += ViewModel_Autheticated;
        }

        private void ViewModel_Autheticated(object sender, EventArgs e)
        {
            NotesWindow notesWindow = new NotesWindow();
            notesWindow.ShowDialog();
            this.Close();
        }
    }
}
