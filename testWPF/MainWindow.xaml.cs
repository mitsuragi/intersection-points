using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Lab1;
using Lab1File;

namespace testWPF
{
    enum Points
    {
        NoPoints = 0,
        OnePoint,
        TwoPoints,
        PointsTotal = 7
    }

    public partial class MainWindow : Window
    {
        private List<Point2D> points = new List<Point2D>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Задание выполнил студент группы №424 Губкин Максим.\n" +
                "Вариант №4\n\n" +
                "Текст задания: Для заданной окружности и луча в плоскости определить, пересекает ли луч окружность. " +
                "Найти координаты точек пересечения.";

            string caption = "Справка";

            MessageBoxImage icon = MessageBoxImage.Information;

            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon);
        }

        private void uploadFileItem_Click(object sender, RoutedEventArgs e)
        {
            List<string> values;

            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Текстовые документы (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                values = FileLogic.FileLoad(filename);
            }
            else
            {
                return;
            }

            if (values.Count != (int)Points.PointsTotal)
            {
                MessageBox.Show("Неверное количество данных в файле", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TextBox[] textBoxes = {BeamX0InputBox, BeamY0InputBox,
                                BeamX1InputBox, BeamY1InputBox,
                                CircleX0InputBox, CircleY0InputBox, CircleRadiusInputBox};

            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].Text = values[i];
            }
        }

        private void initialSaveItem_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = {BeamX0InputBox, BeamY0InputBox,
                                BeamX1InputBox, BeamY1InputBox,
                                CircleX0InputBox, CircleY0InputBox, CircleRadiusInputBox};

            foreach (TextBox box in textBoxes)
            {
                if (string.IsNullOrEmpty(box.Text))
                {
                    MessageBox.Show("Данных нет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            List<Point2D> points = new List<Point2D>();

            for (int i = 0; i < textBoxes.Length - 1; i += 2)
            {
                Point2D pt = new Point2D();
                try
                {
                    pt.Set(textBoxes[i].Text, textBoxes[i + 1].Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Тип введенных данных неверный", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                points.Add(pt);
            }

            double rad;
            try
            {
                rad = Convert.ToDouble(CircleRadiusInputBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Тип введенных данных неверный", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Исходные данные";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Текстовые документы (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                FileLogic.FileSaveInitial(filename, points, rad);
            }
        }

        private void resultSaveItem_Click(object sender, RoutedEventArgs e)
        {
            if (points.Count == 0)
            {
                MessageBox.Show("Данных нет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else
            {
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.FileName = "Результаты";
                dialog.DefaultExt = ".txt";
                dialog.Filter = "Текстовые документы (.txt)|*.txt";

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    string filename = dialog.FileName;
                    FileLogic.FileSaveResult(filename, points);
                }
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            TextOutputBlock.Text = string.Empty;
            Point2D beamPoint1 = new Point2D { X = 0, Y = 0 };
            Point2D beamPoint2 = new Point2D { X = 0, Y = 0 };
            Point2D circlePoint = new Point2D { X = 0, Y = 0 };
            double rad = 0.0;

            int pointsQuantity = 0;

            try
            {
                beamPoint1.Set(BeamX0InputBox.Text, BeamY0InputBox.Text);
                beamPoint2.Set(BeamX1InputBox.Text, BeamY1InputBox.Text);
                circlePoint.Set(CircleX0InputBox.Text, CircleY0InputBox.Text);
                rad = Convert.ToDouble(CircleRadiusInputBox.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("Тип введенных данных неверный", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            points = Logic.GetIntersectionPoints(beamPoint1 , beamPoint2, circlePoint, rad);
            pointsQuantity = points.Count;
            switch(pointsQuantity)
            {
                case (int)Points.NoPoints:

                    TextOutputBlock.Inlines.Add("Точек пересечения нет.");

                    break;
                case (int)Points.OnePoint:

                    TextOutputBlock.Inlines.Add("Найдена одна точка пересечения.\n");
                    TextOutputBlock.Inlines.Add("Её координаты: ");
                    TextOutputBlock.Inlines.Add($"[{points[0].X:f3} ; {points[0].Y:f3}]\n");

                    break;
                case (int)Points.TwoPoints:

                    TextOutputBlock.Inlines.Add("Найдены две точки пересечения.\n");

                    for(int i = 0; i < pointsQuantity; i++)
                    {
                        TextOutputBlock.Inlines.Add($"Координаты {i+1}-й точки пересечения: ");
                        TextOutputBlock.Inlines.Add($"[{points[i].X:f3} ; {points[i].Y:f3}]\n");
                    }
                    
                    break;
            }
        }
    }
}
