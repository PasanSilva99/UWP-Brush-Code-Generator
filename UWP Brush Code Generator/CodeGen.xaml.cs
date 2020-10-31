using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Brush_Code_Generator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeGen : Page
    {

        Random r = new Random();
        SolidColorBrush solidBrush = new SolidColorBrush();
        AcrylicBrush acrylicBrush = new AcrylicBrush();
        String BrushType, BackSource, Key, langagec = "XAML";
        bool
            colorPickerLoaded = false,
            comboLangLoaded = false,
            comboBrushTypeLoaded = false,
            combobackgroundLoaded = false,
            scrl_tintLoaded = false,
            scrl_lumLoaded = false,
            tb_codeViewLoaded = false;

        public CodeGen()
        {
            this.InitializeComponent();
            //Preset Values
            solidBrush.Color = colorPicker.Color;
            acrylicBrush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
            acrylicBrush.TintColor = colorPicker.Color;
            acrylicBrush.TintOpacity = 0.9;
            acrylicBrush.TintLuminosityOpacity = 0.6;
            acrylicBrush.FallbackColor = colorPicker.Color;
            rect_colorView.Fill = acrylicBrush;

        }

        private void generateColors()
        {
            scrl_Preset.Children.Clear();
            for (int i = 0; i < 100; i++)
            {
                Button btn = new Button();
                btn.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255)));
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Margin = new Thickness(5, 20, 5, 0);
                btn.CornerRadius = new CornerRadius(0, 20, 0, 20);
                btn.Width = 200;
                btn.Height = 40;
                btn.Click += PresetColor_Click;

                scrl_Preset.Children.Add(btn);

            }
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            generateColors();
        }

        private void setColor(Color color)
        {
            solidBrush.Color = color;
            acrylicBrush.TintColor = color;
            acrylicBrush.FallbackColor = color;
            generateCode();
        }

        private void colorPicker_Loaded(object sender, RoutedEventArgs e)
        {
            colorPickerLoaded = true;
        }

        private void combo_brushType_Loaded(object sender, RoutedEventArgs e)
        {
            comboBrushTypeLoaded = true;
        }

        private void combo_backSource_Loaded(object sender, RoutedEventArgs e)
        {
            combobackgroundLoaded = true;
        }

        private void tb_codeView_Loaded(object sender, RoutedEventArgs e)
        {
            tb_codeViewLoaded = true;
        }

        private void combo_Lang_Loaded(object sender, RoutedEventArgs e)
        {
            comboLangLoaded = true;
        }

        private void scrl_tint_Loaded(object sender, RoutedEventArgs e)
        {
            scrl_tintLoaded = true;
        }

        private void scrl_lum_Loaded(object sender, RoutedEventArgs e)
        {
            scrl_lumLoaded = true;
        }

        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(tb_codeView.Text);
            Clipboard.SetContent(dataPackage);
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            generateColors();
        }

        private void generateCode()
        {
            if (colorPickerLoaded &&
                tb_codeViewLoaded &&
                comboBrushTypeLoaded &&
                combobackgroundLoaded &&
                scrl_tintLoaded &&
                scrl_lumLoaded &&
                comboLangLoaded)
            {
                string code;
                string color = "#" + colorPicker.Color.ToString().Substring(3);
                if (BrushType == "AcrylicBrush")
                {
                    if (langagec == "XAML")
                    {
                        code = "<AcrylicBrush \n" +
                            "   BackgroundSource=\"" + BackSource + "\"\n" +
                            "   TintColor=\"" + color + "\"\n" +
                            "   TintOpacity=\"" + scrl_tint.Value.ToString("F1") + "\"\n" +
                            "   TintLuminosityOpacity=\"" + scrl_lum.Value.ToString("F1") + "\"\n" +
                            "   FallbackColor=\"" + color + "\"\n/>";
                    }
                    else
                    {
                        if (tb_key.Text == "")
                        {
                            code =
                            "AcrylicBrush acrylicBrush = new AcrylicBrush();" +
                            "\nacrylicBrush.BackgroundSource = AcrylicBackgroundSource." + BackSource + ";" +
                            "\nacrylicBrush.TintColor = " + " Windows.UI.Color.FromArgb((byte)255, (byte)" + colorPicker.Color.R.ToString() + ", (byte)" + colorPicker.Color.G.ToString() + ",(byte)" + colorPicker.Color.B.ToString() + ")" + ";" +
                            "\nacrylicBrush.TintOpacity = " + scrl_tint.Value.ToString("F1") + ";" +
                            "\nacrylicBrush.TintLuminosityOpacity = " + scrl_lum.Value.ToString("F1") + ";" +
                            "\nacrylicBrush.FallbackColor = " + " Windows.UI.Color.FromArgb((byte)255, (byte)" + colorPicker.Color.R.ToString() + ", (byte)" + colorPicker.Color.G.ToString() + ",(byte)" + colorPicker.Color.B.ToString() + ")" + ";";
                        }
                        else
                        {
                            code =
                              "AcrylicBrush " + tb_key.Text + " = new AcrylicBrush();" +
                            "\n" + tb_key.Text + ".BackgroundSource = AcrylicBackgroundSource." + BackSource + ";" +
                            "\n" + tb_key.Text + ".TintColor = " + " Windows.UI.Color.FromArgb((byte)255, (byte)" + colorPicker.Color.R.ToString() + ", (byte)" + colorPicker.Color.G.ToString() + ",(byte)" + colorPicker.Color.B.ToString() + ")" + ";" +
                            "\n" + tb_key.Text + ".TintOpacity = " + scrl_tint.Value.ToString("F1") + ";" +
                            "\n" + tb_key.Text + ".TintLuminosityOpacity = " + scrl_lum.Value.ToString("F1") + ";" +
                            "\n" + tb_key.Text + ".FallbackColor = " + " Windows.UI.Color.FromArgb((byte)255, (byte)" + colorPicker.Color.R.ToString() + ", (byte)" + colorPicker.Color.G.ToString() + ",(byte)" + colorPicker.Color.B.ToString() + ")" + ";";

                        }
                    }
                }
                else
                {
                    if (langagec == "XAML")
                    {


                        code =
                        "<SolidColorBrush\n   Color=\"" + color + "\"/>";
                    }
                    else
                    {
                        if (tb_key.Text == "")
                        {
                            code =
                                "SolidColorBrush solidBrush = new SolidColorBrush();" +
                                "\nsolidBrush.Color = " + " Windows.UI.Color.FromArgb((byte)255, (byte)" + colorPicker.Color.R.ToString() + ", (byte)" + colorPicker.Color.G.ToString() + ",(byte)" + colorPicker.Color.B.ToString() + ")" + ";";
                        }
                        else
                        {
                            code =
                                "SolidColorBrush " + tb_key.Text + " = new SolidColorBrush();" +
                                "\n" + tb_key.Text + ".Color = " + " Windows.UI.Color.FromArgb((byte)255, (byte)" + colorPicker.Color.R.ToString() + ", (byte)" + colorPicker.Color.G.ToString() + ",(byte)" + colorPicker.Color.B.ToString() + ")" + ";";

                        }
                    }
                }

                tb_codeView.Text = code;

            }
        }
        private void PresetColor_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush btnBack = (SolidColorBrush)(sender as Button).Background;
            colorPicker.Color = btnBack.Color;
            setColor(btnBack.Color);
        }

        private void colorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            setColor(colorPicker.Color);
        }

        private void combo_Lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_Lang.SelectedIndex)
            {
                case 0:
                    langagec = "XAML";
                    break;
                case 1:
                    langagec = "CS";
                    break;
            }
            generateCode();
        }

        private void combo_brushType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_brushType.SelectedIndex)
            {
                case 0:
                    rect_colorView.Fill = acrylicBrush;
                    BrushType = "AcrylicBrush";
                    break;
                case 1:
                    rect_colorView.Fill = solidBrush;
                    BrushType = "SolidColorBrush";
                    break;
            }
            generateCode();
        }

        private void combo_backSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_backSource.SelectedIndex)
            {
                case 0:
                    BackSource = "HostBackdrop";
                    acrylicBrush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                    break;
                case 1:
                    BackSource = "Backdrop";
                    acrylicBrush.BackgroundSource = AcrylicBackgroundSource.Backdrop;
                    break;

            }
            generateCode();
        }

        private void scrl_tint_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            acrylicBrush.TintOpacity = scrl_tint.Value;
            generateCode();
        }

        private void scrl_lum_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            acrylicBrush.TintLuminosityOpacity = scrl_lum.Value;
            generateCode();
        }

        private void tb_key_TextChanged(object sender, TextChangedEventArgs e)
        {
            Key = tb_key.Text;
            generateCode();
        }
    }
}
