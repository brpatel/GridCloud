using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Dashboard
{
    class Constants
    {

        public static String OPERATION_AFFECTEDLIST = "AFFECTEDLIST";
        public static String OPERATION_BUSDETAILS = "BUSDETAILS";
        public static String SEPARATOR_DELIMITER = "_";
        public static Color NORMAL_LINE_COLOR = Color.FromArgb(255, 0, 0, 0);
        public static Color WARNING_LINE_COLOR = Color.FromArgb(255, 255, 0, 0);
        public static int UPDATE_CIRCUIT_INTERVAL = (15) * (1000);  // 5 seconds

    }
}
