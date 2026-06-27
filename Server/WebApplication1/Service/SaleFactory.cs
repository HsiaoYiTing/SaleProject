using System.Globalization;

public  class SaleFactory
{
    public static Sale? ParseByLine(string line, string updateBy)
    {
        // 銷售資料組入範例(big5) 格式:門市代號(6)+銷售時間(14)+銷售金額(3)+商品編號(6)+商品名稱(12)+數量(3)       
        var storeId = line.Substring(0, 6);
        var saleTime = DateTime.ParseExact(line.Substring(6, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        var total = decimal.Parse(line.Substring(20, 3));
        var productId = line.Substring(23, 6).Trim();
        var productName = line.Substring(29, 12).Trim();
        var qty = int.Parse(line.Substring(41, 3));

        // Validate
        if (string.IsNullOrEmpty(storeId)) return null;
        if (saleTime > DateTime.Now) return null;
        if (string.IsNullOrEmpty(productId)) return null;
        if (string.IsNullOrEmpty(productName)) return null;
        if (total < 0) return null;
        if (qty < 0) return null;
        if (string.IsNullOrEmpty(updateBy)) return null;

        return new Sale
        {
            Id = Guid.NewGuid().ToString(),
            Store = new Store(){
                Id = storeId,
                Name = string.Empty
            },
            SaleTime = saleTime,
            Price = total,
            Product = new Product()
            {
                Id = productId,
                Name = productName,
                Price = total / qty
            },
            Qty = qty,
            CreateTime = DateTime.Now,
            UpdateBy = updateBy
        };
    }

    public static Sale? ParseByRequest(AddSaleRequest request, Store store, Product product)
    {
        var saleTime = DateTime.ParseExact(request.SaleTime, "yyyyMMddHHmmssff", CultureInfo.InvariantCulture);

        // Validate
        if (!request.Store.Equals(store.Id)) return null;
        if (!request.ItemId.Equals(product.Id)) return null;
        if (request.Qty < 0) return null;
        if (request.SalePrice < 0) return null;
        if (string.IsNullOrEmpty(request.UpdateBy)) return null;

        return new Sale
        {   
            Id = request.Guid,
            Store = store,
            SaleTime = saleTime,
            Price = request.SalePrice,
            Qty = request.Qty,
            Product = product,
            CreateTime = DateTime.Now,
            UpdateBy = request.UpdateBy
        };

    }
}