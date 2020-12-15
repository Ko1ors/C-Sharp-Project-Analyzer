using Analyzer_Test.Data;
using Analyzer_Test.Handlers.ProjectHandlers;
using Analyzer_Test.UI.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
 
namespace Analyzer_Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            LoadRecentSolutions();
        }

        private void LoadRecentSolutions()
        {
            var list = ProjectExtension.GetRecentSolutions();
            solutionListView.Items.Clear();
            solutionListView.ItemsSource = list;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HideStartDialog()
        {
            StartDialog.Visibility = Visibility.Hidden;
        }

        private void OpenSolution(String path)
        {
            HideStartDialog();
            var handler = ProjectHandler.SetHandlers();
            var si = new SolutionInfo
            {
                solutionFilePath = path
            };
            var result = handler.Handle(si);
            if (result.Status == "7")
            {
                foreach(var m in result.Metric)
                {
                    var projectUC = new ProjectUC();
                    projectUC.SetProject(m);
                    listView.Items.Add(projectUC);
                }

                listView.Visibility = Visibility.Visible;
                var solutions = new List<SolutionShortInfo>() { si };
                solutions.AddRange(ProjectExtension.GetRecentSolutions());
                solutions = solutions.GroupBy(e => e.solutionFilePath).Select(e => e.First()).ToList();
                ProjectExtension.SaveRecentSolutions(solutions);
            }
        }

        private void OpenSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "solution files (*.sln)|*.sln";
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                OpenSolution(path);   
            }
        }  
        

        private void solutionListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var solution = (sender as ListView).SelectedItem as SolutionShortInfo;
            OpenSolution(solution.solutionFilePath);
        }
    }
}
