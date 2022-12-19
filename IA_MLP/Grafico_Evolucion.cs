using Nevron;
using Nevron.Chart;
using Nevron.GraphicsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_MLP
{
    public partial class Grafico_Evolucion : Form
    {
        public Grafico_Evolucion()
        {
            InitializeComponent();

            //            Client Id[0]
            //Product Type[NETChart]
            //Edition Type[Lite]

            NLicense license = new NLicense("073c7566-4c00-2a00-5102-57006a01f915");
            NLicenseManager.Instance.SetLicense(license);
            NLicenseManager.Instance.LockLicense = true;
        }

        public double[] ValoresError { get; set; }

        public void ActualizarGrafico()
        {
            // set a chart title
            NLabel title = nChartControl1.Labels.AddHeader("Evolución error");
            title.TextStyle.FontStyle = new NFontStyle("Times New Roman", 12, FontStyle.Italic);
            title.ContentAlignment = ContentAlignment.BottomCenter;
            title.Location = new NPointL(new NLength(50, NRelativeUnit.ParentPercentage), new NLength(2, NRelativeUnit.ParentPercentage));

            // no legend
            nChartControl1.Legends.Clear();

            // configure the chart
            NChart chart = nChartControl1.Charts[0];
            chart.Axis(StandardAxis.Depth).Visible = false;
            chart.AllowContentZoom = true;
            chart.DockMode = PanelDockMode.Fill;

            // add interlaced stripe to the Y axis
            NScaleStripStyle stripStyle = new NScaleStripStyle(new NColorFillStyle(Color.Beige), null, true, 0, 0, 1, 1);
            stripStyle.SetShowAtWall(ChartWallType.Back, true);
            stripStyle.SetShowAtWall(ChartWallType.Left, true);
            stripStyle.Interlaced = true;
            ((NStandardScaleConfigurator)chart.Axis(StandardAxis.PrimaryY).ScaleConfigurator).StripStyles.Add(stripStyle);

            NLineSeries line = (NLineSeries)chart.Series.Add(SeriesType.Line);
            line.InflateMargins = true;
            line.DataLabelStyle.Visible = false;
            line.ShadowStyle.Type = ShadowType.GaussianBlur;
            line.ShadowStyle.Offset = new NPointL(3, 3);
            line.ShadowStyle.FadeLength = new NLength(5);
            line.ShadowStyle.Color = Color.FromArgb(55, 0, 0, 0);
            line.Values = new NDataSeriesDouble();
            foreach (double error_corrida in ValoresError)
            {
                line.Values.Add(error_corrida);
            }
            line.Values.Name = "Valores de error";

        }
    }
}
