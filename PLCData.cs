using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCInterface
{
    public class PLCDataList
    {
        public List<PLCData> Items { get; set; }

        public void AddItem(ReadData nameR, string address, object value, bool isRead, PLCDataType dataType)
        {
            Items.Add(new PLCData
            {
                ReadDataName = nameR,
                Address = address,
                Value = value,
                IsRead = isRead,
                DataType = dataType
            });
        }
        public void AddItem(WriteData nameW, string address, object value, bool isRead, PLCDataType dataType)
        {
            Items.Add(new PLCData
            {
                WriteDataName = nameW,
                Address = address,
                Value = value,
                IsRead = isRead,
                DataType = dataType
            });
        }
    }

    public class PLCData
    {
        public ReadData ReadDataName { get; set; }
        public WriteData WriteDataName { get; set; }
        public string Address { get; set; }
        public object Value { get; set; }
        public bool IsRead { get; set; }
        public PLCDataType DataType { get; set; }
    }
    public enum PLCDataType { BIT, BYTE, WORD, DWORD, LWORD, }

}
