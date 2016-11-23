using System.Drawing;

namespace SCB.OrderSorting.BLL.Model
{
    public struct ButtonPointModel
    {
        public int LatticesettingId { get; set; }

        public Point ButtonLocation { get; set; }

        public Size ButtonSize { get; set; }

        public Font ButtonFont { get; set; }
    }
}
