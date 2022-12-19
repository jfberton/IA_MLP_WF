using Nevron.Chart.Windows;
using Nevron.Chart;
using Nevron.GraphicsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace IA_MLP
{
    public partial class prueba_grafico : Form
    {
        public prueba_grafico()
        {
            InitializeComponent();


        }
        private void prueba_grafico_Load(object sender, EventArgs e)
        {

            // set a chart title
            NLabel title = nChartControl1.Labels.AddHeader("Error");
            title.TextStyle.FontStyle = new NFontStyle("Times New Roman", 12, FontStyle.Italic);
            title.ContentAlignment = ContentAlignment.BottomCenter;
            title.Location = new NPointL(new NLength(50, NRelativeUnit.ParentPercentage), new NLength(2, NRelativeUnit.ParentPercentage));

            // no legend
            nChartControl1.Legends.Clear();

            // configure the chart
            NChart chart = nChartControl1.Charts[0];
            chart.Axis(StandardAxis.Depth).Visible = false;

            // add interlaced stripe to the Y axis
            NScaleStripStyle stripStyle = new NScaleStripStyle(new NColorFillStyle(Color.Beige), null, true, 0, 0, 1, 1);
            stripStyle.SetShowAtWall(ChartWallType.Back, true);
            stripStyle.SetShowAtWall(ChartWallType.Left, true);
            stripStyle.Interlaced = true;
            ((NStandardScaleConfigurator)chart.Axis(StandardAxis.PrimaryY).ScaleConfigurator).StripStyles.Add(stripStyle);

            NLineSeries line = (NLineSeries)chart.Series.Add(SeriesType.Line);
            line.Name = "Line Series";
            line.InflateMargins = true;
            line.DataLabelStyle.Format = "<value>";
            line.MarkerStyle.Visible = true;
            line.MarkerStyle.PointShape = PointShape.Cylinder;
            line.MarkerStyle.Width = new NLength(1.5f, NRelativeUnit.ParentPercentage);
            line.MarkerStyle.Height = new NLength(1.5f, NRelativeUnit.ParentPercentage);
            line.ShadowStyle.Type = ShadowType.GaussianBlur;
            line.ShadowStyle.Offset = new NPointL(3, 3);
            line.ShadowStyle.FadeLength = new NLength(5);
            line.ShadowStyle.Color = Color.FromArgb(55, 0, 0, 0);
            line.Values = new NDataSeriesDouble() { 1, 2, 3, 4, 5, 6, 7, 8 };

            NLineSeries line2 = (NLineSeries)chart.Series.Add(SeriesType.Line);
            line2.Name = "Line 2 Series";
            line2.InflateMargins = true;
            line2.DataLabelStyle.Format = "<value>";
            line2.MarkerStyle.Visible = true;
            line2.MarkerStyle.PointShape = PointShape.Cylinder;
            line2.MarkerStyle.Width = new NLength(1.5f, NRelativeUnit.ParentPercentage);
            line2.MarkerStyle.Height = new NLength(1.5f, NRelativeUnit.ParentPercentage);
            line2.ShadowStyle.Type = ShadowType.GaussianBlur;
            line2.ShadowStyle.Offset = new NPointL(3, 3);
            line2.ShadowStyle.FadeLength = new NLength(5);
            line2.ShadowStyle.Color = Color.FromArgb(85, 0, 0, 0);
            line2.Values.FillRandom(new Random(), 8);


            // apply style sheet
            NStyleSheet styleSheet = NStyleSheet.CreatePredefinedStyleSheet(PredefinedStyleSheet.Fresh);
            styleSheet.Apply(nChartControl1.Document);

        }
    }
}
