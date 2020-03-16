using System.Windows.Forms;
using BergerMT;

namespace BergerMT_UI
{
    public partial class GlobalDisplay : Form
    {
        public GlobalDisplay()
        {
            InitializeComponent();
        }

        public void MvBerger()
        {
            this.MvBerger(Direction.Unknwon);
        }
        public void MvBerger(Direction direction)
        {
            this.display1.MvBerger(direction);
        }
    }
}
