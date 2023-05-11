using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HCP.Material.DO;

namespace HCP.Material.IF
{
    public interface IItemCodesRepository
    {
        Task<List<DO_ItemCodes>> GetItemList();

        Task<List<DO_ItemCodes>> GetItemDetails(int ItemCode);

        Task<List<DO_ItemCodes>> GetItemMaster(int ItemGroup, int ItemCategory, int ItemSubCategory);

        Task<DO_ItemCodes> GetItemParameterList(int ItemCode);

        Task<DO_ReturnParameter> InsertItemCodes(DO_ItemCodes ItemCodes);

        Task<DO_ReturnParameter> UpdateItemCodes(DO_ItemCodes ItemCodes);

        Task<List<DO_ItemBusinessLink>> GetCustodianStore(int BusinessKey, int ItemCode);

        Task<List<DO_StoreMaster>> GetCustodianStoreListbyAccountingStore(int Businesskey, int Itemcode, int accountingcode);

        Task<DO_ReturnParameter> InsertOrUpdateItemCodetoCustodianStore(List<DO_ItemCodeLinkCustodianStore> obj);

        Task<DO_ItemConfigurartion> GetItemTree();
        Task<List<DO_ItemAccountingStore>> GetAccountingStoreForItem(int businesskey, int itemcode, string accountingType);
        Task<DO_ReturnParameter> InsertAccountingStoreForItem(List<DO_ItemAccountingStore> obj);

        Task<List<DO_ItemReorderLevel>> GetItemReorderLevel(int BusinessKey, int StoreCode);

        Task<DO_ReturnParameter> InsertOrUpdateItemReorderLevel(List<DO_ItemReorderLevel> sd);

        Task<List<DO_ItemAccountingStore>> GetStoreClassStorebyBusinessKey(int businesskey);

        Task<List<DO_ItemAccountingStore>> GetCustodianStoreListbyAccountType(int Businesskey, int Itemcode, string actype);
    }
}
