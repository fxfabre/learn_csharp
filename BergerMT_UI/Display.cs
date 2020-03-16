using System.Windows.Forms;
using BergerMT;

namespace BergerMT_UI
{
    public partial class Display : UserControl
    {
        public Display()
        {
            InitializeComponent();
        }

        public void MvBerger(Direction direction)
        {
            this.ImgBerger.Location = new System.Drawing.Point(125, 25);
        }
    }
}
