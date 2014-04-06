using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Dashboard
{
    class BusControl : System.Windows.Forms.Panel
    {
        private String busNo, areaNumber, busBaseKiloVoltage, busVoltage, voltagePhaseAngle, busName, busType;

        private float x;
        private float y;

        public BusControl(String id, float p1, float p2, float p3, float p4)
        {
            this.busNo = id;
            this.x = Math.Abs(p1 - p3);
            this.y = Math.Abs(p2 - p4);
            this.BackColor = Color.Yellow;
            this.Paint += BusControl_Paint;
            this.Size = x > y ? new Size((int)x, 2) : new Size(2, (int)y); // new Size(804, 498);

        }

         void BusControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
         {
             Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 4.0f);
             e.Graphics.DrawLine(pen, 0, 0, x, y);
         }

        /***************  GETTERs*********************/
        public String getBusNo()
         {
             return this.busNo;
         }

        public String getAreaNumber()
        {
            return this.areaNumber;
        }

        public String getBusBaseKiloVoltage()
        {
            return this.busBaseKiloVoltage;
        }

        public String getBusVoltage()
        {
            return this.busVoltage;
        }

        public String getVoltagePhaseAngle()
        {
            return this.voltagePhaseAngle;
        }

        public String getBusName()
        {
            return this.busName;
        }

        public String getBusType()
        {
            return this.busType;
        }


        internal void setBusDetails(string busDetailsString)
        {
            String[] busDetailsValues = busDetailsString.Split(Constants.SEPARATOR_DELIMITER.ToCharArray());

            this.busNo = busDetailsValues[0];
            this.busName = busDetailsValues[1];
            this.busBaseKiloVoltage = busDetailsValues[2];
            this.busType = busDetailsValues[3];
            this.areaNumber = busDetailsValues[4];
            this.busVoltage = busDetailsValues[5];
            this.voltagePhaseAngle = busDetailsValues[6];

        }
    }
}
