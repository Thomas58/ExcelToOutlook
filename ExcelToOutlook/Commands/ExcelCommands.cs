using Excel;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ExcelToOutlook.Commands
{
    class ExcelCommands
    {
        public FileInfo OpenFile()
        {
            List<int> list = new List<int>();
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.AddExtension = true;
            Dialog.CheckPathExists = true;
            Dialog.CheckFileExists = true;
            Dialog.Filter = "Excel Files(*.xlsx; *.xls)|*.xlsx;*.xls";
            if (Dialog.ShowDialog() == DialogResult.OK)
                return new FileInfo(Dialog.FileName);
            else
                return null;
        }
    }
}
