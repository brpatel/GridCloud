using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Dashboard
{
    class LineControl : System.Windows.Forms.Panel
    {
        private String id;
        private float x;
        private float y;
        private Color LineColor = Constants.NORMAL_LINE_COLOR;

        public LineControl(String id, float p1, float p2, float p3, float p4)
        {
            this.id = id;
            this.x = Math.Abs(p1 - p3);
            this.y = Math.Abs(p2 - p4);
            this.BackColor = Color.Yellow;
            this.Paint += LineControl_Paint;
            this.Size = x > y ? new Size((int)x, 2) : new Size(2, (int)y); //new Size(804, 498); 
            
        }

        void LineControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Pen pen = new Pen(LineColor, 4.0f);
            e.Graphics.DrawLine(pen, 0, 0, x, y); 

        }

        public void UpdateLine(Color ColorIndicator)
        {
            this.LineColor = ColorIndicator;
            this.Invalidate();

        }



    }
}
