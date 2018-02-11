using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using OfficeLibrary;
using TrendAnalysis.Data;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models;
using TrendAnalysis.Models.MarkSix;


namespace TrendAnalysis.Service.MarkSix
{

    public class MarkSixPurchaseService
    {

        /// <summary>
        ///     获取指定位置的购买记录
        /// </summary>
        /// <returns></returns>
        public List<MarkSixSpecifiedLocationPurchase> SearchSpecifiedLocation(MarkSixSpecifiedLocationPurchaseSearchDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixSpecifiedLocationPurchase>().AsQueryable();

                source = source.WhereDateTime(nameof(MarkSixSpecifiedLocationPurchase.OnCreated), dto.StartDateTime, dto.EndDateTime);

                if (dto.Location > 0)
                {
                    source = source.Where(m => m.Location == dto.Location);
                }
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    source = source.Where(m => m.Times.Contains(dto.Times));
                }
                if (dto.IsGetTotalCount)
                {
                    dto.TotalCount = source.Count();
                }
                return source.OrderBy(m => m.Times).Skip(dto.StartIndex).Take(dto.PageSize).ToList();
            }
        }


        public void SaveSpecifiedLocation(MarkSixSpecifiedLocationPurchase dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Times))
            {
                throw new Exception("错误，期次不能为空");
            }
            if (string.IsNullOrWhiteSpace(dto.PurchaseList))
            {
                throw new Exception("错误，购买清单不能为空");
            }
            if (dto.Location > 7 || dto.Location < 1)
            {
                throw new Exception("错误，购买的指定位置必须为1-7！");
            }
            if (dto.Odds <= 0)
            {
                throw new Exception("错误，赔率不能小于等于0！");
            }
            using (var dao = new TrendDbContext())
            {
                if (dto.Id > 0)
                {
                    var purchase = dao.Set<MarkSixSpecifiedLocationPurchase>().FirstOrDefault(m => m.Id == dto.Id);
                    if (purchase == null)
                    {
                        throw new Exception(string.Format("错误，购买记录不存在！（Id:{0}）", dto.Id));
                    }
                    purchase.Times = dto.Times;
                    purchase.PurchaseList = dto.PurchaseList;
                    purchase.Odds = dto.Odds;
                    purchase.Location = dto.Location;
                    purchase.PurchaseAmount = dto.PurchaseAmount;
                    purchase.OnModified = DateTime.Now;
                }
                else
                {
                    dto.OnCreated = dto.OnModified = DateTime.Now;
                    dao.Set<MarkSixSpecifiedLocationPurchase>().Add(dto);
                }
                dao.SaveChanges();
            }
        }


        public MarkSixSpecifiedLocationPurchase GetSpecifiedLocationPurchaseById(long id)
        {
            using (var dao = new TrendDbContext())
            {
                return dao.Set<MarkSixSpecifiedLocationPurchase>().FirstOrDefault(m => m.Id == id);
            }
        }


        public void RemoveSpecifiedLocation(params long[] ids)
        {
            using (var dao = new TrendDbContext())
            {
                foreach (var id in ids)
                {
                    var record = dao.Set<MarkSixSpecifiedLocationPurchase>().FirstOrDefault(m => m.Id == id);
                    if (record == null)
                        throw new Exception(string.Format("错误，购买记录不存在！(Id:{0})", id));
                    dao.Entry(record).State = EntityState.Deleted;
                }
                dao.SaveChanges();
            }
        }


        /// <summary>
        ///     导出
        /// </summary>
        /// <returns></returns>
        public void Export(DataTable table, string fileName)
        {
            table.TableName = "MarksixRecord";
            var toExcel = new DataTableToExcel();
            toExcel.Export(table, fileName, "MarksixRecord");
        }

    }

}