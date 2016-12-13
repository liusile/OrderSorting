namespace SCB.OrderSorting.Client
{
    using System;
    using System.IO;
    internal class FileTool
    {
        public byte[] Read()
        {
            string path = System.Windows.Forms.Application.StartupPath + @"\App_Data\ISO-STM32.bin";
            if (!File.Exists(path))
            {
                throw new Exception("文件不存在！");
            }
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            }
            catch (IOException ex)
            {
                throw new Exception("读取文件失败：" + ex.ToString());
            }
            finally
            {
                fs?.Close();
            }
        }
    }
}