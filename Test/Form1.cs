using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Test
{
     public partial class Form1 : Form
     {
        private List<string> _activePorts = new List<string>();
        private int _rightPortNumber = -1;
        private PortChecker _portChecker = new PortChecker();
        private ActivePortsFinder _activePortsFinder = new ActivePortsFinder();

        public Form1()
        {
            InitializeComponent();

            //В целях тестирования
            //_sPort = serialPort1;
            //_sPort.Open();
            

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            CheckPorts();
        }

        private void CheckPorts()
        {
            _rightPortNumber = -1;
            _activePorts = _activePortsFinder.Find();
            //_activePorts = 
            if (_activePorts != null && _activePorts.Count > 0)
            {
                richTextBox1.Visible = false;
                ShowActivePorts(_activePorts);
                for (int i = 0; i < _activePorts.Count; i++)
                {
                    if (_portChecker.Do(_activePorts[i], textBox1.Text))
                    {
                        _rightPortNumber = i;
                    }
                }
            }
            else
            {
                richTextBox1.Visible = true;
            }
        }
        private void ShowActivePorts(List<string> activePorts)
        {
            foreach (string s in activePorts)
            {
                if (s != null)
                {
                    listBox1.Items.Add(s);
                }                           
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            ListBox lb = (ListBox)sender;

            if (e.Index == _rightPortNumber)
            {
                g.FillRectangle(new SolidBrush(Color.Green), e.Bounds);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
            }

            if (lb.Items.Count > 0)
            {
                g.DrawString(lb.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF(e.Bounds.X, e.Bounds.Y));
            }
            
            e.DrawFocusRectangle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            CheckPorts();
            listBox1.Invalidate();
        }
    }
}
