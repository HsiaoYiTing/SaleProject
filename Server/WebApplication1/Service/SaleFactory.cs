using System.Globalization;

public  class SaleFactory
{
    public static SaleLine ParseByLine(string line)
    {
        // 銷售資料組入範例(big5) 格式:門市代號(6)+銷售時間(14)+銷售金額(3)+商品名稱(12)+數量(3)                                				
        return new SaleLine
        {
            Id = Guid.NewGuid().ToString(),
            Store = line.Substring(0, 6),
            SaleTime = DateTime.ParseExact(line.Substring(6, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
            Price = decimal.Parse(line.Substring(20, 3)),
            ProductName = line.Substring(23, 12).Trim(),
            Qty = int.Parse(line.Substring(35, 3)),
            UpdateBy = "OL"
        };
    }

    public static Sale ParseByRequest(AddSaleRequest request, Store store, Product product)
    {
        return new Sale
        {   
            Id = request.Guid,
            Store = store,
            SaleTime = DateTime.ParseExact(request.SaleTime, "yyyyMMddHHmmssff", CultureInfo.InvariantCulture),
            Price = request.SalePrice,
            Qty = request.Qty,
            Product = product,
            UpdateBy = "OL"
        };

    }
}