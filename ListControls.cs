using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Linq;

namespace Dewey_Decimal_Training_App
{
    public class ListControls
    {
        public static void SwapIndexes(int change, ListBox listbox)
        {
            if (listbox.SelectedIndex == null || listbox.SelectedIndex < 0)
            {
                return;
            }

            int newIndex = listbox.SelectedIndex + change;

            if (newIndex < 0 || newIndex >= listbox.Items.Count)
            {
                return;
            }

            object selected = listbox.SelectedItem;

            listbox.Items.Remove(selected);

            listbox.Items.Insert(newIndex, selected);

            listbox.SelectedIndex = newIndex;
        }

        public static void randomizeList(ListBox listBox)
        {
            var list = new List<string>();
            Random random = new Random();

            list = listBox.Items.Cast<string>().ToList();

            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n);
                n--;
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            listBox.Items.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                listBox.Items.Add(list[i]);
            }
        }
    }
}
