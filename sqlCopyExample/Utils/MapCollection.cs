using System.Data;
using System.Reflection;

namespace sqlCopyExample.Utils;

public static class MapCollection
{
    public static DataTable MapCollectionToDataTable<T>(IEnumerable<T> items)
    {
        var table = new DataTable();
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.CanRead)
                            .ToList();

        // สร้างคอลัมน์ใน DataTable
        foreach (var prop in properties)
        {
            var columnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            var column = table.Columns.Add(prop.Name, columnType);
            if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
            {
                column.AllowDBNull = true;
            }
        }

        // เพิ่มแถวใน DataTable
        foreach (var item in items)
        {
            var row = table.NewRow();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(item);
                row[prop.Name] = value ?? DBNull.Value;
            }
            table.Rows.Add(row);
        }

        return table;
    }
}
