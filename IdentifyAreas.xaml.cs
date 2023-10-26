using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Dewey_Decimal_Training_App
{
    public partial class IdentifyAreas : Page
    {
        public static int loopcount = 4;
        public static bool callbol = true;
        public static int scoreObtained;
        public static int highscore;
        Random random = new Random();
        IDictionary<string, string> questions = new Dictionary<string, string>();
        IDictionary<string, string> oldQuestions = new Dictionary<string, string>();

        public IdentifyAreas()
        {
            InitializeComponent();
            load();
            populate();
        }

        private void load()
        {
            questions.Clear();
            oldQuestions.Clear();
            questions.Add("000-099", "General Works");
            questions.Add("100-199", "Philosophy and Psychology");
            questions.Add("200-299", "Religion");
            questions.Add("300-399", "Social Sciences");
            questions.Add("400-499", "Language");
            questions.Add("500-599", "Natural Sciences and Mathematics");
            questions.Add("600-699", "Technology");
            questions.Add("700-799", "The Arts");
            questions.Add("800-899", "Literature and Rhetoric");
            questions.Add("900-999", "History, Biography and Geography");
        }

        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
            where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType()).GetTypeInfo().IsSubclassOf(typeof(T)))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }

        private int CalcScore()
        {
            int score = 0;
            List<ListBoxItem> list = new List<ListBoxItem>();
            FindChildren(list, lstAnswers);

            for (int i = 0; i < loopcount; i++)
            {
                string callNumber;
                string description;

                if (!callbol)
                {
                    callNumber = lstQuestions.Items[i].ToString();
                    description = lstAnswers.Items[i].ToString();
                }
                else
                {
                    callNumber = lstAnswers.Items[i].ToString();
                    description = lstQuestions.Items[i].ToString();
                }

                if (oldQuestions[callNumber] == description)
                {
                    list[i].Background = new SolidColorBrush(Colors.Green);
                    score++;
                }
                else
                {
                    list[i].Background = new SolidColorBrush(Colors.Red);
                }
            }

            for (int i = loopcount; i < lstAnswers.Items.Count; i++)
            {
                list[i].Background = new SolidColorBrush(Colors.PaleVioletRed);
            }
            return score;
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            int score = CalcScore();
            btnUpArrow.IsEnabled = false;
            btnDownArrow.IsEnabled = false;

            load();
            scoreObtained = scoreObtained + score;
            highscore = highscore + loopcount;

            PointsLabel.Content = " Score: " + scoreObtained + "/" + highscore;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (btnCheckAnswer.IsEnabled == true)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to start a new game?",
                    "This will reset your score", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    scoreObtained = 0;
                    highscore = 0;
                    PointsLabel.Content = "Begin New Game - Match columns";
                }
                else
                {
                    return;
                }
            }

            load();
            populate();
            btnUpArrow.IsEnabled = true;
            btnDownArrow.IsEnabled = true;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

        private void UpArrow(object sender, RoutedEventArgs e)
        {
            ListControls.SwapIndexes(-1, lstAnswers);
        }

        private void DownArrow(object sender, RoutedEventArgs e)
        {
            ListControls.SwapIndexes(1, lstAnswers);
        }

        private void getKVP(out string call, out string desc)
        {
            KeyValuePair<string, string> kvp;
            int index = random.Next(questions.Count());
            kvp = questions.ElementAt(index);
            oldQuestions.Add(kvp.Key, kvp.Value);
            questions.Remove(kvp.Key);
            call = kvp.Key;
            desc = kvp.Value;
        }

        private void populate()
        {
            lstQuestions.Items.Clear();
            lstAnswers.Items.Clear();

            if (callbol)
            {
                for (int i = 0; i < 4; i++)
                {
                    getKVP(out string callNo, out string desc);
                    lstQuestions.Items.Add(callNo);
                    lstAnswers.Items.Add(desc);
                }

                for (int i = 0; i < 3; i++)
                {
                    getKVP(out _, out string desc);
                    lstAnswers.Items.Add(desc);
                }
                callbol = false;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    getKVP(out string callNo, out string desc);
                    lstQuestions.Items.Add(desc);
                    lstAnswers.Items.Add(callNo);
                }

                for (int i = 0; i < 3; i++)
                {
                    getKVP(out string callNo, out _);
                    lstAnswers.Items.Add(callNo);
                }
                callbol = true;
            }

            ListControls.randomizeList(lstQuestions);
            ListControls.randomizeList(lstAnswers);
        }
    }
}
