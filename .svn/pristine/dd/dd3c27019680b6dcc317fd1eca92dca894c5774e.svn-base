using HCP.Material.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HCP.Material.IF
{
    public interface ICommonRepository
    {
        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codeType);

        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType);

        Task<List<DO_ItemGroup>> GetItemGroup();

        Task<List<DO_ItemCategory>> GetItemCategory(int ItemGroup);

        Task<List<DO_ItemSubCategory>> GetItemSubCategory(int ItemCategory);

        Task<List<DO_ItemGroupCategoryLink>> GetItemCategoryForItemGroup(int ItemGroup);

        Task<List<DO_UnitsofMeasure>> GetUnitofMeasure();

        Task<List<DO_BusinessLocation>> GetBusinessKey();

        Task<List<DO_StoreMaster>> GetAccountingStore();

        Task<List<DO_StoreMaster>> GetCustodianStore();

        Task<List<DO_StoreMaster>> GetStoreListByBusinessKey(int BusinessKey);
    }
}
