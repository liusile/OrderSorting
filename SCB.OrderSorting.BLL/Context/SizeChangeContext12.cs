using System;

namespace SCB.OrderSorting.BLL.Context
{
    internal class SizeChangeContext12 : SizeChangeContext
    {
        private int _xAdd;
        private int _yAdd;
        internal override void BeforeAdd(int formWidth, int formHeight)
        {
            _columnCount = 1;
            _startX1 = Convert.ToInt32(formWidth * 0.025);//起始位置的坐标的x的值
            _startX2 = Convert.ToInt32(formWidth * 0.5);
            _x = _startX1;
            _startY1 = 50 + Convert.ToInt32(formHeight * 0.05);//起始位置的坐标的y的值
            _y = _startY1;
            _btnWidth = Convert.ToInt32(formWidth * 0.23);
            _btnHeight = Convert.ToInt32(formHeight * 0.26);
            _emSize = (float)((formWidth + formHeight) * 0.0075);
            _xAdd = Convert.ToInt32(_btnWidth * 1.013);
            _yAdd = Convert.ToInt32(_btnHeight * 1.013);
        }

        internal override void AfterAdd()
        {
            _x += _xAdd;//每装载下一个button使其x坐标增加
            _columnCount += 1;
            if (_columnCount > 4)//每4格换行
            {
                _columnCount = 1;
                _x = _startX1;
                _y += _yAdd;
            }
        }
    }
}
