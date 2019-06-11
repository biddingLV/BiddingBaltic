using Bidding.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Pagination
{
    public class Pagination
    {
        public static void PaginateResponse<T>(ref T response, int pageSize, int page) where T : BaseListResponseModel
        {
            var pageNum = page;

            if (response.IsReponseEmpty()) return;

            int totalPages = 0;

            if (response.ItemCount > pageSize)
            {
                totalPages = response.ItemCount / pageSize;
                if (response.ItemCount % pageSize > 0)
                {
                    totalPages++;
                }
            }
            else
            {
                totalPages = 1;
            }

            if (totalPages < pageNum)
            {
                pageNum = totalPages;
            }

            if (((pageNum - 1) * pageSize) < 0)
            {
                response.Offset = 0;
            }
            else
            {
                response.Offset = (pageNum - 1) * pageSize;
            }

            response.TotalPages = totalPages;
            response.CurrentPage = page;

            return;
        }

        public static (int, int) GetStartAndEnd(BaseListRequestModel request)
        {
            int startFromThisItem = request.OffsetStart;
            int takeUntilThisItem = request.OffsetEnd;
            int startFrom = Math.Max(startFromThisItem - 1, 0) * takeUntilThisItem;
            int endAt = startFrom + takeUntilThisItem;
            return (startFrom, takeUntilThisItem);
        }
    }
}
