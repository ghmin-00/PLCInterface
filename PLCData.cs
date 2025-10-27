using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCInterface
{
    public enum ValueType { None, BIT, BYTE, WORD, DWORD, LWORD, }

    public interface IPLCData
    {
        string Name { get; }
        string Address { get; }
        short Value { get; }
        ValueType ValueType { get; }
        bool IsRead { get; }
    }

    public class PLCData : IPLCData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public short Value { get; set; }
        public ValueType ValueType { get; set; }
        public bool IsRead { get; set; }

        public PLCData(string name, string address, short value, ValueType valueType, bool isRead)
        {
            this.Name = name;
            this.Address = address;
            this.Value = value;
            this.ValueType = valueType;
            this.IsRead = isRead;
        }

        public PLCData With(string name = null, string address = null, short value = 0, ValueType valueType = ValueType.None, bool isRead = false)
        {
            return new PLCData(
                name ?? this.Name,
                address ?? this.Address,
                value,
                valueType == ValueType.None ? this.ValueType : valueType,
                isRead
                );
        }

        public override string ToString() => $"Name: {Name}, Address: {Address}, Value: {Value}, ValueType: {ValueType}, IsRead: {IsRead}";

        public override bool Equals(object obj)
        {
            return obj is PLCData other &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Value == other.Value &&
                   ValueType == other.ValueType &&
                   IsRead == other.IsRead;
        }

        public override int GetHashCode() => (Name, Address, Value, ValueType, IsRead).GetHashCode();
    }

    /// <summary>
    /// 각 프로그램에서 사용할 PLCDataList 추상 클래스임
    /// 무조건 상속받아서 내용 정의 필요
    /// </summary>
    public abstract class BasePLCDataList
    {
        public static List<PLCData> GetPLCDataList(BasePLCDataList basePLCDataList)
        {
            Type type = basePLCDataList.GetType();
            if (type.BaseType != typeof(BasePLCDataList))
                return null;
            var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            List<PLCData> listPLCData = new List<PLCData>();
            foreach (var field in fields)
            {
                listPLCData.Add((PLCData)field.GetValue(null));
            }
            return listPLCData;
        }
    }
}
