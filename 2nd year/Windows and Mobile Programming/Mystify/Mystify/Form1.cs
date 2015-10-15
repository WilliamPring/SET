using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mystify
{
    public partial class Form1 : Form
    {
        ThreadRepository trdRepo = new ThreadRepository();
        Random rndPoint = new Random();
        private int randomX;
        private int RandomY;
        private int randomX1;
        private int RandomY1;
        public Form1()
        {
            InitializeComponent();
        }

        private void pnScreen_Paint(object sender, PaintEventArgs e)
        {

        }

    

        private void bnPause_Click(object sender, EventArgs e)
        {

        }

        private void bnNewStick_Click(object sender, EventArgs e)
        {
            randomX = rndPoint.Next(0, pnScreen.Width);
            randomX1 = rndPoint.Next(0, pnScreen.Width);
            RandomY = rndPoint.Next(0 , pnScreen.Height );
            RandomY1 = rndPoint.Next(0 , pnScreen.Height );

        }
    }
}
