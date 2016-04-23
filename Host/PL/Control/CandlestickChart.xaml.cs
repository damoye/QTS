using Host.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Host.PL.Control
{
    public partial class CandlestickChart : UserControl
    {
        private int preIndex = -1;
        private double topPrice;
        private double proportion;
        private double barWidth;
        private List<Candle> currentCandles = new List<Candle>();
        private static readonly Pen GridLineXPen = new Pen(Brushes.Gray, 1) { DashStyle = DashStyles.Dash };

        public CandlestickChart()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<Candle> Candles
        {
            get { return (ObservableCollection<Candle>)GetValue(CandlesProperty); }
            set { SetValue(CandlesProperty, value); }
        }
        public static readonly DependencyProperty CandlesProperty =
            DependencyProperty.Register("Candles", typeof(ObservableCollection<Candle>), typeof(CandlestickChart), new FrameworkPropertyMetadata(null, OnCandlesChanged));

        public int Precision
        {
            get { return (int)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }
        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.Register("Precision", typeof(int), typeof(CandlestickChart), new PropertyMetadata(0));

        private static void OnCandlesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newCandles = e.NewValue as ObservableCollection<Candle>;
            var chart = (CandlestickChart)d;
            chart.ScrollBar.ViewportSize = chart.Candles.Count;
            chart.GenerateAndDraw();
            newCandles.CollectionChanged += chart.CandlesCollectionChanged;
        }

        private void GenerateAndDraw(object obj = null, EventArgs args = null)
        {
            //GenerateCurrentCandles
            this.GenerateCurrentCandles();
            //DrawCandle
            this.DrawCandle();
        }

        private void GenerateCurrentCandles()
        {
            this.ScrollBar.Maximum = this.Candles.Count - this.ScrollBar.ViewportSize;
            int startIndex = (int)this.ScrollBar.Value;
            int endIndex = startIndex + (int)this.ScrollBar.ViewportSize;
            this.currentCandles.Clear();
            for (int i = startIndex; i < endIndex; i++)
            {
                this.currentCandles.Add(this.Candles[i]);
            }
        }

        private void DrawCandle()
        {
            this.barWidth = this.DrawElement.ActualWidth / this.currentCandles.Count;
            this.topPrice = this.currentCandles[0].High;
            double bottomPrice = this.currentCandles[0].Low;
            for (int i = 1; i < this.currentCandles.Count; i++)
            {
                Candle item = this.currentCandles[i];
                if (item.High > topPrice)
                {
                    topPrice = item.High;
                }
                if (item.Low < bottomPrice)
                {
                    bottomPrice = item.Low;
                }
            }
            this.proportion = this.DrawElement.ActualHeight / (topPrice - bottomPrice);
            var lines = new List<Tuple<Pen, Point, Point>>();
            DateTime tradingDay = this.currentCandles[0].TradingDay;
            for (int i = 0; i < this.currentCandles.Count; i++)
            {
                Candle item = this.currentCandles[i];
                double x = (i + 0.5) * this.barWidth;
                Point lowPoint = new Point(x, this.GetY(item.Low));
                Point highPoint = new Point(x, this.GetY(item.High));
                Point openPoint = new Point(x, this.GetY(item.Open));
                Point closePoint = new Point(x, this.GetY(item.Close));
                Brush brush;
                if (item.Open == item.Close)
                {
                    brush = Brushes.Black;
                    ++closePoint.Y;
                }
                else if (item.Open > item.Close)
                {
                    brush = Brushes.Green;
                }
                else
                {
                    brush = Brushes.Red;
                }
                lines.Add(Tuple.Create(new Pen(brush, 1), lowPoint, highPoint));
                lines.Add(Tuple.Create(new Pen(brush, this.barWidth * 0.8), openPoint, closePoint));
                if (tradingDay != item.TradingDay)
                {
                    double startX = i * this.barWidth;
                    lines.Add(Tuple.Create(GridLineXPen, new Point(startX, 0), new Point(startX, this.DrawElement.ActualHeight)));
                    tradingDay = item.TradingDay;
                }
            }
            using (DrawingContext dc = this.DrawElement.RenderOpen())
            {
                foreach (var line in lines)
                {
                    dc.DrawLine(line.Item1, line.Item2, line.Item3);
                }
            }
        }

        private void CandlesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.ScrollBar.Value != this.ScrollBar.Maximum)
            {
                return;
            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                this.currentCandles[this.currentCandles.Count - 1] = this.Candles[this.Candles.Count - 1];
                this.DrawCandle();
            }
            else
            {
                ++this.ScrollBar.Maximum;
                ++this.ScrollBar.Value;
            }
            if (!this.MainGrid.IsMouseOver)
            {
                return;
            }
            Point position = Mouse.GetPosition(this.DrawElement);
            int index = (int)(position.X / this.barWidth);
            if (index == this.currentCandles.Count)
            {
                --index;
            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (index != this.currentCandles.Count - 1)
                {
                    return;
                }
            }
            else
            {
                this.XCursor.Text = this.currentCandles[index].CandleTime.ToString();
            }
            double price = this.topPrice - position.Y / this.proportion;
            this.YCursor.Text = price.ToString("F" + this.Precision);
            this.InfoBlock.Text = this.currentCandles[index].ToString();
        }

        private void ChartMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool delta = e.Delta > 0;
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (delta)
                {
                    this.ScrollBar.ViewportSize = Math.Max(this.ScrollBar.ViewportSize / 1.2, 1);
                }
                else
                {
                    this.ScrollBar.ViewportSize = Math.Min(Math.Ceiling(this.ScrollBar.ViewportSize * 1.2), this.Candles.Count);
                }
                this.GenerateAndDraw();
            }
            else
            {
                if (delta)
                {
                    this.ScrollBar.Value -= this.ScrollBar.ViewportSize * 0.2;
                }
                else
                {
                    this.ScrollBar.Value += this.ScrollBar.ViewportSize * 0.2;
                }
            }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.SetVisibility(Visibility.Visible);
            this.preIndex = -1;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.SetVisibility(Visibility.Hidden);
        }

        private void SetVisibility(Visibility value)
        {
            this.XLine.Visibility = this.YLine.Visibility = this.XCursor.Visibility = this.YCursor.Visibility = this.InfoBlock.Visibility = value;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.currentCandles.Count == 0)
            {
                return;
            }
            Point position = e.GetPosition(this.DrawElement);
            double price = this.topPrice - position.Y / this.proportion;
            this.YLine.Y1 = position.Y;
            this.YCursor.Text = price.ToString("F" + this.Precision);
            this.YCursor.SetValue(Canvas.TopProperty, position.Y);
            int index = (int)(position.X / this.barWidth);
            if (index == this.currentCandles.Count)
            {
                --index;
            }
            if (index == this.preIndex)
            {
                return;
            }
            this.preIndex = index;
            Candle candle = this.currentCandles[index];
            double x = (index + 0.5) * this.barWidth;
            this.XLine.X1 = x;
            this.XCursor.Text = candle.CandleTime.ToString();
            this.XCursor.SetValue(Canvas.LeftProperty, x);
            this.InfoBlock.Text = candle.ToString();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.currentCandles.Count == 0)
            {
                return;
            }
            this.DrawCandle();
        }

        private double GetY(double price)
        {
            return (this.topPrice - price) * this.proportion;
        }
    }
}