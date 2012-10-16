using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TickSell
{
    public class ButtonSize
    {
        public ButtonSize()
        {
            width = 75;
            height = 23;
            margin = new Padding(3, 3, 3, 3);
        }


        public void putintoButton(Button btn)
        {
            btn.Width = this.width;
            btn.Height = this.height;
            btn.Margin = this.margin;
        }

        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }


        private Padding margin;

        public Padding Margin
        {
            get { return margin; }
            set { margin = value; }
        }


    }
}
