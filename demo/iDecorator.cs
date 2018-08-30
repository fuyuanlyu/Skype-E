using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iMessageDemo {
    public class iDecorator : Decorator {
        public iDecorator() {
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
        }

        public bool Direction {
            get { return (bool)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint) {
            Size result = new Size();
            if (Child != null) {
                Child.Measure(constraint);
                result.Width = Child.DesiredSize.Width + padding.Left + padding.Right;
                result.Height = Child.DesiredSize.Height + padding.Top + padding.Bottom;
                if (result.Height < 35) {
                    result.Height = 35;
                    padding.Top = padding.Bottom = (result.Height - Child.DesiredSize.Height) / 2;
                }

            }
            return result;
        }

        protected override Size ArrangeOverride(Size arrangeSize) {
            if (Child != null) {
                Child.Arrange(new Rect(new Point(padding.Left, padding.Top),
                    Child.DesiredSize));
            }
            return arrangeSize;
        }

        protected override void OnRender(DrawingContext dc) {
            if (Child != null) {

                Geometry cg = null;

                Brush brush = null;

                Pen pen = new Pen();

                pen.Brush = new SolidColorBrush(Colors.Black);
                pen.Thickness = 1;

                if (Direction) {
                    //生成小尾巴在右侧的图形和底色
                    cg = CreateGeometryTailAtRight();

                    brush = CreateBrushTailAtRight();
                }
                else {
                    //生成小尾巴在左侧的图形和底色
                    cg = CreateGeometryTailAtLeft();

                    brush = CreateBrushTailAtLeft();
                }

                dc.DrawGeometry(brush, pen, cg);

                //绘制光照效果
                GradientStopCollection gscLight = new GradientStopCollection();
                gscLight.Add(new GradientStop(Color.FromArgb(0xDA, 0xFF, 0xFF, 0xFF), 0));
                gscLight.Add(new GradientStop(Color.FromArgb(0x68, 0xFF, 0xEF, 0xFF), 1));
                Brush lightBrush = new LinearGradientBrush(gscLight, new Point(0, 0), new Point(0, 1));
                dc.DrawRoundedRectangle(lightBrush, null, new Rect(22, 1, this.ActualWidth - 45, 20), 10, 10);

            }
        }

        Geometry CreateGeometryTailAtRight() {
            CombinedGeometry result = new CombinedGeometry();



            Point arcPoint1 = new Point(this.ActualWidth - 9, this.ActualHeight - 18);
            Point arcPoint2 = new Point(this.ActualWidth, this.ActualHeight - 3);
            Point arcPoint3 = new Point(this.ActualWidth - 20, this.ActualHeight - 6);
            ArcSegment as1_2 = new ArcSegment(arcPoint2, new Size(15, 20), 0, false, SweepDirection.Counterclockwise, true);
            ArcSegment as2_3 = new ArcSegment(arcPoint3, new Size(18, 10), 0, false, SweepDirection.Clockwise, true);

            PathFigure pf1 = new PathFigure();
            pf1.IsClosed = false;
            pf1.StartPoint = arcPoint1;
            pf1.Segments.Add(as1_2);
            pf1.Segments.Add(as2_3);

            PathGeometry pg1 = new PathGeometry();
            pg1.Figures.Add(pf1);

            RectangleGeometry rg2 = new RectangleGeometry(new Rect(9, 0, this.ActualWidth - 18, this.ActualHeight), 20, 20);
            result.Geometry1 = pg1;
            result.Geometry2 = rg2;
            result.GeometryCombineMode = GeometryCombineMode.Union;

            return result;
        }

        Geometry CreateGeometryTailAtLeft() {
            CombinedGeometry result = new CombinedGeometry();

            //Point arcPoint1 = new Point(18, this.ActualHeight - 70);
            //Point arcPoint2 = new Point(0, this.ActualHeight - 60);
            //Point arcPoint3 = new Point(15, this.ActualHeight - 6);

            //ArcSegment as1_2 = new ArcSegment(arcPoint2, new Size(15, 20), 0, false, SweepDirection.Counterclockwise, true);
            //ArcSegment as2_3 = new ArcSegment(arcPoint3, new Size(18, 10), 0, false, SweepDirection.Clockwise, true);

            Point arcPoint1 = new Point(9, this.ActualHeight - 18);
            Point arcPoint2 = new Point(0, this.ActualHeight - 3);
            Point arcPoint3 = new Point(20, this.ActualHeight - 6);

            ArcSegment as1_2 = new ArcSegment(arcPoint2, new Size(15, 20), 0, false, SweepDirection.Clockwise, true);
            ArcSegment as2_3 = new ArcSegment(arcPoint3, new Size(18, 10), 0, false, SweepDirection.Counterclockwise, true);

            PathFigure pf = new PathFigure();
            pf.IsClosed = false;
            pf.StartPoint = arcPoint1;
            pf.Segments.Add(as1_2);
            pf.Segments.Add(as2_3);

            PathGeometry g1 = new PathGeometry();
            g1.Figures.Add(pf);


            RectangleGeometry g2 = new RectangleGeometry(new Rect(9, 0, this.ActualWidth - 18, this.ActualHeight), 20, 20);
            result.Geometry1 = g1;
            result.Geometry2 = g2;
            result.GeometryCombineMode = GeometryCombineMode.Union;

            return result;
        }

        Brush CreateBrushTailAtRight() {

            Brush result = null;

            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0x34, 0x9A, 0x33), 0));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0x89, 0xCD, 0x3D), 0.2));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0x98, 0xD6, 0x40), 0.8));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0x9F, 0xDA, 0x41), 0.81));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0xB1, 0xE3, 0x44), 0.97));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0xBE, 0xEE, 0x5C), 0.98));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0xD3, 0xFF, 0x89), 0.99));
            LinearGradientBrush backGroundBrush = new LinearGradientBrush(gsc, new Point(0, 0), new Point(0, 1));
            result = backGroundBrush;

            return result;
        }

        Brush CreateBrushTailAtLeft() {

            Brush result = null;

            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD), 0));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0xDE, 0xDE, 0xDE), 0.7));
            gsc.Add(new GradientStop(Color.FromArgb(0xFF, 0xEF, 0xEF, 0xEF), 0.99));
            LinearGradientBrush backGroundBrush = new LinearGradientBrush(gsc, new Point(0, 0), new Point(0, 1));
            result = backGroundBrush;

            return result;
        }

        public static void OnDirectionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var self = d as iDecorator;
            self.HorizontalAlignment = (bool)e.NewValue ?
                HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        private Thickness padding = new Thickness(25, 6, 25, 6);

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(bool), typeof(iDecorator),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender, OnDirectionPropertyChangedCallback));
    }
}
