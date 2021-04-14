using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shapes;

namespace ScreenToGif.Controls.LightWindow
{
    public class LightWindow : Window
    {
        #region Native

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Properties

        public string Caption
        {
            get => _Caption;
            set
            {
                _Caption = value;

                if (GetTemplateChild("CaptionText") is TextBlock text)
                {
                    text.Text = value;
                }
            }
        }
        private string _Caption;

        #endregion

        private HwndSource _HwndSource;

        static LightWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LightWindow), new FrameworkPropertyMetadata(typeof(LightWindow)));
        }

        public LightWindow()
        {
            PreviewMouseMove += OnPreviewMouseMove;
        }

        #region Event

        protected void OnPreviewMouseMove(object sende, RoutedEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        protected void MinimizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        protected void RestoreClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;

                if (sender is Button button)
                {
                    button.Content = "2";
                }
            }
            else
            {
                WindowState = WindowState.Normal;

                if (sender is Button button)
                {
                    button.Content = "1";
                }
            }
        }

        protected void CloseClick(object sender, RoutedEventArgs e) => Close();

        protected void ResizeRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Rectangle rectangle)
            {
                switch (rectangle.Name)
                {
                    case "top":
                    case "bottom":
                        Cursor = Cursors.SizeNS;
                        break;
                    case "left":
                    case "right":
                        Cursor = Cursors.SizeWE;
                        break;
                    case "topLeft":
                    case "bottomRight":
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case "topRight":
                    case "bottomLeft":
                        Cursor = Cursors.SizeNESW;
                        break;
                }
            }
        }

        protected void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Rectangle rectangle)
            {
                switch (rectangle.Name)
                {
                    case "top":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Top);
                        break;
                    case "bottom":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Bottom);
                        break;
                    case "left":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Left);
                        break;
                    case "right":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Right);
                        break;
                    case "topLeft":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.TopLeft);
                        break;
                    case "topRight":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.TopRight);
                        break;
                    case "bottomLeft":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.BottomLeft);
                        break;
                    case "bottomRight":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.BottomRight);
                        break;
                }
            }
        }

        private void MoveRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _HwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("minimizeButton") is Button minimizeButton)
            {
                minimizeButton.Click += MinimizeClick;
            }

            if (GetTemplateChild("restoreButton") is Button restoreButton)
            {
                restoreButton.Click += RestoreClick;
            }

            if (GetTemplateChild("closeButton") is Button closeButton)
            {
                closeButton.Click += CloseClick;
            }

            if (GetTemplateChild("moveRectangle") is Grid moveRectangle)
            {
                moveRectangle.PreviewMouseDown += MoveRectangle_PreviewMouseDown;
            }

            if (GetTemplateChild("resizeGrid") is Grid resizeGrid)
            {
                foreach (UIElement element in resizeGrid.Children)
                {
                    if (element is Rectangle resizeRectangle)
                    {
                        resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                        resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                    }
                }
            }

            base.OnApplyTemplate();
        }

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += OnSourceInitialized;

            base.OnInitialized(e);
        }


        private void ResizeWindow(ResizeDirection direction) => SendMessage(_HwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);

        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }
    }
}