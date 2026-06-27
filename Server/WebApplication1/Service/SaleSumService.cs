using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using OfficeOpenXml;

public class SaleSumService
{
    private readonly ISaleSumRepository _repository;
    private readonly ISaleRepository _saleRepository;


    public SaleSumService(ISaleSumRepository repository, ISaleRepository saleRepository)
    {
        _repository = repository;
        _saleRepository = saleRepository;
    }


    public async Task<ResponseBase<List<SaleSum>?>> GetSaleSumListAsync(RequestBase request)
    {
        var storeId = request.StoreId;
        var date = request.Date;
        var startTime = date.ToDateTime(TimeOnly.MinValue);
        var endTime = date.AddDays(1).ToDateTime(TimeOnly.MinValue);

        var sumList = await _repository.GetSalesByConditionsAsync(startTime, endTime, storeId);

        if (sumList != null)
        {
            return ResponseFactory.CreateSuccessResponse<List<SaleSum>?>(sumList, "取得成功");
        } 
        else
        {
            return ResponseFactory.CreateErrorResponse<List<SaleSum>?>(null, "查無相關資料");
        }
    }

    public async Task<ResponseBase> Summary(RequestBase request)
    {
        var date = request.Date;
        var startTime = date.ToDateTime(TimeOnly.MinValue);
        var endTime = date.AddDays(1).ToDateTime(TimeOnly.MinValue);
        var parsedDate = date.ToString("yyyyMMdd");

        var list = await _saleRepository.GetSalesByConditionsAsync(startTime, endTime, request.StoreId);

        if (list == null || list.Count == 0)
        {
            return ResponseFactory.CreateErrorResponse("選取日期/店家無資料");
        }

        decimal total = 0;
        foreach(Sale sale in list)
        {
            var price = sale.Price;
            total += price;
        }

        var saleSum = new SaleSum
        {
            Id = $"{request.StoreId}{parsedDate}",
            Store_Id = request.StoreId,
            Sale_Time = request.Date,
            Price = total,
            Create_Time = DateTime.Now
        };

        var result = await _repository.UpdateSaleAsync(saleSum);
        
        if (result != null)
        {   
            return ResponseFactory.CreateSuccessResponse("彙整成功");
        } 
        else
        {
            return ResponseFactory.CreateErrorResponse("彙整失敗");
        }
    }

    public async Task<byte[]?> ExportExcel(RequestBase request)
    {
        var storeId = request.StoreId;
        var date = request.Date;
        var startTime = date.ToDateTime(TimeOnly.MinValue);
        var endTime = date.AddDays(1).ToDateTime(TimeOnly.MinValue);

        var sumList = await _repository.GetSalesByConditionsAsync(startTime, endTime, storeId);

        if (sumList == null || sumList.Count == 0)
        {
            return null;
        }

        using var package = new ExcelPackage();

        var sheet = package.Workbook.Worksheets.Add("Sales");
        package.Encryption.IsEncrypted = true;
        package.Encryption.Password = "123456";

        sheet.Cells[1, 1].Value = "門市代號";
        sheet.Cells[1, 2].Value = "銷售日期";
        sheet.Cells[1, 3].Value = "銷售金額";
        sheet.Cells[1, 4].Value = "建立時間";

        for (int i = 0; i < sumList.Count; i++)
        {
            var sale = sumList[i];
            sheet.Cells[i + 2, 1].Value = sale.Store_Id;
            sheet.Cells[i + 2, 2].Value = sale.Sale_Time.ToString("yyyy/MM/dd");
            sheet.Cells[i + 2, 3].Value = sale.Price;
            sheet.Cells[i + 2, 4].Value = sale.Create_Time.ToString("yyyy/MM/dd HH:mm:ss");
        }
        sheet.Cells.AutoFitColumns();

        var bytes = package.GetAsByteArray();

        return bytes;
    }

}