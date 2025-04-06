using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Prism.Mvvm;
using Calendar.Models;
using Prism.Commands;
using System.Windows;

namespace Calendar.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";

        private DateTime _currentMonth = DateTime.Today;

        public ObservableCollection<CalendarDay> CalendarDays { get; set; }

        public ICommand NextMonthCommand { get; }


        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            CalendarDays = new ObservableCollection<CalendarDay>();
            NextMonthCommand = new DelegateCommand(ShowNextMonth);
            CreateCalendar(_currentMonth);
        }
        private void ShowNextMonth()
        {
            _currentMonth = _currentMonth.AddMonths(1);
            CreateCalendar(_currentMonth);
        }

        private void CreateCalendar(DateTime targetMonth)
        {
            CalendarDays.Clear();

            DateTime firstDay = new DateTime(targetMonth.Year, targetMonth.Month, 1);
            int dayOfWeek = (int)firstDay.DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month);

            // 前の月の空白
            for (int i = 0; i < dayOfWeek; i++)
            {
                CalendarDays.Add(new CalendarDay { DisplayText = "" });
            }

            // 現在の月の日付
            for (int day = 1; day <= daysInMonth; day++)
            {
                int currentDay = day; // キャプチャ用
                CalendarDays.Add(new CalendarDay
                {
                    DisplayText = currentDay.ToString(),
                    ClickCommand = new DelegateCommand(() => OnDateClicked(targetMonth.Year, targetMonth.Month, currentDay))
                });
            }

            // 足りない分の空白を追加して35個にする
            while (CalendarDays.Count < 35)
            {
                CalendarDays.Add(new CalendarDay { DisplayText = "" });
            }
        }

        private void OnDateClicked(int year, int month, int day)
        {
            // クリック時の処理（○をつけるなど）
            MessageBox.Show($"{year}年{month}月{day}日がクリックされました！");
        }


    }
}
