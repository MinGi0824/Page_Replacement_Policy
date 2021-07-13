using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Memory_Policy_Simulator
{
    public partial class Form1 : Form
    {
        Graphics g;
        PictureBox pbPlaceHolder;
        Bitmap bResultImage;

        public Form1()
        {
            InitializeComponent();
            this.pbPlaceHolder = new PictureBox();
            this.bResultImage = new Bitmap(2048, 2048);
            this.pbPlaceHolder.Size = new Size(2048, 2048);
            g = Graphics.FromImage(this.bResultImage);
            pbPlaceHolder.Image = this.bResultImage;
            this.pImage.Controls.Add(this.pbPlaceHolder);
        }

        private void DrawBase(Core core, int windowSize, int dataLength)
        {
            /* parse window */
            var psudoQueue = new Queue<char>();

            g.Clear(Color.Black);

            for ( int i = 0; i < dataLength; i++ ) // length
            {
                int psudoCursor = core.pageHistory[i].loc;
                char data = core.pageHistory[i].data;
                Page.STATUS status = core.pageHistory[i].status;

                switch ( status )
                {
                    case Page.STATUS.PAGEFAULT:
                        psudoQueue.Enqueue(data);
                        break;
                    case Page.STATUS.MIGRATION:
                        psudoQueue.Dequeue();
                        psudoQueue.Enqueue(data);
                        break;
                }

                for ( int j = 0; j <= windowSize; j++) // height - STEP
                {
                    if (j == 0)
                    {
                        DrawGridText(i, j, data);
                    }
                    else
                    {
                        DrawGrid(i, j);
                    }
                }

                DrawGridHighlight(i, psudoCursor, status);
                int depth = 1;

                foreach ( char t in psudoQueue )
                {
                    DrawGridText(i, depth++, t);
                }
            }
        }
        private void DrawBase(LRU core, int windowSize, int dataLength)
        {
            /* parse window */
            var psudoQueue = new List<char>();

            g.Clear(Color.Black);

            for (int i = 0; i < dataLength; i++) // length
            {
                int psudoCursor = core.pageHistory[i].loc;
                char data = core.pageHistory[i].data;
                int cursor = core.pageHistory[i].loc-1;
                Page.STATUS status = core.pageHistory[i].status;

                switch (status)
                {
                    case Page.STATUS.PAGEFAULT:
                        psudoQueue.Add(data);
                        break;
                    case Page.STATUS.MIGRATION:
                        psudoQueue.RemoveAt(cursor);
                        psudoQueue.Insert(cursor,data);
                        break;
                }

                for (int j = 0; j <= windowSize; j++) // height - STEP
                {
                    if (j == 0)
                    {
                        DrawGridText(i, j, data);
                    }
                    else
                    {
                        DrawGrid(i, j);
                    }
                }

                DrawGridHighlight(i, psudoCursor, status);
                int depth = 1;

                foreach (char t in psudoQueue)
                {
                    DrawGridText(i, depth++, t);
                }
            }
        }
        private void DrawBase(LFU core, int windowSize, int dataLength)
        {
            /* parse window */
            var psudoQueue = new List<char>();

            g.Clear(Color.Black);

            for (int i = 0; i < dataLength; i++) // length
            {
                int psudoCursor = core.pageHistory[i].loc;
                char data = core.pageHistory[i].data;
                int cursor = core.pageHistory[i].loc - 1;
                Page.STATUS status = core.pageHistory[i].status;

                switch (status)
                {
                    case Page.STATUS.PAGEFAULT:
                        psudoQueue.Add(data);
                        break;
                    case Page.STATUS.MIGRATION:
                        psudoQueue.RemoveAt(cursor);
                        psudoQueue.Insert(cursor, data);
                        break;
                }

                for (int j = 0; j <= windowSize; j++) // height - STEP
                {
                    if (j == 0)
                    {
                        DrawGridText(i, j, data);
                    }
                    else
                    {
                        DrawGrid(i, j);
                    }
                }

                DrawGridHighlight(i, psudoCursor, status);
                int depth = 1;

                foreach (char t in psudoQueue)
                {
                    DrawGridText(i, depth++, t);
                }
            }
        }
        private void DrawBase(MFU core, int windowSize, int dataLength)
        {
            /* parse window */
            var psudoQueue = new List<char>();

            g.Clear(Color.Black);

            for (int i = 0; i < dataLength; i++) // length
            {
                int psudoCursor = core.pageHistory[i].loc;
                char data = core.pageHistory[i].data;
                int cursor = core.pageHistory[i].loc - 1;
                Page.STATUS status = core.pageHistory[i].status;

                switch (status)
                {
                    case Page.STATUS.PAGEFAULT:
                        psudoQueue.Add(data);
                        break;
                    case Page.STATUS.MIGRATION:
                        psudoQueue.RemoveAt(cursor);
                        psudoQueue.Insert(cursor, data);
                        break;
                }

                for (int j = 0; j <= windowSize; j++) // height - STEP
                {
                    if (j == 0)
                    {
                        DrawGridText(i, j, data);
                    }
                    else
                    {
                        DrawGrid(i, j);
                    }
                }

                DrawGridHighlight(i, psudoCursor, status);
                int depth = 1;

                foreach (char t in psudoQueue)
                {
                    DrawGridText(i, depth++, t);
                }
            }
        }
        private void DrawBase(Referencebit core, int windowSize, int dataLength)
        {
            /* parse window */
            var psudoQueue = new List<char>();

            g.Clear(Color.Black);

            for (int i = 0; i < dataLength; i++) // length
            {
                int psudoCursor = core.pageHistory[i].loc;
                char data = core.pageHistory[i].data;
                int cursor = core.pageHistory[i].loc - 1;
                Page.STATUS status = core.pageHistory[i].status;

                switch (status)
                {
                    case Page.STATUS.PAGEFAULT:
                        psudoQueue.Add(data);
                        break;
                    case Page.STATUS.MIGRATION:
                        psudoQueue.RemoveAt(cursor);
                        psudoQueue.Insert(cursor, data);
                        break;
                }

                for (int j = 0; j <= windowSize; j++) // height - STEP
                {
                    if (j == 0)
                    {
                        DrawGridText(i, j, data);
                    }
                    else
                    {
                        DrawGrid(i, j);
                    }
                }

                DrawGridHighlight(i, psudoCursor, status);
                int depth = 1;

                foreach (char t in psudoQueue)
                {
                    DrawGridText(i, depth++, t);
                }
            }
        }
        private void DrawGrid(int x, int y)
        {
            int gridSize = 30;
            int gridSpace = 5;
            int gridBaseX = x * gridSize;
            int gridBaseY = y * gridSize;

            g.DrawRectangle(new Pen(Color.White), new Rectangle(
                gridBaseX + (x * gridSpace),
                gridBaseY,
                gridSize,
                gridSize
                ));
        }

        private void DrawGridHighlight(int x, int y, Page.STATUS status)
        {
            int gridSize = 30;
            int gridSpace = 5;
            int gridBaseX = x * gridSize;
            int gridBaseY = y * gridSize;

            SolidBrush highlighter = new SolidBrush(Color.LimeGreen);

            switch (status)
            {
                case Page.STATUS.HIT:
                    break;
                case Page.STATUS.MIGRATION:
                    highlighter.Color = Color.Purple;
                    break;
                case Page.STATUS.PAGEFAULT:
                    highlighter.Color = Color.Red;
                    break;
            }

            g.FillRectangle(highlighter, new Rectangle(
                gridBaseX + (x * gridSpace),
                gridBaseY,
                gridSize,
                gridSize
                ));
        }

        private void DrawGridText(int x, int y, char value)
        {
            int gridSize = 30;
            int gridSpace = 5;
            int gridBaseX = x * gridSize;
            int gridBaseY = y * gridSize;

            g.DrawString(
                value.ToString(), 
                new Font(FontFamily.GenericMonospace, 8), 
                new SolidBrush(Color.White), 
                new PointF(
                    gridBaseX + (x * gridSpace) + gridSize / 3,
                    gridBaseY + gridSize / 4));
        }

        private void btnOperate_Click(object sender, EventArgs e)
        {
            this.tbConsole.Clear();

            if (this.tbQueryString.Text != "" || this.tbWindowSize.Text != "")
            {
                string data = this.tbQueryString.Text;
                int windowSize = int.Parse(this.tbWindowSize.Text);

                //LRU 시간 카운트
                int current = 0;
                int[] time = new int[windowSize];
                for (int i = 0; i < windowSize; i++)
                    time[i] = 0;

                //LFU, MFU 빈도수 카운트
                int[] freq = new int[windowSize];
                for (int i = 0; i < windowSize; i++)
                    freq[i] = 0;

                //Reference bit (false=0,true=1)
                bool[] refbit = new bool[windowSize];
                for (int i = 0; i < windowSize; i++)
                    refbit[i] = false;

                /* initalize */
                var window = new Core(windowSize);
                var window1 = new LRU(windowSize);
                var window2 = new LFU(windowSize);
                var window3= new MFU(windowSize);
                var window4 = new Referencebit(windowSize);

                if (comboBox1.Text == "FIFO")
                {
                    window = new Core(windowSize);
                }
                else if (comboBox1.Text == "LRU")
                {
                    window1 = new LRU(windowSize);
                }
                else if (comboBox1.Text == "LFU")
                {
                    window2 = new LFU(windowSize);
                }
                else if (comboBox1.Text == "MFU")
                {
                    window3 = new MFU(windowSize);
                }
                else if (comboBox1.Text == "Referencebit")
                {
                    window4 = new Referencebit(windowSize);
                }

                foreach ( char element in data )
                {
                    if (comboBox1.Text == "FIFO")
                    {
                        var status = window.Operate(element);
                        this.tbConsole.Text += "DATA " + element + " is " +
                            ((status == Page.STATUS.PAGEFAULT) ? "Page Fault" : status == Page.STATUS.MIGRATION ? "Migrated" : "Hit")
                            + "\r\n";
                    }
                    else if (comboBox1.Text == "LRU")
                    {
                        var status = window1.Operate(element,ref time,ref current);
                        this.tbConsole.Text += "DATA " + element + " is " +
                            ((status == Page.STATUS.PAGEFAULT) ? "Page Fault" : status == Page.STATUS.MIGRATION ? "Migrated" : "Hit")
                            + "\r\n";
                    }
                    else if (comboBox1.Text == "LFU")
                    {
                        var status = window2.Operate(element, ref time, ref current,ref freq);
                        this.tbConsole.Text += "DATA " + element + " is " +
                            ((status == Page.STATUS.PAGEFAULT) ? "Page Fault" : status == Page.STATUS.MIGRATION ? "Migrated" : "Hit")
                            + "\r\n";
                    }
                    else if (comboBox1.Text == "MFU")
                    {
                        var status = window3.Operate(element, ref time, ref current, ref freq);
                        this.tbConsole.Text += "DATA " + element + " is " +
                            ((status == Page.STATUS.PAGEFAULT) ? "Page Fault" : status == Page.STATUS.MIGRATION ? "Migrated" : "Hit")
                            + "\r\n";
                    }
                    else if (comboBox1.Text == "Referencebit")
                    {
                        var status = window4.Operate(element, ref time, ref current, ref refbit);
                        this.tbConsole.Text += "DATA " + element + " is " +
                            ((status == Page.STATUS.PAGEFAULT) ? "Page Fault" : status == Page.STATUS.MIGRATION ? "Migrated" : "Hit")
                            + "\r\n";
                    }

                }

                if (comboBox1.Text == "FIFO")
                    DrawBase(window, windowSize, data.Length);
                else if(comboBox1.Text=="LRU")
                    DrawBase(window1, windowSize, data.Length);
                else if (comboBox1.Text == "LFU")
                    DrawBase(window2, windowSize, data.Length);
                else if (comboBox1.Text == "MFU")
                    DrawBase(window3, windowSize, data.Length);
                else if (comboBox1.Text == "Referencebit")
                    DrawBase(window4, windowSize, data.Length);

                this.pbPlaceHolder.Refresh();

                /* 차트 생성 */
                chart1.Series.Clear();
                Series resultChartContent = chart1.Series.Add("Statics");
                resultChartContent.ChartType = SeriesChartType.Pie;
                resultChartContent.IsVisibleInLegend = true;

                if (comboBox1.Text == "FIFO")
                {
                    resultChartContent.Points.AddXY("Hit", window.hit);
                    resultChartContent.Points.AddXY("Page Fault", window.fault - window.migration);
                    resultChartContent.Points.AddXY("Migrated", window.migration);
                }
                else if (comboBox1.Text == "LRU")
                {
                    resultChartContent.Points.AddXY("Hit", window1.hit);
                    resultChartContent.Points.AddXY("Page Fault", window1.fault - window1.migration);
                    resultChartContent.Points.AddXY("Migrated", window1.migration);
                }
                else if (comboBox1.Text == "LFU")
                {
                    resultChartContent.Points.AddXY("Hit", window2.hit);
                    resultChartContent.Points.AddXY("Page Fault", window2.fault - window2.migration);
                    resultChartContent.Points.AddXY("Migrated", window2.migration);
                }
                else if (comboBox1.Text == "MFU")
                {
                    resultChartContent.Points.AddXY("Hit", window3.hit);
                    resultChartContent.Points.AddXY("Page Fault", window3.fault - window3.migration);
                    resultChartContent.Points.AddXY("Migrated", window3.migration);
                }
                else if (comboBox1.Text == "Referencebit")
                {
                    resultChartContent.Points.AddXY("Hit", window4.hit);
                    resultChartContent.Points.AddXY("Page Fault", window4.fault - window4.migration);
                    resultChartContent.Points.AddXY("Migrated", window4.migration);
                }

                resultChartContent.Points[0].IsValueShownAsLabel = true;
                resultChartContent.Points[1].IsValueShownAsLabel = true;
                resultChartContent.Points[2].IsValueShownAsLabel = true;

                if(comboBox1.Text=="FIFO")
                     this.lbPageFaultRatio.Text = Math.Round(((float)window.fault / (window.fault + window.hit)), 2) * 100 + "%";
                else if (comboBox1.Text == "LRU")
                    this.lbPageFaultRatio.Text = Math.Round(((float)window1.fault / (window1.fault + window1.hit)), 2) * 100 + "%";
                else if (comboBox1.Text == "LFU")
                    this.lbPageFaultRatio.Text = Math.Round(((float)window2.fault / (window2.fault + window2.hit)), 2) * 100 + "%";
                else if (comboBox1.Text == "MFU")
                    this.lbPageFaultRatio.Text = Math.Round(((float)window3.fault / (window3.fault + window3.hit)), 2) * 100 + "%";
                else if (comboBox1.Text == "Referencebit")
                    this.lbPageFaultRatio.Text = Math.Round(((float)window4.fault / (window4.fault + window4.hit)), 2) * 100 + "%";
            }
            else
            {
            }

        }

        private void pbPlaceHolder_Paint(object sender, PaintEventArgs e)
        {
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void tbWindowSize_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbWindowSize_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
        }

        private void btnRand_Click(object sender, EventArgs e)
        {
            Random rd = new Random();

            int count = rd.Next(5, 50);
            StringBuilder sb = new StringBuilder();


            for ( int i = 0; i < count; i++ )
            {
                sb.Append((char)rd.Next(65, 90));
            }

            this.tbQueryString.Text = sb.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bResultImage.Save("./result.jpg");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
