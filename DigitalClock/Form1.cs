using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalClock
{
    public partial class Form1 : Form
    {
        SegmentDisplay SD;
        SegmentDisplay[] hours;
        SegmentDisplay[] minutes;
        SegmentDisplay[] seconds;
        byte[] DateTimeNowOn;

        public Form1()
        {
            InitializeComponent();
            LightOffEveryPanel();
            SD = new SegmentDisplay(0);
            timer1.Start();
        }
        
        public void LightOn(byte flag)
        {
            // A를 뜻하는 첫 번째 비트가 ON되어있다면
            if((flag & 1)!=0)
            {
                textBox0_0.BackColor = Color.Yellow;
            }
            if((flag & 2)!= 0)
            {
                textBox0_1.BackColor = Color.Yellow;
            }
            if ((flag & 4) != 0)
            {
                textBox0_2.BackColor = Color.Yellow;
            }
            if ((flag & 8) != 0)
            {
                textBox0_3.BackColor = Color.Yellow;
            }
            if ((flag & 16) != 0)
            {
                textBox0_4.BackColor = Color.Yellow;
            }
            if ((flag  & 32) != 0)
            {
                textBox0_5.BackColor = Color.Yellow;
            }
            if ((flag & 64) != 0)
            {
                textBox0_6.BackColor = Color.Yellow;
            }
        }
        public void LightOnPanelSegment(byte flag , Panel panel)
        {
            //ControlCollection Boxes = (Form.ControlCollection)panel.Controls;
            //panel.Controls[0].BackColor = Color.Blue;
            if ((flag & 1) != 0)
            {
                panel.Controls[0].BackColor = Color.Blue;
            }
            if ((flag & 2) != 0)
            {
                panel.Controls[1].BackColor = Color.Blue;
            }
            if ((flag & 4) != 0)
            {
                panel.Controls[2].BackColor = Color.Blue;
            }
            if ((flag & 8) != 0)
            {
                panel.Controls[3].BackColor = Color.Blue;
            }
            if ((flag & 16) != 0)
            {
                panel.Controls[4].BackColor = Color.Blue;
            }
            if ((flag & 32) != 0)
            {
                panel.Controls[5].BackColor = Color.Blue;
            }
            if ((flag & 64) != 0)
            {
                panel.Controls[6].BackColor = Color.Blue;
            }

        }
        public void LightOnPanelSegment(byte flag, Panel panel, Color color)
        {
            //ControlCollection Boxes = (Form.ControlCollection)panel.Controls;
            //panel.Controls[0].BackColor = Color.Blue;
            if ((flag & 1) != 0)
            {
                panel.Controls[0].BackColor = color;
            }
            if ((flag & 2) != 0)
            {
                panel.Controls[1].BackColor = color;
            }
            if ((flag & 4) != 0)
            {
                panel.Controls[2].BackColor = color;
            }
            if ((flag & 8) != 0)
            {
                panel.Controls[3].BackColor = color;
            }
            if ((flag & 16) != 0)
            {
                panel.Controls[4].BackColor = color;
            }
            if ((flag & 32) != 0)
            {
                panel.Controls[5].BackColor = color;
            }
            if ((flag & 64) != 0)
            {
                panel.Controls[6].BackColor = color;
            }

        }
        public void LightOffEveryPanel()
        {
            LightOffPanel(panel_Hours1);
            LightOffPanel(panel_Hours2);
            LightOffPanel(panel_Minutes1);
            LightOffPanel(panel_Minutes2);
            LightOffPanel(panel_Seconds1);
            LightOffPanel(panel_Seconds2);
        }
        public void LightOnEveryPanel()
        {
            LightOnPanel(panel_Hours1);
            LightOnPanel(panel_Hours2);
            LightOnPanel(panel_Minutes1);
            LightOnPanel(panel_Minutes2);
            LightOnPanel(panel_Seconds1);
            LightOnPanel(panel_Seconds2);
        }
        private void LightOffPanel(Panel panel)
        {
            foreach (Control contrl in panel.Controls)
            {
                contrl.BackColor = Color.Gray;
            }
        }
        private void LightOnPanel(Panel panel)
        {
            foreach (Control contrl in panel.Controls)
            {
                contrl.BackColor = Color.Yellow;
            }
        }

 
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] s = DateTime.Now.ToString("HH:mm:ss").Split(':');
            hours = SD.convertToDigitDisplay(Convert.ToByte(s[0]));
            minutes = SD.convertToDigitDisplay(Convert.ToByte(s[1]));
            seconds = SD.convertToDigitDisplay(Convert.ToByte(s[2]));

            LightOffEveryPanel();
            LightOnPanelSegment(hours[0].Flag, panel_Hours1,Color.Yellow);
            LightOnPanelSegment(hours[1].Flag, panel_Hours2, Color.Yellow);
            LightOnPanelSegment(minutes[0].Flag, panel_Minutes1, Color.Yellow);
            LightOnPanelSegment(minutes[1].Flag, panel_Minutes2, Color.Yellow);
            LightOnPanelSegment(seconds[0].Flag, panel_Seconds1, Color.Yellow);
            LightOnPanelSegment(seconds[1].Flag, panel_Seconds2, Color.Yellow);


        }
    }

    class Segment_Number
    {

    }


    class SegmentDisplay
    {
        private byte flag = 0;
        // 0000 0000
        // dp ,G,F,E  D,C,B,A 로 하면 되겠다.
        // 일단 dp 없이 만들자.

        public SegmentDisplay(byte Number)
        {
            setFlag(Number);
        }

        public void setFlag(byte Number)
        {
            this.flag = 0;
            switch (Number)
            {
                case 0: // A B C D E F
                    this.flag = 1 + 2 + 4 + 8 + 16 + 32;
                    break;
                case 1: // 숫자 1 -> B와 C
                        // 0000 0110
                    this.flag = 4 + 2;
                    break;
                case 2: // 숫자 2 -> A B D E G
                    this.flag = 1 + 2 + 8 + 16 + 64;
                    break;

                case 3: // A B C D G
                    this.flag = 1 + 2 + 4 + 8 + 64;
                    break;

                case 4: // B C F G
                    this.flag = 2 + 4 + 32 + 64;
                    break;
                case 5: // A C D F G
                    this.flag = 1 + 4 + 8 + 32 + 64;
                    break;
                case 6: // A C D E F G
                    this.flag = 1 + 4 + 8 + 16 + 32 + 64;
                    break;
                case 7: // A B C
                    this.flag = 1 + 2 + 4;
                    break;
                case 8: // A B C D E F G
                    this.flag = 1 + 2 + 4 + 8 + 16 + 32 + 64;
                    break;
                case 9: // A B C F G
                    this.flag = 1 + 2 + 4 + 32 + 64;
                    break;
                default:
                    break;
            }
        }

        // 숫자를 넣으면 두자리의 segment 배열에 setFlag 해주는 함수.
        public SegmentDisplay[] convertToDigitDisplay(byte Number)
        {
            SegmentDisplay[] displays = new SegmentDisplay[2];

            byte Number_10 = (byte)(Number / 10); // 10의 자리의 수
            byte Number_1 = (byte)(Number % 10); // 1의자리의 수

            displays[0] = new SegmentDisplay(Number_10);
            displays[1] = new SegmentDisplay(Number_1);

            return displays;
        }

        //public byte Flag => flag;
   
        public byte Flag
        {
            get { return this.flag; }
        }
    }


}
