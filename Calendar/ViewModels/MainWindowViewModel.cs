using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Prism.Mvvm;
using Calendar.Models;
using Prism.Commands;
using System.Windows;


using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;
using jp.co.systembase.NPOI;
using Prism.Services.Dialogs;
using System.Data;
using System.IO;




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

        // DataTableを利用したサンプル
        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("mitsumoriNo", typeof(Decimal));
            ret.Columns.Add("mitsumoriDate", typeof(DateTime));
            ret.Columns.Add("tokuisaki1", typeof(String));
            ret.Columns.Add("tokuisaki2", typeof(String));
            ret.Columns.Add("hinmei", typeof(String));
            ret.Columns.Add("irisu", typeof(Decimal));
            ret.Columns.Add("hakosu", typeof(Decimal));
            ret.Columns.Add("tani", typeof(String));
            ret.Columns.Add("tanka", typeof(Decimal));
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
              "株式会社 岩手商事", "北上支社",
              "ノートパソコン", 1, 10, "台", 70000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
              "株式会社 岩手商事", "北上支社",
              "モニター", 1, 10, "台", 20000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
              "株式会社 岩手商事", "北上支社",
              "プリンタ", 1, 2, "台", 25000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
              "岩手機器販売　株式会社", "北上店",
              "トナーカートリッジ", 2, 2, "本", 5000);
            return ret;
        }

        public MainWindowViewModel()
        {

            //string st = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int st = 123;
            bool a = this.IsValidString(st);
            /*
            CalendarDays = new ObservableCollection<CalendarDay>();
            NextMonthCommand = new DelegateCommand(ShowNextMonth);
            CreateCalendar(_currentMonth);

            // 帳票定義ファイルを読み込みます
            Report report = new Report(Json.Read("report\\example_image.rrpt"));

            // 帳票にデータを渡します
            report.Fill(new ReportDataSource(getDataTable()));

            // ページ分割を行います
            ReportPages pages = report.GetPages();
            using (FileStream fs = new FileStream("output\\example1.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                // バックスラッシュ文字を円マーク文字に変換します
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            */

        }

        public bool IsValidString(object input)
        {
            // inputがstring型で、かつ32文字以内ならtrue
            if (input is string str)
            {
                return str.Length <= 32;
            }

            // それ以外はfalse
            return false;
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
            // 月初の週を求める
            int dayOfWeek = (int)firstDay.DayOfWeek;
            // 月末を求める
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
    class ExampleDto
    {
        public String Tokuisaki1 { get; set; }
        public String Tokuisaki2 { get; set; }
        public int MitsumoriNo { get; set; }
        public DateTime MitsumoriDate { get; set; }
        public String Hinmei { get; set; }
        public int Irisu { get; set; }
        public int Hakosu { get; set; }
        public String Tani { get; set; }
        public int Tanka { get; set; }
    }
}
