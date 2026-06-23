using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using OfficeOpenXml;

public class SaleSumService
{
    private readonly ISaleSumRepository _repository;


    public SaleSumService(ISaleSumRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseBase<List<SaleSum>?>> GetSaleSumListAsync(SaleSumRequest request)
    {
        var storeId = request.StoreId;
        var startDate = request.StartDate;
        var endDate = request.EndDate;

        var sumList = await _repository.GetSalesByConditionsAsync(startDate, endDate, storeId);

        if (sumList != null)
        {
            return ResponseFactory.CreateSuccessResponse<List<SaleSum>?>(sumList, "取得成功");
        } 
        else
        {
            return ResponseFactory.CreateErrorResponse<List<SaleSum>?>(null, "查無相關資料");
        }
    }

    public async Task<byte[]?> ExportExcel(SaleSumRequest request)
    {
        var storeId = request.StoreId;
        var startDate = request.StartDate;
        var endDate = request.EndDate;

        var sumList = await _repository.GetSalesByConditionsAsync(startDate, endDate, storeId);

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