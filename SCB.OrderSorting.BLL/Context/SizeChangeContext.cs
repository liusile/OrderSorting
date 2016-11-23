namespace SCB.OrderSorting.BLL.Context
{
    internal abstract class SizeChangeContext
    {
        protected int _columnCount;
        protected int _rowCount;
        protected int _groupCount;
        protected int _startX1;
        protected int _startX2;
        protected int _x;
        protected int _startY1;
        protected int _startY2;
        protected int _y;
        protected int _btnWidth;
        protected int _btnHeight;
        protected float _emSize;

        internal int X { get { return _x; } }
        internal int Y { get { return _y; } }
        internal int BtnWidth { get { return _btnWidth; } }
        internal int BtnHeight { get { return _btnHeight; } }
        internal float EmSize { get { return _emSize; } }
        internal abstract void BeforeAdd(int formWidth, int formHeight);
        internal abstract void AfterAdd();
    }
}
