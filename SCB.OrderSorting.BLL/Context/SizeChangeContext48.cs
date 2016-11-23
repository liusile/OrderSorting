using System;

namespace SCB.OrderSorting.BLL.Context
{
    internal class SizeChangeContext48 : SizeChangeContext
    {
        internal override void BeforeAdd(int formWidth, int formHeight)
        {
            _columnCount = 1;
            _rowCount = 1;
            _groupCount = 1;
            _startX1 = Convert.ToInt32(formWidth * 0.025);//起始位置的坐标的x的值
            _startX2 = Convert.ToInt32(formWidth * 0.5);
            _x = _startX1;
            _startY1 = 50 + Convert.ToInt32(formHeight * 0.04);//起始位置的坐标的y的值
            _startY2 = 50 + Convert.ToInt32(formHeight * 0.45);
            _y = _startY1;
            _btnWidth = Convert.ToInt32(formWidth * 0.115);
            _btnHeight = Convert.ToInt32(formHeight * 0.13);
            _emSize = (float)((formWidth + formHeight) * 0.005);
        }

        internal override void AfterAdd()
        {
            _x += _btnWidth;//每装载下一个button使其x坐标增加145
            _columnCount += 1;
            if (_columnCount > 4)//每4格换行
            {
                _columnCount = 1;
                _rowCount += 1;
                _x = _groupCount % 2 == 0 ? _startX2 : _startX1;
                _y += _btnHeight;
                if (_rowCount > 3)//每3行一组
                {
                    _rowCount = 1;
                    _groupCount += 1;
                    _x = _groupCount % 2 == 0 ? _startX2 : _startX1;
                    _y = _groupCount > 2 ? _startY2 : _startY1;
                }
            }
        }
    }
}
